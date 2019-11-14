using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BUFFiMG.Models
{
    public class FormImage
    {
        public IFormFile ImageUpload { get; set; }

        public string Title { get; set; }

        public List<string> Tags { get; set; }
    }
}
