using Microsoft.AspNetCore.Http;
using MimeKit;
using System.Collections.Generic;

namespace Evaluation.Portal.API.Models
{
    public class EmailRequest
    {
        public List<string> ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<IFormFile> Attachments { get; set; }
    }
}