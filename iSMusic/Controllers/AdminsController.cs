using iSMusic.Models.EFModels;
using iSMusic.Models.Infrastructures.Extensions;
using iSMusic.Models.Infrastructures.Repositories;
using iSMusic.Models.Services;
using iSMusic.Models.Services.Interfaces;
using iSMusic.Models.ViewModels;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace iSMusic.Controllers
{
    public class AdminsController : Controller
    {
        private AppDbContext db = new AppDbContext();
        public  AdminRepository adminRepository;
        public  AdminService adminService;
        public AdminsController()
        {
            var db = new AppDbContext();        
            IAdminRepository repo = new AdminRepository(db);
            this.adminService = new AdminService(repo);
        }

        // GET: Admin
        public ActionResult Index()
        {            
            var data = adminService.Search(null, null).Select(x =>x.ToAdminIndexVM()).ToList();
            return View(data);
        }
        
        public ActionResult Create()
        {            
            ViewBag.departmentId = new SelectList(db.Departments, "id", "departmentName").Prepend(new SelectListItem { Value = string.Empty, Text = "請選擇" });
            return View();
        }

        [HttpPost]
        public ActionResult Create(AdminCreateVM model)
        {
            try
            {
                ViewBag.departmentId = new SelectList(db.Departments, "id", "departmentName").Prepend(new SelectListItem { Value = string.Empty, Text = "請選擇" });

                adminService.adminCreate(model.ToAdminCreateDTO());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "新增失敗," + ex.Message);
            }

            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}