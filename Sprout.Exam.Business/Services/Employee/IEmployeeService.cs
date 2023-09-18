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
        Task<IEnumerable<DataAccess.Models.Employee>> GetAll(bool includeDeleted = false);
        Task<DataAccess.Models.Employee> GetById(int employeeId);
        Task<DataAccess.Models.Employee> Add(DataAccess.Models.Employee employee);
        Task<DataAccess.Models.Employee> Edit(DataAccess.Models.Employee employee);
        Task<DataAccess.Models.Employee> Delete(int employeeId);
        decimal CalculateSalary(Common.Enums.EmployeeType employeeType, decimal days);
    }
}
