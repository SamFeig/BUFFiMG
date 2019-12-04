using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BUFFiMG.Data;
using BUFFiMG.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;

namespace BUFFiMG.Controllers
{
    [Authorize]
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
            if (!User.Identity.IsAuthenticated)
            {
                return View("Error");
            }
            return View("Upload");
        }

        public async Task<IActionResult> Upload(IFormFile image) //the model will automatically populate since the property names match the form names
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View("Error");
            }

            //check if it exists
            if (image != null)
            {
                var user = User.FindFirst(ClaimTypes.NameIdentifier);
                var userId = user.Value;

                var imageId = RandomString(8);
                var fileExtension = Path.GetExtension(image.FileName);

                var fileLocation = Path.Combine(he.WebRootPath, "user_images", Path.GetFileName(imageId) + Path.GetExtension(image.FileName));

                var db = new buffimgContext();

                if (db.Photos.SingleOrDefault(p => p.FilePath == imageId) != null)
                {
                    return View("Error");
                }

                //copy the image to that location
                await using var stream = new FileStream(fileLocation, FileMode.Create);
                try
                {
                    await image.CopyToAsync(stream);
                }
                finally
                {
                    stream.Close();
                }

                db.Photos.Add(new Photos()
                {
                    FilePath = imageId, IsPublic = true, UserId = userId, FileExtension = fileExtension
                });

                await db.SaveChangesAsync();

                //replace with SQL code

                //pass the location's index 
                return RedirectToAction("Image","Image", new { imageName = imageId });
            }
            else
            {
                //they didn't upload a file
                return View("Error");
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