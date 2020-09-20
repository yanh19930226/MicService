using MicService.Contact.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace MicService.Contact.Api.Data.Impletment
{
    public class MongoContactApplyRequestRepository : IContactApplyRequestRepository
    {
        private readonly ContactContext _contactContext = new ContactContext("mongodb://120.78.1.82:27017", "beta_contactbooks");
        /// <summary>
        /// 请求添加好友
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddRequestAsync(ContactApplyRequest request, CancellationToken cancellationToken)
        {
            var filter = Builders<ContactApplyRequest>.Filter.Where(q => q.UserId == request.UserId && q.AppliedId == request.AppliedId);
            //如果已经有了该好友请求更新请求时间
            if ((await _contactContext.ContactApplyRequests.CountDocumentsAsync(filter)) > 0)
            {
                var update = Builders<ContactApplyRequest>.Update.Set(r => r.ApplyTime, DateTime.Now);
                var updateRes = await _contactContext.ContactApplyRequests.UpdateOneAsync(filter, update);
                return updateRes.MatchedCount == updateRes.ModifiedCount && updateRes.MatchedCount == 1;
            }
            await _contactContext.ContactApplyRequests.InsertOneAsync(request, null, cancellationToken);
            return true;
        }
        /// <summary>
        /// 是否同意好友申请
        /// </summary>
        /// <param name="appliedId"></param>
        /// <returns></returns>
        public async Task<bool> ApprovalAsync(int userId, int appliedId, CancellationToken cancellationToken)
        {
            var filter = Builders<ContactApplyRequest>.Filter.Where(q => q.UserId == userId && q.AppliedId == appliedId);
            var update = Builders<ContactApplyRequest>.Update.Set(r => r.Approvaled, 1).Set(r => r.HandleTime, DateTime.Now);
            var updateRes = await _contactContext.ContactApplyRequests.UpdateOneAsync(filter, update, null, cancellationToken);
            return updateRes.MatchedCount == updateRes.ModifiedCount && updateRes.MatchedCount == 1;
        }
        /// <summary>
        /// 获取当前用户的好友申请列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<ContactApplyRequest>> GetRequestListAsync(int userId, CancellationToken cancellationToken)
        {
            return (await _contactContext.ContactApplyRequests.FindAsync(a => a.UserId == userId)).ToList(cancellationToken);
        }
    }
}
