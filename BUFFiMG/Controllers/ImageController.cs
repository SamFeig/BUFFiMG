using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
            //replace with SQL
            //get the file location

            var files = Directory.GetFiles(Path.Combine(he.WebRootPath, "user_images"), imageName + ".*");

            if (files.Length == 0) return View("Error");
            var fileName = Path.GetFileName(files[0]);

            var path = Path.Combine("/user_images/", fileName);

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