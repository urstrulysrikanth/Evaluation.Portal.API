using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Evaluation.Portal.API.Models
{
    [BsonIgnoreExtraElements]
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string UserId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("tcsEmailId")]
        public string TcsEmailId { get; set; }

        [BsonElement("clientEmailId")]
        public string ClientEmailId { get; set; }

        [BsonElement("mobile")]
        public long Mobile { get; set; }
        [BsonElement("employeeId")]
        public long EmployeeId { get; set; }

        [BsonElement("role")]
        public string Role { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("isActive")]
        public bool IsActive { get; set; }

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
