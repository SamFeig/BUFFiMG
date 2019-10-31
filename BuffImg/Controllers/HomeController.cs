using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BUFFiMG.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace BUFFiMG.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHostingEnvironment he;

        private static List<string> imgNames = new List<string>();
        private static List<string> imgTitles = new List<string>();

        public HomeController(ILogger<HomeController> logger, IHostingEnvironment e)
        {
            _logger = logger;
            he = e;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UploadPage()
        {
            return View();
        }

        /* //the base code to upload an image
        public IActionResult Upload(FormImage imageModel)
        {
            ViewData["name"] = imageModel.Title;
            IFormFile img = imageModel.ImageUpload;

            if(img != null)
            {
                var fileName = Path.Combine(he.WebRootPath, Path.GetFileName(img.FileName));
                img.CopyTo(new FileStream(fileName, FileMode.Create));
                ViewData["fileLocation"] = "/" + Path.GetFileName(img.FileName);
            }
            return View();
        }
        */

        //My code to upload an image and store the location
        public IActionResult Upload(FormImage formImage) //the model will automatically populate since the property names match the form names
        {
            //make the variable's name shorter
            IFormFile img = formImage.ImageUpload;

            //check if it exists
            if (img != null)
            {
                var fileLocation = Path.Combine(he.WebRootPath, "user_images/" + Path.GetFileName(img.FileName));
                
                //copy the image to that location
                img.CopyTo(new FileStream(fileLocation, FileMode.Create));

                //replace with SQL code
                //add it to a list
                imgNames.Add("/user_images/" + Path.GetFileName(img.FileName));
                imgTitles.Add(formImage.Title);
                
                //pass the location's index
                return RedirectPermanent("/Home/ImageViewer/id?imageId=" + (imgNames.Count-1));
            }
            else
            {
                //they didn't upload a file
                return RedirectPermanent("/Home");
            }
        }

        public IActionResult ImageViewer(int imageID)
        {
            //replace with SQL
            //get the file location
            var filePath = imgNames[imageID];

            //prep the model
            DisplayImage img = new DisplayImage();
            img.src = filePath;
            img.title = imgTitles[imageID];
            img.tags = new List<string>();

            //pass the model to the page
            return View(img);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
