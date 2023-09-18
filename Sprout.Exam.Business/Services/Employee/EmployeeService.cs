using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprout.Exam.DataAccess.Interfaces.UnitOfWork;
using Sprout.Exam.DataAccess.Models;
using Sprout.Exam.Common;


namespace Sprout.Exam.Business.Services.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmployeeFactory _employeeFactory;
        public EmployeeService(IUnitOfWork unitOfWork, IEmployeeFactory employeeFactory)
        {
            _unitOfWork = unitOfWork;
            _employeeFactory = employeeFactory;
        }

        public async Task<IEnumerable<DataAccess.Models.Employee>> GetAll()
        {
            return await _unitOfWork.Employees.GetAllAsync();
        }

        public async Task Add(DataAccess.Models.Employee employee)
        {
            var res = _unitOfWork.Employees.AddAsync(employee);
            await _unitOfWork.CompleteAsync();
        }

        public decimal CalculateSalary(Common.Enums.EmployeeType employeeType, decimal days)
        {
            var empFactory = _employeeFactory.GetEmployee(Common.Enums.EmployeeType.Regular);
            return empFactory.CalculateSalary(days); 
        }
    }
}
