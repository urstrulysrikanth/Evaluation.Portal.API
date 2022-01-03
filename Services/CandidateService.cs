using Evaluation.Portal.API.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Evaluation.Portal.API.Services
{
    public class CandidateService
    {
        private readonly IMongoCollection<Candidate> _candidates;
        private readonly IMongoCollection<CandidateHistory> _candidateHistory;

        public CandidateService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _candidates = database.GetCollection<Candidate>(settings.CandidatesCollectionName);
            _candidateHistory = database.GetCollection<CandidateHistory>(settings.CandidateHistoryCollectionName);

        }

        public List<Candidate> Get()
        {
            List<Candidate> candidates;
            candidates = _candidates.Find(x => true).ToList();
            return candidates;
        }

        public Candidate Get(string id) =>
            _candidates.Find<Candidate>(c => c.CandidateId == id).FirstOrDefault();

        public void InsertCandidate(Candidate candidate)
        {
            _candidates.InsertOneAsync(candidate).ConfigureAwait(false);
        }

        public void InsertCandidates(List<Candidate> candidates)
        {
            _candidates.InsertManyAsync(candidates).ConfigureAwait(false);
        }

        public void DeleteCandidate(string candidateId)
        {
            FilterDefinition<Candidate> filterDefinition = Builders<Candidate>.Filter.Eq(u => u.CandidateId, candidateId);
            _candidates.DeleteOneAsync(filterDefinition).ConfigureAwait(false);
        }


        public List<Candidate> GetNonRejectedCandidates()
        {
            List<Candidate> candidates;
            candidates = _candidates.Find(x => x.Details.Status != "Rejected").ToList();
            return candidates;
        }

        public List<Candidate> GetCandidatesByPanelSetupStatus()
        {
            List<Candidate> candidates;
            candidates = _candidates.Find(x => x.Details.Status == "Panel Setup").ToList();
            return candidates;
        }

        public void InsertCandidateHistory(CandidateHistory candidateHistory)
        {
            UpdateDefinition<Candidate> updateDefinition = Builders<Candidate>.Update
                .Set(m => m.Details.Status, candidateHistory.Status)
                .Set(m => m.Details.Engagement, candidateHistory.Engagement)
                .Set(m => m.Details.UpdatedDate, DateTime.Now)
                .Set(m => m.Details.UpdatedBy, "");

            _candidates.UpdateOneAsync(Builders<Candidate>.Filter.Eq(c => c.CandidateId, candidateHistory.CandidateId), updateDefinition,
                new UpdateOptions { IsUpsert = true });

            _candidateHistory.InsertOneAsync(candidateHistory).ConfigureAwait(false);
        }
    }
}
