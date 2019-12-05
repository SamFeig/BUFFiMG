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
using BUFFiMG.Data;

namespace BUFFiMG.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var db = new buffimgContext();

            var recentPhotos = db.Photos.OrderByDescending(p => p.PhotoId).Where(p => p.IsPublic).Take(10);
            var imageModel = new UserImagesModel();

            foreach (var recentPhoto in recentPhotos)
            {
                var path = Path.Combine("/user_images/", recentPhoto.FilePath + recentPhoto.FileExtension);

                imageModel.imageList.Add(new DisplayImage() { src = path, tags = new List<string>() });
            }

            return View(imageModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Test()
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
