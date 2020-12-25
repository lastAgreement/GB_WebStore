using Microsoft.AspNetCore.Mvc;
using System;
using WebStore.Infrastructure.Interfaces;
using WebStore.ViewModels;
using WebStore.Models;
using Microsoft.AspNetCore.Authorization;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData _employeesData;
        public EmployeesController(IEmployeesData employeesData)
        {
            _employeesData = employeesData;
        }
        public IActionResult Index()
        {
            return View(_employeesData.GetAll());
        }

        public IActionResult Details(int id)
        {
            var empl = _employeesData.Get(id);
            if (empl is null) return NotFound();
            return View(new EmployeeView(empl));
        }

        [Authorize(Roles = Role.Administrator)]
        public IActionResult Edit (int? id)
        {
            if (id is null) return View(new EmployeeView());

            var empl = _employeesData.Get((int)id);
            if (empl is null) return NotFound();
            return View(new EmployeeView(empl));
        }

        [HttpPost]
        [Authorize(Roles = Role.Administrator)]
        public IActionResult Edit(EmployeeView Model)
        {
            if (!ModelState.IsValid) return View(Model);

            if (Model is null)
                throw new ArgumentNullException(nameof(Model));
            //!
            var employee = new Employee(Model);

            if (employee.Id == 0)
                _employeesData.Add(employee);
            else
                _employeesData.Update(employee);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = Role.Administrator)]
        public IActionResult Delete(int id)
        {
            _employeesData.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
