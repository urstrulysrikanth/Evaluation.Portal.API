using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Evaluation.Portal.API.Models
{
    public class ReportFilter
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("from")]
        public DateTime? From { get; set; }

        [JsonProperty("to")]
        public DateTime? To { get; set; }
    }
  
    public class Report
    {
        [JsonProperty("reportLabels")]
        public List<string> ReportLabels { get; set; }

        [JsonProperty("reportData")]
        public List<ReportData> ReportData { get; set; }
    }

    public class ReportData
    {
        [JsonProperty("data")]
        public IEnumerable<object> Data { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }
}
