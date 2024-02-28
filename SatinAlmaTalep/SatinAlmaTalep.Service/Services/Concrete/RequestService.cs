using SatinAlmaTalep.Data.UnitOfWorks;
using SatinAlmaTalep.Entity.Entities;
using SatinAlmaTalep.Service.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatinAlmaTalep.Service.Services.Concrete
{
    public class RequestService : IRequestService
    {
        private readonly IUnitOfWork unitOfWork;

        public RequestService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Request> AddRequestAsync(Request request)
        {
            await unitOfWork.GetRepository<Request>().AddAsync(request);
            await unitOfWork.SaveAsync();
            return request;
        }

        public async Task<Request> DeleteRequestAsync(Request request)
        {
            await unitOfWork.GetRepository<Request>().DeleteAsync(request);
            await unitOfWork.SaveAsync();
            return request;
        }

        public async Task<List<Request>> GetAllRequestsAsync()
        {
            return await unitOfWork.GetRepository<Request>().GetAllAsync();
        }

        public async Task<List<Request>> GetApprovedRequest()
        {
            var requests = await unitOfWork.GetRepository<Request>().GetAllAsync();
            List<Request> approvedRequests = new List<Request>();
            foreach (var item in requests)
            {
                if (item.Status == Entity.Enums.RequestStatus.Approved)
                {
                    approvedRequests.Add(item);
                }
            }
            return approvedRequests;

        }

        public async Task<Request> GetRequestByIdAsync(int id)
        {
            return await unitOfWork.GetRepository<Request>().GetByIdAsync(id);
        }

        public async Task<Request> UpdateRequestAsync(Request request)
        {
            await unitOfWork.GetRepository<Request>().UpdateAsync(request);
            await unitOfWork.SaveAsync();
            return request;
        }
    }
}
