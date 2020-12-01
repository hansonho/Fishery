using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Fishery.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fishery.Areas.Admin.Controllers
{
    public class ScenicController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly FisheryContext _context;

        public ScenicController(IWebHostEnvironment env, FisheryContext context)
        {
            _env = env;
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.ScenicSpot.ToList());
        }

        public IActionResult Edit(int id)
        {
            return View(new ScenicPageModel() { ScenicSpot = _context.ScenicSpot.Find(id) });
        }

        public IActionResult Create()
        {
            return View("~/Areas/Admin/Views/Scenic/Edit.cshtml", 
                new ScenicPageModel() { ScenicSpot = new ScenicSpot() { Id = 0 } });
        }

        public IActionResult Save(ScenicPageModel model) 
        {
            if (ModelState.IsValid)
            {
                var scenicSpot = _context.ScenicSpot.Find(model.ScenicSpot.Id);
                if (model.ScenicSpot.Id != 0)
                {
                    if (scenicSpot == null) return Content("資料錯誤");
                    else
                    {
                        scenicSpot.Name = model.ScenicSpot.Name;
                        scenicSpot.Desc = model.ScenicSpot.Desc;
                        scenicSpot.Seq = model.ScenicSpot.Seq;
                        scenicSpot.UpdateTime = DateTime.Now;
                    }
                }
                else
                {
                    scenicSpot = model.ScenicSpot;
                    scenicSpot.UpdateTime = DateTime.Now;
                    _context.ScenicSpot.Add(scenicSpot);
                }

                HandleUploadImage(model, scenicSpot, "Cover");
                HandleUploadImage(model, scenicSpot, "Pic1");
                HandleUploadImage(model, scenicSpot, "Pic2");
                HandleUploadImage(model, scenicSpot, "Pic3");

                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("~/Areas/Admin/Views//Edit.cshtml", model.ScenicSpot.Id);
        }

        private void HandleUploadImage(ScenicPageModel model, ScenicSpot scenicSpot, string PropertyName)
        {
            var iFormFile = (IFormFile)GetPropValue(model, PropertyName);
            if (iFormFile != null)
            {
                //有選擇檔案, 先檢查檔名
                string filename = iFormFile.FileName;
                var targetFile = Path.Combine(_env.WebRootPath, "images", "scenics", iFormFile.FileName);
                int index = 1;

                while (System.IO.File.Exists(targetFile))
                {
                    //改名改到不同名稱為止
                    filename = String.Format("{0}_{1}.{2}",
                        Path.GetFileNameWithoutExtension(iFormFile.FileName),
                        index,
                        Path.GetExtension(iFormFile.FileName));
                    targetFile = Path.Combine(_env.WebRootPath, "images", "scenics", filename);
                    index++;
                }
                iFormFile.CopyTo(new FileStream(targetFile, FileMode.Create));

                // 這邊要剛好 PageModel 裡面的 IFormFile 屬性名稱 與 ScenicSpot裡面的一樣
                SetPropValue(model.ScenicSpot, PropertyName, filename);

                SetPropValue(scenicSpot, PropertyName, filename);
            }
        }

        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        public static void SetPropValue(object src, string propName, object value)
        {
            src.GetType().GetProperty(propName).SetValue(src, value);
        }

        public IActionResult Delete(int id)
        {
            _context.ScenicSpot.Remove(_context.ScenicSpot.Find(id));
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }

    public class ScenicPageModel
    { 
        public ScenicSpot ScenicSpot { get; set; }
        public IFormFile Cover { get; set; }
        public IFormFile Pic1 { get; set; }
        public IFormFile Pic2 { get; set; }
        public IFormFile Pic3 { get; set; }

    }
}
