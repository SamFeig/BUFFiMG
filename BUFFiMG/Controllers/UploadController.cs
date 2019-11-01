using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BUFFiMG.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.Logging;

namespace BUFFiMG.Controllers
{
    public class UploadController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment he;

        private static List<string> imgNames = new List<string>();
        private static List<string> imgTitles = new List<string>();

        public UploadController(ILogger<HomeController> logger, IWebHostEnvironment e)
        {
            _logger = logger;
            he = e;
        }

        public IActionResult Index()
        {
            return View("Upload");
        }

        public async Task<IActionResult> Upload(FormImage formImage) //the model will automatically populate since the property names match the form names
        {
            //make the variable's name shorter
            IFormFile img = formImage.ImageUpload;

            //check if it exists
            if (img != null)
            {
                var imageId = RandomString(8);

                var fileLocation = Path.Combine(he.WebRootPath, "user_images", Path.GetFileName(imageId) + Path.GetExtension(img.FileName));

                //copy the image to that location
                await using var stream = new FileStream(fileLocation, FileMode.Create);
                try
                {
                    await img.CopyToAsync(stream);
                }
                finally
                {
                    stream.Close();
                }

                //replace with SQL code

                //pass the location's index 
                return RedirectToAction("Image","Image", new { imageName = imageId });
            }
            else
            {
                //they didn't upload a file
                return RedirectPermanent("/Home");
            }
        }

        private Random random = new Random();
        private string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}