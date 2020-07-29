using Microsoft.AspNetCore.Http;
using System;

namespace DatingApp.API.Controllers
{
    public class PhotoForCreationDto
    {
        public string Url { get; set; }
        public IFormFile File { get; set; }
        public string Descriprion { get; set; }
        public DateTime DateAdded { get; set; }
        public string PublicId { get; set; }
        public PhotoForCreationDto()
        {
            this.DateAdded = DateTime.Now;
        }
    }
}