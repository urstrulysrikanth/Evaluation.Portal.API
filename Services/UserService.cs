using Evaluation.Portal.API.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Evaluation.Portal.API.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;
        public UserService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<User>(settings.UsersCollectionName);

        }

        public List<User> Get()
        {
            List<User> users;
            users = _users.Find(x => true).ToList();
            return users;
        }

        public User Get(string id) =>
            _users.Find<User>(emp => emp.UserId == id).FirstOrDefault();

        public void InsertUser(User user)
        {
            _users.InsertOneAsync(user).ConfigureAwait(false);
        }

        public void UpdateUser(User user)
        {
            UpdateDefinition<User> updateDefinition = Builders<User>.Update
                 .Set(m => m.TcsEmailId, user.TcsEmailId)
                 .Set(m => m.ClientEmailId, user.ClientEmailId)
                 .Set(m => m.Name, user.Name)
                 .Set(m => m.Mobile, user.Mobile)
                 .Set(m => m.EmployeeId, user.EmployeeId)
                 .Set(m => m.Role, user.Role)
                 .Set(m => m.Type, user.Type)
                 .Set(m => m.IsActive, user.IsActive)
                 .Set(m => m.UpdatedDate, user.UpdatedDate)
                 .Set(m => m.UpdatedBy, user.UpdatedBy);

            _users.UpdateOneAsync(Builders<User>.Filter.Eq(c => c.UserId, user.UserId), updateDefinition, new UpdateOptions { IsUpsert = true });
        }

        public void DeleteUser(string userId)
        {
            FilterDefinition<User> filterDefinition = Builders<User>.Filter.Eq(u => u.UserId, userId);
            _users.DeleteOneAsync(filterDefinition).ConfigureAwait(false);
        }


        public List<User> GetTrAndMrUsers()
        {
            List<User> users;
            users = _users.Find(x => x.IsActive && new[] { "TR", "MR", "TR or MR" }.Contains(x.Type)).ToList();
            return users;
        }
    }
}
