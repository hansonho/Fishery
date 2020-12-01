using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Fishery.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fishery.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly FisheryContext _context;

        public HomeController(IWebHostEnvironment env, FisheryContext context)
        {
            _env = env;
            _context = context;
        }

        public IActionResult Index()
        {
            HomePageModel homePageModel = new HomePageModel() { 
                marquees = _context.Marquee.FirstOrDefault(),
                carousels = _context.Carousel.OrderBy(a => a.Seq).ToList()
            };
            return View(homePageModel);
        }

        public IActionResult SaveMarquees(HomePageModel model)
        {
            _context.Marquee.Update(model.marquees);
            _context.SaveChanges();
            
            return RedirectToAction("Index");
        }

        public IActionResult CarouselEdit(int id)
        {
            return View(new CarouselPageModel { carousel = _context.Carousel.Find(id) });
        }

        public IActionResult CarouselCreate()
        {
            return View("~/Areas/Admin/Views/Home/CarouselEdit.cshtml", new CarouselPageModel { carousel = new Carousel() { Id = 0, @Type="pic" } });
        }

        public IActionResult CarouselSave(CarouselPageModel model)
        {
            if (ModelState.IsValid)
            {
                var carousel = _context.Carousel.Find(model.carousel.Id);
                if (model.carousel.Id != 0)
                {
                    if (carousel == null) return Content("資料錯誤");
                    else
                    {
                        carousel.Type = model.carousel.Type;
                        carousel.Alt = model.carousel.Alt;
                        carousel.Seq = model.carousel.Seq;
                    }
                }
                else
                {
                    carousel = model.carousel;
                    _context.Carousel.Add(carousel);
                }

                if (model.carousel != null)
                {
                    //有選擇檔案, 先檢查檔名
                    string filename = model.Src.FileName;
                    var targetFile = Path.Combine(_env.WebRootPath, "images", "carousel", model.Src.FileName);
                    int index = 1;

                    while (System.IO.File.Exists(targetFile))
                    {
                        //改名改到不同名稱為止
                        filename = String.Format("{0}_{1}.{2}",
                            Path.GetFileNameWithoutExtension(model.Src.FileName),
                            index,
                            Path.GetExtension(model.Src.FileName));
                        targetFile = Path.Combine(_env.WebRootPath, "images", "carousel", filename);
                        index++;
                    }
                    model.Src.CopyTo(new FileStream(targetFile, FileMode.Create));
                    model.carousel.Src = filename;
                    carousel.Src = filename;
                }
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("~/Areas/Admin/Views/Home/CarouselEdit.cshtml", model.carousel.Id);
        }

        public IActionResult Delete(int id)
        {
            _context.Carousel.Remove(_context.Carousel.Find(id));
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }

    public class HomePageModel
    {
        public Marquee marquees { get; set; }
        public List<Carousel> carousels { get; set; }
    }

    public class CarouselPageModel 
    {
        public Carousel carousel { get; set; }
        public IFormFile Src { get; set; }
    }
}
