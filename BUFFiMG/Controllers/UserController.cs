using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authorization;
using BUFFiMG.Models;
using System.IO;
using System.Security.Claims;

namespace BUFFiMG.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View("Error");
            }
            var user = User.FindFirst(ClaimTypes.NameIdentifier);
            var userId = user.Value;
            var db = new BUFFiMG.Data.buffimgContext();
            var currentUser = db.AspNetUsers.SingleOrDefault(u => u.Id == userId);

            if(currentUser == null){
                return View("Error");
            }

            var photos = currentUser.Photos;

            var ImageModel = new UserImagesModel();

            foreach(var photo in photos) {
                var path = Path.Combine("/user_images/", photo.FilePath + photo.FileExtension);

                ImageModel.imageList.Add(new DisplayImage(){src=path, tags=new List<string>()});
                }

            return View(ImageModel);
        }

        //
        // GET: /HelloWorld/Welcome/

        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;

            return View();
        }
    }
}