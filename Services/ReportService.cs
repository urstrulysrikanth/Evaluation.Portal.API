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

            switch (reportFilter.Name?.ToUpper())
            {
                case "PROFILES RECEIVED BY MONTH":
                    FilterDefinitionBuilder<Candidate> filterBuilder = Builders<Candidate>.Filter;

                    FilterDefinition<Candidate> filter = filterBuilder.Gte(x => x.Details.CreatedDate, reportFilter.From) & filterBuilder.Lte(x => x.Details.CreatedDate, reportFilter.To);

                    //DateRange filter on DB
                    List<Candidate> result = _candidates.Find(filter).ToListAsync().Result;

                    //GetMonth names
                    var monthRanges = result.GroupBy(i => i.Details.CreatedDate.HasValue ? i.Details.CreatedDate.Value.Month : 0)
                          .Select(g => new
                          {
                              MonthNumber = g.Key,
                              MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key)
                          });

                    //Unique soures
                    IEnumerable<string> sources = result.Select(x => x.Source.Name).Distinct();

                    //Labels
                    report.ReportLabels = new List<string>();
                    report.ReportLabels.AddRange(monthRanges.Select(x => x.MonthName));

                    List<ReportData> reportData = new List<ReportData>();

                    List<string> dataList = new List<string>();

                    foreach (var soure in sources)
                    {
                        List<object> data = new List<object>();

                        foreach (var month in monthRanges)
                        {
                            var countData = result.Where(x => x.Source.Name == soure && x.Details.CreatedDate.Value.Month == month.MonthNumber).Count();
                            data.Add(countData);
                        }
                        reportData.Add(new ReportData()
                        {
                            Data = data,
                            Label = soure
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
