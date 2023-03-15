using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using biggschool.Models;
using biggschool.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Provider;

namespace biggschool.Controllers
{
    public class CoursesController : Controller
    {
        // GET: Courses
        private readonly ApplicationDbContext _dbContext;

        public CoursesController()
        {
            _dbContext = new ApplicationDbContext();
        }
        // GET: Courses
        [Authorize]

        public ActionResult Create()

        {
            var viewModel = new CourseViewModel
            {
                Categories = _dbContext.Categories.ToList()
            };

            return View(viewModel);

        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseViewModel viewModel)

        {
            {
                if (!ModelState.IsValid)
                {
                    viewModel.Categories = _dbContext.Categories.ToList();
                    return View("Create", viewModel);
                }
                var course = new Course
                {
                    LecturerId = User.Identity.GetUserId(),
                    DateTime = viewModel.GetDateTime(),
                    CategoryId = viewModel.Category,
                    Place = viewModel.Place

                };
                _dbContext.Courses.Add(course);
                _dbContext.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
        }
    }
}