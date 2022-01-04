using Evaluation.Portal.API.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;


namespace Evaluation.Portal.API.Services
{
    public class ReportService
    {
        private readonly IMongoCollection<Candidate> _candidates;
        private readonly IMongoCollection<CandidateHistory> _candidateHistory;

        public ReportService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _candidates = database.GetCollection<Candidate>(settings.CandidatesCollectionName);
            _candidateHistory = database.GetCollection<CandidateHistory>(settings.CandidateHistoryCollectionName);

        }

        public Report GetReport(ReportFilter reportFilter)
        {
            Report report = new Report();
            
            switch (reportFilter.Name.ToUpper())
            {
                case "PROFILES RECEIVED":
                    FilterDefinitionBuilder<Candidate> filterBuilder = Builders<Candidate>.Filter;

                    FilterDefinition<Candidate> filter = filterBuilder.Gte(x => x.Details.CreatedDate, reportFilter.From) & filterBuilder.Lte(x => x.Details.CreatedDate, reportFilter.To);

                    report.ReportLabels = _candidates.Find(filter).ToListAsync().Result.GroupBy(i => i.Details.CreatedDate.HasValue ? i.Details.CreatedDate.Value.Month : 0)
                         .Select(g => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key));


                    IEnumerable<string> sources = _candidates.Find(filter).ToListAsync().Result.GroupBy(i => i.Source.Name)
                         .Select(g => g.Key);

                    List<ReportData> reportData = new List<ReportData>();
                    foreach (var source in sources)
                    {
                        filter = filterBuilder.Gte(x => x.Details.CreatedDate, reportFilter.From) & filterBuilder.Lte(x => x.Details.CreatedDate, reportFilter.To) & filterBuilder.Eq(x => x.Source.Name, source);

                        var data = _candidates.Find(filter).ToListAsync().Result.GroupBy(i => i.Details.CreatedDate.HasValue ? i.Details.CreatedDate.Value.Month : 0)
                            .Select(g => new
                            {
                                Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key),
                                count = g.Count()
                            });

                        reportData.Add(new ReportData
                        {
                            Label = source,
                            Data = data.Select(x => x.count as object).ToList()
                        });
                    }
                    report.ReportData = reportData;
                    break;
                default:
                    break;
            }
            return report;
        } 
    }
}
