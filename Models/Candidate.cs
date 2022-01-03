using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;


namespace Evaluation.Portal.API.Models
{
    [BsonIgnoreExtraElements]
    public class Candidate
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string CandidateId { get; set; }

        [BsonElement("source")]
        public Source Source { get; set; }

        [BsonElement("details")]
        public Details Details { get; set; }
    }


    public class Details
    {
        [BsonElement("epNumber")]
        public string EpNumber { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("mailId")]
        public string MailId { get; set; }

        [BsonElement("mobile")]
        public long Mobile { get; set; }

        [BsonElement("skillSet")]
        public string SkillSet { get; set; }

        [BsonElement("location")]
        public string Location { get; set; }

        [BsonElement("experience")]
        public int Experience { get; set; }

        [BsonElement("availability")]
        public string Availability { get; set; }

        [BsonElement("engagement")]
        public string Engagement { get; set; }

        [BsonElement("status")]
        public string Status { get; set; }

        [BsonElement("pendingSinceDays")]
        public int PendingSinceDays { get; set; }

        [BsonElement("resumes")]
        public string Resumes { get; set; }

        [BsonElement("eligibilityXls")]
        public string EligibilityXls { get; set; }

        [BsonElement("joiningDate")]
        public DateTime? JoiningDate { get; set; }

        [BsonElement("createdBy")]
        public string CreatedBy { get; set; }

        [BsonElement("createdDate")]
        public DateTime? CreatedDate { get; set; }

        [BsonElement("updatedBy")]
        public string UpdatedBy { get; set; }

        [BsonElement("updatedDate")]
        public DateTime? UpdatedDate { get; set; }
    }


    public class Source
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("details")]
        public string Details { get; set; }

        [BsonElement("mailId")]
        public string MailId { get; set; }

        [BsonElement("dateOfReceiving")]
        public DateTime? DateOfReceiving { get; set; }

        [BsonElement("tagged")]
        public string Tagged { get; set; }
    }

    public class CandidateHistory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string CandidateHistoryId { get; set; }

        [BsonElement("candidateId")]
        public string CandidateId { get; set; }

        [BsonElement("engagement")]
        public string Engagement { get; set; }

        [BsonElement("status")]
        public string Status { get; set; }

        [BsonElement("feedback")]
        public string Feedback { get; set; }

        [BsonElement("evaluatedBy")]
        public string EvaluatedBy { get; set; }

        [BsonElement("updatedBy")]
        public string UpdatedBy { get; set; }

        [BsonElement("updatedDate")]
        public DateTime? UpdatedDate { get; set; }
    }
}
