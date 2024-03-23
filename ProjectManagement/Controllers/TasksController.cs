using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Models;
using TaskModel = ProjectManagement.Models.Task;

namespace ProjectManagement.Controllers
{
    public class TasksController : Controller
    {
        private static List<TaskModel> tasks = new List<TaskModel>();

        [HttpGet]
        public ActionResult Index()
        {
            return View(tasks);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TaskModel task)
        {
            tasks.Add(task);
            return RedirectToAction("Index");
        }
    }
}
