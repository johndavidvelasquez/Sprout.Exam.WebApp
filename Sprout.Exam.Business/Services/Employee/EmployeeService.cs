using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprout.Exam.DataAccess.Interfaces.UnitOfWork;
using Sprout.Exam.DataAccess.Models;


namespace Sprout.Exam.Business.Services.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
    }
}
