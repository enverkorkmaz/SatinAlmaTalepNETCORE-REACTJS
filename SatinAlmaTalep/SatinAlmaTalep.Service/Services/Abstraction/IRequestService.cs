using SatinAlmaTalep.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatinAlmaTalep.Service.Services.Abstraction
{
    public interface IRequestService
    {
        Task<List<Request>> GetAllRequestsAsync();
        Task<Request> GetRequestByIdAsync(int id);
        Task<Request> AddRequestAsync(Request request);
        Task<Request> UpdateRequestAsync(Request request);
        Task<Request> DeleteRequestAsync(Request request);
        Task<List<Request>> GetApprovedRequest();
    }
}
