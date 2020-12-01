using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Fishery.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fishery.Areas.Admin.Controllers
{
    public class FoodController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly FisheryContext _context;

        public FoodController(IWebHostEnvironment env, FisheryContext context)
        {
            _env = env;
            _context = context;
        }

        /// <summary>
        /// 預設是餐廳管理
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var model = _context.Food.OrderBy(a => a.Seq).ToList();
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var model = _context.Food.Find(id);
            return View(new FoodPageModel() { food = model });
        }

        public IActionResult Create()
        {
            return View("~/Areas/Admin/Views/Food/Edit.cshtml",new FoodPageModel() { food = new Food() { Id = 0} });
        }

        [HttpPost]
        public IActionResult Save(FoodPageModel model)
        {
            if (ModelState.IsValid)
            {
                var food = _context.Food.Find(model.food.Id);
                if (model.food.Id != 0)
                {
                    if (food == null) return Content("資料錯誤");
                    else
                    {
                        food.Name = model.food.Name;
                        food.Desc = model.food.Desc;
                        food.Seq = model.food.Seq;
                        food.UpdateTime = DateTime.Now;
                    }
                }
                else
                {
                    food = model.food;
                    food.UpdateTime = DateTime.Now;
                    _context.Food.Add(food);
                }

                if (model.coverImage != null)
                {
                    //有選擇檔案, 先檢查檔名
                    string filename = model.coverImage.FileName;
                    var targetFile = Path.Combine(_env.WebRootPath, "images", "foods", model.coverImage.FileName);
                    int index = 1;

                    while (System.IO.File.Exists(targetFile))
                    {
                        //改名改到不同名稱為止
                        filename = String.Format("{0}_{1}.{2}",
                            Path.GetFileNameWithoutExtension(model.coverImage.FileName),
                            index,
                            Path.GetExtension(model.coverImage.FileName));
                        targetFile = Path.Combine(_env.WebRootPath, "images", "foods", filename);
                        index++;
                    }
                    model.coverImage.CopyTo(new FileStream(targetFile, FileMode.Create));
                    model.food.Cover = filename;
                    food.Cover = filename;
                }
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("~/Areas/Admin/Views/Food/Edit.cshtml", model.food.Id);
        }

        /// <summary>
        /// 列出指定餐廳的菜單
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Menu(int id)
        {
            var food = _context.Food.Find(id);
            if (food == null)
            {
                return Content("輸入錯誤");
            }
            ViewData["name"] = food.Name;
            ViewData["foodid"] = food.Id;
            var menu = _context.FoodMenu.Where(a => a.FoodId == id).ToList();

            return View(menu);
        }

        public IActionResult MenuEdit(int id)
        {
            var menu = _context.FoodMenu.Find(id);
            if (menu == null) return Content("資料錯誤.");

            FoodMenuPageModel model = new FoodMenuPageModel()
            {
                 menu = menu
            };
            return View(model);
        }

        public IActionResult MenuCreate(int id)
        {
            FoodMenuPageModel model = new FoodMenuPageModel() { 
                menu = new FoodMenu() { 
                     Id = 0,
                     FoodId = id
                }
            };
            return View("~/Areas/Admin/Views/Food/MenuEdit.cshtml", model);
        }

        public IActionResult MenuSave(FoodMenuPageModel model)
        {
            if (ModelState.IsValid)
            {
                var menu = _context.FoodMenu.Find(model.menu.Id);
                if (model.menu.Id != 0)
                {
                    if (menu == null) return Content("資料錯誤");
                    else
                    {
                        menu.Name = model.menu.Name;
                        menu.Desc = model.menu.Desc;
                    }
                }
                else
                {
                    menu = model.menu;
                    _context.FoodMenu.Add(menu);
                }


                if (model.pic != null)
                {
                    //有選擇檔案, 先檢查檔名
                    string filename = model.pic.FileName;
                    var targetFile = Path.Combine(_env.WebRootPath, "images", "foods", model.pic.FileName);
                    int index = 1;

                    while (System.IO.File.Exists(targetFile))
                    {
                        //改名改到不同名稱為止
                        filename = String.Format("{0}_{1}.{2}",
                            Path.GetFileNameWithoutExtension(model.pic.FileName),
                            index,
                            Path.GetExtension(model.pic.FileName));
                        targetFile = Path.Combine(_env.WebRootPath, "images", "foods", filename);
                        index++;
                    }
                    model.pic.CopyTo(new FileStream(targetFile, FileMode.Create));
                    model.menu.Pic1 = filename;
                    menu.Pic1 = filename;
                }
                _context.SaveChanges();
                return RedirectToAction(actionName: "Menu", routeValues: new { id = model.menu.FoodId });
            }
            else
            { 
                return View("~/Areas/Admin/Views/Food/MenuEdit.cshtml", model); 
            }
            
        }

        public IActionResult Delete(int id)
        {
            // 先刪除所有菜單
            _context.FoodMenu.RemoveRange(_context.FoodMenu.Where(a => a.FoodId == id));
            _context.Food.Remove(_context.Food.Find(id));
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult MenuDelete(int id)
        {
            _context.FoodMenu.Remove(_context.FoodMenu.Find(id));
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }

    public class FoodPageModel
    {
        public Food food { get; set; }
        public IFormFile coverImage { set; get; }
    }

    public class FoodMenuPageModel
    { 
        public FoodMenu menu { get; set; }

        public IFormFile pic { get; set; }
    }
}
