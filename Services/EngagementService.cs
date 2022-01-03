using Evaluation.Portal.API.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Evaluation.Portal.API.Services
{
    public class EngagementService
    {

        private readonly IMongoCollection<Engagement> _engagements;
        public EngagementService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _engagements = database.GetCollection<Engagement>(settings.EngagementsCollectionName);

        }

        public List<Engagement> Get()
        {
            List<Engagement> engagements;
            engagements = _engagements.Find(x => true).ToList();
            return engagements;
        }

        public Engagement Get(string id) =>
            _engagements.Find<Engagement>(emp => emp.EngagementId == id).FirstOrDefault();

        public void InsertEngagement(Engagement engagement)
        {
            _engagements.InsertOneAsync(engagement).ConfigureAwait(false);
        }

        public void UpdateEngagement(Engagement engagement)
        {
            UpdateDefinition<Engagement> updateDefinition = Builders<Engagement>.Update
                 .Set(m => m.SkillSet, engagement.SkillSet)
                 .Set(m => m.Experience, engagement.Experience)
                 .Set(m => m.CloseBy, engagement.CloseBy)
                 .Set(m => m.Name, engagement.Name)
                 .Set(m => m.Location, engagement.Location)
                 .Set(m => m.NumberOfPositions, engagement.NumberOfPositions)
                 .Set(m => m.UpdatedDate, DateTime.Now)
                 .Set(m => m.UpdatedBy, engagement.UpdatedBy);

            _engagements.UpdateOneAsync(Builders<Engagement>.Filter.Eq(c => c.EngagementId, engagement.EngagementId), updateDefinition, new UpdateOptions { IsUpsert = true });
        }

        public void DeleteEngagement(string engagementId)
        {
            FilterDefinition<Engagement> filterDefinition = Builders<Engagement>.Filter.Eq(u => u.EngagementId, engagementId);
            _engagements.DeleteOneAsync(filterDefinition).ConfigureAwait(false);
        }
    }
}
