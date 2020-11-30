using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Controllers
{
    public class EmployeesController : Controller
    {
        public IActionResult Index()
        {
            return View(TestData.__Employees);
        }

        public IActionResult Details(int id)
        {
            var empl = TestData.__Employees.FirstOrDefault(x => x.Id == id);
            if (empl is null) return NotFound();
            return View(empl);
        }
    }
}
