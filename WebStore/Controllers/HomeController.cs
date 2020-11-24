using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _Configuration;
        private static List<Employee> __Employees = new List<Employee>()
        {
            new Employee (1, "Emp1"),
            new Employee (2, "Emp2")
        };
        public HomeController(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SecondAction()
        {
            return Content(_Configuration["ControllerActionText"]);
        }
        public IActionResult Employees()
        {
            return View(__Employees);
        }
    }
}
