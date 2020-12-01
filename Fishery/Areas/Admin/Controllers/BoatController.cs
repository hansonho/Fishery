using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fishery.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fishery.Areas.Admin.Controllers
{
    public class BoatController : Controller
    {
        private readonly FisheryContext _context;

        public BoatController(FisheryContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = _context.Boat.ToList();
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            return View(_context.Boat.Find(id));
        }

        public IActionResult Create()
        {
            return View("~/Areas/Admin/Views/Boat/Edit.cshtml", new Boat());
        }

        [HttpPost]
        public IActionResult Save(Boat model)
        {
            if (ModelState.IsValid)
            {
                _context.Boat.Update(model);
                _context.SaveChanges();
            }
            else
            {
                return View("~/Areas/Admin/Views/Boat/Edit.cshtml", model);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _context.Boat.Remove(_context.Boat.Find(id));
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
