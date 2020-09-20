using MicService.Abstractions;
using MicService.Abstractions.IntegrationEvents;
using MicService.Contact.Api.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MicService.Contact.Api.Data.Impletment
{
    public class MogoContactRepository : IContactRepository
    {
        private readonly ContactContext _contactContext = new ContactContext("mongodb://localhost:27017", "beta_contactbooks");

        public async Task<bool> AddContact(int userId, UserInfo user, CancellationToken cancellationToken)
        {
            if (await _contactContext.ContactBooks.CountDocumentsAsync(q => q.UserId == userId) == 0)
            {
                await _contactContext.ContactBooks.InsertOneAsync(new ContactBook { UserId = userId });
            }
            var filter = Builders<ContactBook>.Filter.Eq(q => q.UserId, userId);
            var update = Builders<ContactBook>.Update.AddToSet(q => q.Contacts, new Models.Contact
            {
                UserId = user.UserId,
                Avatar = user.Avatar,
                Company = user.Company,
                Title = user.Title,
                Name = user.Name
            });
            var updateRes = await _contactContext.ContactBooks.UpdateOneAsync(filter, update, null, cancellationToken);
            return updateRes.MatchedCount == updateRes.ModifiedCount;
        }

        public async Task<List<Models.Contact>> GetContactAsync(int userid, CancellationToken cancellationToken)
        {
            var contactbook = (await _contactContext.ContactBooks.FindAsync(q => q.UserId == userid)).FirstOrDefault();
            if (contactbook != null)
            {
                return contactbook.Contacts;
            }
            return new List<Models.Contact>();
        }
        /// <summary>
        /// 更新好友标签
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        public async Task<bool> TagContactAsync(int userId, int contactId, List<string> tags, CancellationToken cancellationToken)
        {
            var filter = Builders<ContactBook>.Filter.And(
               Builders<ContactBook>.Filter.Eq(c => c.UserId, userId),
               Builders<ContactBook>.Filter.Eq("Contacts.UserId", contactId)
               );
            var update = Builders<ContactBook>.Update
                .Set("Contacts.$.Tags", tags);
            var updateRes = await _contactContext.ContactBooks.UpdateOneAsync(filter, update, null, cancellationToken);
            return updateRes.MatchedCount == updateRes.ModifiedCount && updateRes.ModifiedCount == 1;
        }

        public async Task<bool> UpdateContactInfo(UserProfileChangedEvent user, CancellationToken cancellationToken)
        {
            var contactbook = (await _contactContext.ContactBooks.FindAsync(q => q.UserId == user.UserId, null, cancellationToken)).FirstOrDefault(cancellationToken);
            if (contactbook == null)
            {
                return true;
            }
            var contactids = contactbook.Contacts.Select(c => c.UserId);
            var filter = Builders<ContactBook>.Filter.And(
                Builders<ContactBook>.Filter.In(c => c.UserId, contactids),
                Builders<ContactBook>.Filter.ElemMatch(c => c.Contacts, contact => contact.UserId == user.UserId)
                );
            var update = Builders<ContactBook>.Update
                .Set("Contacts.$.Name", user.Name)
                .Set("Contacts.$.Avatar", user.Avatar)
                .Set("Contacts.$.Company", user.Company)
                .Set("Contacts.$.Title", user.Title);
            var updateRes = _contactContext.ContactBooks.UpdateMany(filter, update);
            return updateRes.MatchedCount == updateRes.ModifiedCount;
        }
    }
}
