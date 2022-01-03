using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Evaluation.Portal.API.Models
{
    [BsonIgnoreExtraElements]
    public class Engagement
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string EngagementId { get; set; }         

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("skillSet")]
        public string SkillSet { get; set; }

        [BsonElement("experience")]
        public int Experience { get; set; }

        [BsonElement("numberOfPositions")]
        public int NumberOfPositions { get; set; }

        [BsonElement("location")]
        public string Location { get; set; }

        [BsonElement("closeBy")]
        public DateTime CloseBy { get; set; }

        [BsonElement("createdBy")]
        public string CreatedBy { get; set; }

        [BsonElement("createdDate")]
        public DateTime? CreatedDate { get; set; }

        [BsonElement("updatedBy")]
        public string UpdatedBy { get; set; }

        [BsonElement("updatedDate")]
        public DateTime? UpdatedDate { get; set; }
    }
}
