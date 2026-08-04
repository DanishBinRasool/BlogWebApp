using BlogApp.Data;
using BlogApp.Models;
using BlogApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers
{
    public class PostController(AppDbContext context,IWebHostEnvironment webHostEnvironment) : Controller
    {
        private readonly string[] allowedImageExtensions = { ".jpg ", ".jpeg", ".png", ".gif",
                          ".avif",".webp",".jfif", ".heif", ".heic",".tif", ".tiff",".bmp",".svg",".eps","" };




        [HttpGet]
        public IActionResult Index(int? categoryId)
        {
            var postQuery = context.Posts.Include(c=>c.Category).AsQueryable();
            if (categoryId.HasValue)
            {
                postQuery = postQuery.Where(p => p.CategoryId == categoryId);
            }
            var posts = postQuery.ToList();
            ViewBag.categories = context.Categories.ToList();
            return View(posts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var postViewModel = new PostViewModel();
            postViewModel.Categories = context.Categories.Select(c=>
                new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }
            ).ToList();

           
            return View(postViewModel);
        }

        [HttpPost]

        public async Task<IActionResult> Create(PostViewModel postViewModel)
        {
            if (ModelState.IsValid)
            {
                var inputFileExtension = Path.GetExtension(postViewModel.FeatureImage.FileName).ToLower();
                if(allowedImageExtensions.Contains(inputFileExtension))
                {
                    postViewModel.post.FeatureImagePath = await UploadFileToFolder(postViewModel.FeatureImage);


                    await context.Posts.AddAsync(postViewModel.post);
                    await context.SaveChangesAsync();
                    return RedirectToAction("Index", "Post");
                }
                else
                {
                    ModelState.AddModelError("FeatureImage", "Invalid image format. Allowed formats are: " + string.Join(", ", allowedImageExtensions));
                    postViewModel.Categories = context.Categories.Select(c =>
                        new SelectListItem
                        {
                            Text = c.Name,
                            Value = c.Id.ToString()
                        }
                    ).ToList();
                    return View(postViewModel);
                }

               
            }
            // If the model state is not valid,return the view
            postViewModel.Categories = context.Categories.Select(c =>
                new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }
            ).ToList();
            return View(postViewModel);
        }

        private async Task<string> UploadFileToFolder(IFormFile file)
        {
            var inputFileExtension = Path.GetExtension(file.FileName);
            var fileName = Guid.NewGuid().ToString() + inputFileExtension;
            var wwwRootPath = webHostEnvironment.WebRootPath;
            var imagesFolderPath = Path.Combine(wwwRootPath, "images");

            // Below line does what above two lines do,but when we deploy the app to a server,
            // the below line will not work because it will look for the images folder in the current directory of the server,
            // which may not be the same as the wwwroot folder of the app.
            //var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
            if (!Directory.Exists(imagesFolderPath))
            {
                Directory.CreateDirectory(imagesFolderPath);

            }
            var filePath = Path.Combine(imagesFolderPath, fileName);
            try 
            {
                await using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
            catch(Exception ex)
            {
                return "Error uploading file: " + ex.Message;
            }

            return "/images/" + fileName;
        }
    }
}
