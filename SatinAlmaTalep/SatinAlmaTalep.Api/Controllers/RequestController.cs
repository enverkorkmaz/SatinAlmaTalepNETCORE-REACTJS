using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SatinAlmaTalep.Entity.DTOs.Product;
using SatinAlmaTalep.Entity.DTOs.Request;
using SatinAlmaTalep.Entity.Entities;
using SatinAlmaTalep.Service.Services.Abstraction;
using SatinAlmaTalep.Service.Services.Concrete;

namespace SatinAlmaTalep.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService requestService;
        private readonly IProductService productService;

        public RequestController(IRequestService requestService, IProductService productService)
        {
            this.requestService = requestService;
            this.productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRequests()
        {
            var response = await requestService.GetAllRequestsAsync();
            var jsonResponse = JsonConvert.SerializeObject(response);
            return Ok(jsonResponse);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRequest([FromBody] RequestDto requestDto)
        {
            if (requestDto == null)
            {
                return BadRequest("Request data is invalid.");
            }

            try
            {
                var product = await productService.GetProductByIdAsync(requestDto.ProductId);
                Request newRequest = new()
                {
                    ProductId = requestDto.ProductId,
                    Quantity = requestDto.Quantity,
                    Status = Entity.Enums.RequestStatus.Pending,
                    TotalPrice = requestDto.Quantity * product.Price,
                    Product = product

                };
                var createdRequest = await requestService.AddRequestAsync(newRequest);
                return Ok(createdRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the product: {ex.Message}");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetRequest(int requestId)
        {
            try
            {
                var request = await requestService.GetRequestByIdAsync(requestId);

                if (request == null)
                {
                    return NotFound($"Request with ID {requestId} not found.");
                }
                if(request.Product == null)
                {
                    var product = await productService.GetProductByIdAsync(request.ProductId);
                    request.Product = product;
                }
             

                

                return Ok(request);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching request details: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> ApproveRequest([FromBody] RequestUpdateDto requestUpdateDto)
        {
            if (requestUpdateDto == null)
            {
                return BadRequest("Product data is invalid.");
            }

            try
            {
                var existingRequest = await requestService.GetRequestByIdAsync(requestUpdateDto.Id);
                if (existingRequest == null)
                {
                    return NotFound("Request not found.");
                }

               existingRequest.Status = Entity.Enums.RequestStatus.Approved;

                var approvedRequest = await requestService.UpdateRequestAsync(existingRequest);
                return Ok(approvedRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the product: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> RejectRequest([FromBody] RequestUpdateDto requestUpdateDto)
        {
            if (requestUpdateDto == null)
            {
                return BadRequest("Product data is invalid.");
            }

            try
            {
                var existingRequest = await requestService.GetRequestByIdAsync(requestUpdateDto.Id);
                if (existingRequest == null)
                {
                    return NotFound("Request not found.");
                }

                existingRequest.Status = Entity.Enums.RequestStatus.Rejected;

                var approvedRequest = await requestService.UpdateRequestAsync(existingRequest);
                return Ok(approvedRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the product: {ex.Message}");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetApprovedRequests()
        {
            var approvedRequests = await requestService.GetApprovedRequest();
            var jsonResponse = JsonConvert.SerializeObject(approvedRequests);
            return Ok(jsonResponse);
        }
    }
}

