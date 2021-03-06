﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.ViewModels;

namespace WebStore.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }
        public int Age { get; set; }
        public Employee() { }
        public Employee(EmployeeView employee)
        {
            Id = employee.Id;
            FirstName = employee.FirstName;
            LastName = employee.LastName;
            Phone = employee.Phone;
            Email = employee.Email;
            Notes = employee.Notes;
            Age = employee.Age;
        }
        public void SetValuesFrom(Employee employee)
        {
            FirstName = employee.FirstName;
            LastName = employee.LastName;
            Phone = employee.Phone;
            Email = employee.Email;
            Notes = employee.Notes;
            Age = employee.Age;
        }
    }
}
