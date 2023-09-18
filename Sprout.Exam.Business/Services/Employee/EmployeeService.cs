using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprout.Exam.DataAccess.Interfaces.UnitOfWork;
using Sprout.Exam.DataAccess.Models;
using Sprout.Exam.Common;
using System.Text.RegularExpressions;

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

        public async Task<IEnumerable<DataAccess.Models.Employee>> GetAll(bool includeDeleted = false)
        {
            var result = await _unitOfWork.Employees.GetAllAsync();
            if(!includeDeleted)
                result = result.Where(x => x.IsDeleted == false).ToList();

            return result;
        }

        public async Task<DataAccess.Models.Employee> GetById(int employeeId)
        {
            return await _unitOfWork.Employees.GetByIdAsync(employeeId);
        }

        public async Task<DataAccess.Models.Employee> Add(DataAccess.Models.Employee employee)
        {
            var newEmployee = await _unitOfWork.Employees.AddAsync(employee);
            await _unitOfWork.CompleteAsync();

            return newEmployee;
        }

        public async Task<DataAccess.Models.Employee> Edit(DataAccess.Models.Employee employee)
        {
            var employeeToUpdate = await _unitOfWork.Employees.GetByIdAsync(employee.Id);
            if (employeeToUpdate != null)
            {
                employeeToUpdate.FullName = employee.FullName;
                employeeToUpdate.Birthdate = employee.Birthdate;
                employeeToUpdate.EmployeeTypeId = employee.EmployeeTypeId;
                employeeToUpdate.Tin = employee.Tin;
                await _unitOfWork.CompleteAsync();
            }

            return employeeToUpdate;
        }

        public async Task<DataAccess.Models.Employee> Delete(int employeeId)
        {
            var employeeToDelete = await _unitOfWork.Employees.GetByIdAsync(employeeId);
            if (employeeToDelete != null)
            {
                employeeToDelete.IsDeleted = true;
                await _unitOfWork.CompleteAsync();
            }

            return employeeToDelete;
        }

        public decimal CalculateSalary(Common.Enums.EmployeeType employeeType, decimal absentDays, decimal workedDays)
        {
            var empFactory = _employeeFactory.GetEmployee(employeeType);
            return empFactory.CalculateSalary(absentDays, workedDays); 
        }
    }
}
