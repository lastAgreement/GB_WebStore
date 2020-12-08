using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.ViewModels
{
    public class EmployeeView
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Display(Name = "Имя")]
        [Required(ErrorMessage ="Имя является обязательным!")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Минимум 3 символа!")]
        public string FirstName { get; set; }
        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Фамилия является обязательной!")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Минимум 3 символа!")]
        public string LastName { get; set; }
        [Display(Name = "Телефон")]
        public string Phone { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Заметки")]
        public string Notes { get; set; }
        [Display(Name = "Возраст")]
        [Range(18, 80, ErrorMessage = "От 18 до 80!")]
        public int Age { get; set; }
        public EmployeeView() { }
        public EmployeeView(Employee employee)
        {
            Id = employee.Id;
            FirstName = employee.FirstName;
            LastName = employee.LastName;
            Phone = employee.Phone;
            Email = employee.Email;
            Notes = employee.Notes;
            Age = employee.Age;
        }
    }
}
