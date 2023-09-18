using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprout.Exam.DataAccess.Models;

namespace Sprout.Exam.Business.Services.Employee
{
    public interface IEmployeeService
    {
        Task<IEnumerable<DataAccess.Models.Employee>> GetAll();
        Task Add(DataAccess.Models.Employee employee);
        decimal CalculateSalary(Common.Enums.EmployeeType employeeType, decimal days);
    }
}
