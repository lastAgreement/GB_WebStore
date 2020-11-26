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
            new Employee {Id = 1, FirstName ="Иван", LastName = "Палочкин", Age =24, Email = "ip@gmail.com", Phone = "79153651526", Notes = "грубиян (" },
            new Employee {Id = 2, FirstName ="Анна", LastName = "Каренина", Age = 29, Email = "carenina@gmail.com", Phone = "79261512515", Notes = "инстаграмм @carenina" },
            new Employee {Id = 3, FirstName ="Михаил", LastName = "Подвойский", Age = 35, Email = "zubrra@gmail.com", Phone = "79295123748", Notes = "" }
        };
        public HomeController(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Employees()
        {
            return View(__Employees);
        }

        public IActionResult Details(int id)
        {
            return View(__Employees.Where(x => x.Id == id).First());
        }
    }
}
