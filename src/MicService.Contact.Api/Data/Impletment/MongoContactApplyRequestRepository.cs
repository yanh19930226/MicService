using MicService.Contact.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MicService.Contact.Api.Data.Impletment
{
    public class MongoContactApplyRequestRepository : IContactApplyRequestRepository
    {
        private readonly ContactContext _contactContext = new ContactContext("mongodb://120.78.1.82:27017", "beta_contactbooks");
        public Task<bool> AddRequestAsync(ContactApplyRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ApprovalAsync(int userId, int appliedId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<ContactApplyRequest>> GetRequestListAsync(int userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
