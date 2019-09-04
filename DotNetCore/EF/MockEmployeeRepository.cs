using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.EF
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;

        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>() {
                new Employee()
                {
                    Department = Dept.IT,
                    Id =1,Name="Selvan1",
                    Email ="asdas@asds.com"
                },
                new Employee()
                {
                    Department = Dept.IT,
                    Id =2,Name="Selvan2",
                    Email ="asdas@asds.com"
                },
                new Employee()
                {
                    Department = Dept.IT,
                    Id =3,Name="Selvan3",
                    Email ="asdas@asds.com"
                }

            };
        }

        public Employee Add(Employee employee)
        {
            employee.Id = _employeeList.Max(e => e.Id) + 1;
            _employeeList.Add(employee);
            return employee;
        }

        public Employee Delete(int Id)
        {
            Employee employee = _employeeList.FirstOrDefault(e => e.Id == Id);
            if (employee != null)
            {
                _employeeList.Remove(employee);
            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int Id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == Id);
        }

        public Employee Update(Employee employeeChanges)
        {
            Employee employee = _employeeList.FirstOrDefault(e => e.Id == employeeChanges.Id);
            if (employee != null)
            {
                employee.Name = employeeChanges.Name;
                employee.Email = employeeChanges.Email;
                employee.Department = employeeChanges.Department;
            }
            return employee;
        }
    }
}
