using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Evaluation.Portal.API.Models
{
    [BsonIgnoreExtraElements]
    public class Panel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string PanelId { get; set; }

        [BsonElement("candidateId")]
        public string CandidateId { get; set; }

        [BsonElement("candidateName")]
        public string CandidateName { get; set; }

        [BsonElement("panelDate")]
        public DateTime? PanelDate { get; set; }

        [BsonElement("panelTimeZone")]
        public string PanelTimeZone { get; set; }

        [BsonElement("timeAndDate")]
        public DateTime? TimeAndDate { get; set; }

        [BsonElement("trAssociate")]
        public Associate TrAssociate { get; set; }

        [BsonElement("mrAssociate")]
        public Associate MrAssociate { get; set; }

        [BsonElement("selectedTr")]
        public string SelectedTr { get; set; }

        [BsonElement("selectedMr")]
        public string SelectedMr { get; set; }

        [BsonElement("createdBy")]
        public string CreatedBy { get; set; }

        [BsonElement("createdDate")]
        public DateTime? CreatedDate { get; set; }
        [BsonElement("updatedBy")]
        public string UpdatedBy { get; set; }

        [BsonElement("updatedDate")]
        public DateTime? UpdatedDate { get; set; }
    }


    public class Associate
    {
        [BsonElement("employeeId")]
        public long EmployeeId { get; set; }

        [BsonElement("tcsEmailId")]
        public string TcsEmailId { get; set; }

        [BsonElement("clientEmailId")]
        public string ClientEmailId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("mobile")]
        public long Mobile { get; set; }
    }
}
