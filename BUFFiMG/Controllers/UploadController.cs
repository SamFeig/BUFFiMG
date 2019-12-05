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
using Microsoft.Extensions.Hosting;

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

        public async Task<IActionResult> Upload(IFormFile image, string tags) //the model will automatically populate since the property names match the form names
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View("Error");
            }

            //check if it exists
            if (image == null) return View("Error");

            var user = User.FindFirst(ClaimTypes.NameIdentifier);
            var userId = user.Value;

            var imageId = RandomString(8);
            var fileExtension = Path.GetExtension(image.FileName);
            
            var tagList = new List<string>();

            if (tags != null)
            {
                tagList = tags.Split(',').Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            }

            if (fileExtension.ToLowerInvariant() == ".png" || fileExtension.ToLowerInvariant() == ".jpg" || fileExtension.ToLowerInvariant() == ".jpeg" || fileExtension.ToLowerInvariant() == ".gif")
            {
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

                var newPhoto = new Photos()
                {
                    FilePath = imageId,
                    IsPublic = true,
                    UserId = userId,
                    FileExtension = fileExtension
                };

                db.Photos.Add(newPhoto);

                var newTagsList = new List<Tags>();
                var existingTagsList = new List<Tags>();

                foreach (var tag in tagList)
                {
                    var normalizedTag = tag.ToLowerInvariant();
                    var existingTag = db.Tags.SingleOrDefault(t => t.Description == normalizedTag);

                    if (existingTag == null)
                    {
                        var tagId = new Random().Next(int.MaxValue);
                        // make sure tagId is unique
                        while (db.Tags.SingleOrDefault(t => t.TagId == tagId) != null)
                        {
                            tagId = new Random().Next(int.MaxValue);
                        }

                        newTagsList.Add(new Tags() { Description = normalizedTag, TagId = tagId });
                    }
                    else
                    {
                        existingTagsList.Add(existingTag);
                    }
                }

                db.Tags.AddRange(newTagsList);

                // add new photo and tags
                await db.SaveChangesAsync();

                newPhoto = db.Photos.Single(p => p.FilePath == imageId);

                foreach (var tag in newTagsList)
                {
                    db.PhotoTagMap.Add(new PhotoTagMap() { PhotoId = newPhoto.PhotoId, TagId = tag.TagId });
                }
                foreach (var existingTag in existingTagsList)
                {
                    db.PhotoTagMap.Add(new PhotoTagMap() { PhotoId = newPhoto.PhotoId, TagId = existingTag.TagId });
                }

                await db.SaveChangesAsync();

                return RedirectToAction("Image", "Image", new { imageName = imageId });
            }

            //they didn't upload a file
            return View("Error");
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