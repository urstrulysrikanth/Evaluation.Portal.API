using System;
using System.Collections.Generic;

namespace Evaluation.Portal.API.Models
{
    public class ReportFilter
    {
        public string Name { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

    }
    public class Report
    {
        public IEnumerable<string> ReportLabels { get; set; }

        public List<ReportData> ReportData { get; set; }
    }

    public class ReportData
    {
        public IEnumerable<object> Data { get; set; }

        public string Label { get; set; }
    }
}
