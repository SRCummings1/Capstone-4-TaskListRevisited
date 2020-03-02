using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Capstone3_TaskListRevisited1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Capstone3_TaskListRevisited1.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private readonly IdentityTaskListDbContext _context;
        public TasksController(IdentityTaskListDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            string id = User.FindFirst(ClaimTypes.NameIdentifier).Value;  // link between theh batabasese..... prmary key 
            List<Tasks> thisUsersTasks = _context.Tasks.Where(x => x.TaskOwnerId == id).ToList();
            return View(thisUsersTasks);
        }
        [HttpGet]
        public IActionResult AddTasks()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddTasks(Tasks newTask)
        {
            newTask.TaskOwnerId = User.FindFirst(ClaimTypes.NameIdentifier).Value;//finds the taskownerid that way you can assign the taks to the new owner
            if (ModelState.IsValid)
            {
                _context.Add(newTask);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }     
        }
        public IActionResult DeleteTasks(int Id)
        {
            Tasks foundTasks = _context.Tasks.Find(Id);
            _context.Tasks.Remove(foundTasks);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        public IActionResult EditTasks(int Id)
        {
            Tasks foundTasks = _context.Tasks.Find(Id);

            return View(foundTasks); 
        }

        [HttpPost]
        public IActionResult EditTasks(Tasks tasks)
        {
            Tasks foundTasks = _context.Tasks.Find(tasks.Id);

            if(ModelState.IsValid)
            {
                foundTasks.TaskDescription = tasks.TaskDescription;
                foundTasks.DueDate = tasks.DueDate;
                foundTasks.Completed = tasks.Completed;

                _context.Entry(foundTasks).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.Update(foundTasks);
                _context.SaveChanges();
            }


            return RedirectToAction("Index");
        }

        //public IActionResult SomeActionResultNameThatIsSuperCool(string myString, int myInt)
        //{
        //    if(myString == "steve is cool")
        //    {
        //        ViewBag.WhateverItTellsYouToCallIt = myInt * 2;
        //        return RedirectToAction("ActionName");
        //    }
        //    return View("ViewName");

        //}

    }
}