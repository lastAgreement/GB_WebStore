using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Infrastructure.Services
{
    public class InMemoryEmployeesData : IEmployeesData
    {
        private readonly List<Employee> _employees = TestData.__Employees;

        public IEnumerable<Employee> GetAll() => _employees;

        public Employee Get(int id) => _employees.FirstOrDefault(item => item.Id == id);

        public int Add(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));

            if (_employees.Contains(employee))
                return employee.Id;

            employee.Id = _employees.Select(item => item.Id).DefaultIfEmpty().Max() + 1;
            _employees.Add(employee);
            return employee.Id;
        }
        public void Update(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));

            if (_employees.Contains(employee))
                return;

            var db_item = Get(employee.Id);
            if (db_item is null)
                return;

            db_item.SetValuesFrom(employee);
        }

        public bool Delete(int id)
        {
            var employee = Get(id);
            if(employee is null) throw new ArgumentNullException(nameof(employee));
            return _employees.Remove(employee);
        }
    }
}
