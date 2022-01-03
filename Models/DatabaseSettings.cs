namespace Evaluation.Portal.API.Models
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string CandidatesCollectionName { get; set; }
        public string CandidateHistoryCollectionName { get; set; }
        public string EngagementsCollectionName { get; set; }
        public string PanelsCollectionName { get; set; }
        public string UsersCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IDatabaseSettings
    {
        public string CandidatesCollectionName { get; set; }
        public string CandidateHistoryCollectionName { get; set; }
        public string EngagementsCollectionName { get; set; }
        public string PanelsCollectionName { get; set; }
        public string UsersCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
