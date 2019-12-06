using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BUFFiMG.Data;
using BUFFiMG.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BUFFiMG.Controllers
{
    public class SearchResultsController : Controller
    {
        public IActionResult Search(string tag)
        {
            var db = new buffimgContext();
            var imageModel = new UserImagesModel();

            var tags = db.Tags.Include(p => p.PhotoTagMap).ThenInclude(y => y.Photo).ToList();
            var relevantTags = tags.Where(t => t.Description == tag.ToLowerInvariant()).ToList();

            foreach (var relevantTag in relevantTags)
            {
                foreach (var photoTagMap in relevantTag.PhotoTagMap)
                {
                    if (!photoTagMap.Photo.IsPublic) continue;
                    var path = Path.Combine("/user_images/", photoTagMap.Photo.FilePath + photoTagMap.Photo.FileExtension);

                    imageModel.imageList.Add(new DisplayImage() { src = path, tags = new List<string>() });
                }
            }

            return View(imageModel);
        }
    }
}