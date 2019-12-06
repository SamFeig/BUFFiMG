using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BUFFiMG.Data;
using BUFFiMG.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BUFFiMG.Controllers
{
    public class ImageController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment he;

        public ImageController(ILogger<HomeController> logger, IWebHostEnvironment e)
        {
            _logger = logger;
            he = e;
        }

        public IActionResult Index()
        {
            return View("Image");
        }

        public IActionResult Image(string imageName)
        {
            var db = new buffimgContext();

            var photo = db.Photos.SingleOrDefault(p => p.FilePath == imageName);
            
            if (photo == null) return View("Error");

            var path = Path.Combine("/user_images/", photo.FilePath + photo.FileExtension);

            //prep the model
            DisplayImage img = new DisplayImage();
            img.src = path;
            img.title = "";
            img.tags = new List<string>();

            //pass the model to the page
            return View(img);
        }
    }
}