using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.DataAccess.Interfaces.UnitOfWork;
using Sprout.Exam.DataAccess.Models;
using EmployeeType = Sprout.Exam.Common.Enums.EmployeeType;
using Sprout.Exam.Business.Services.Employee;
using AutoMapper;
using Sprout.Exam.Business;
using System.Text.RegularExpressions;

namespace Sprout.Exam.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {

        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        public EmployeesController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var employees = await _employeeService.GetAll();
            var result = _mapper.Map<List<EmployeeDto>>(employees);
            return Ok(result);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _employeeService.GetById(id);
            if(employee == null)
                return NotFound();

            var result = _mapper.Map<EmployeeDto>(employee);
            return Ok(result);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and update changes to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] EditEmployeeDto input)
        {
            if (!new Regex(@"^[A-Za-z ]+$").IsMatch(input.FullName))
            {
                ModelState.AddModelError(nameof(input.FullName), "Numerical and special character values are not allowed in Full Name");
            }

            if (!new Regex(@"^[0-9-]*$").IsMatch(input.Tin))
            {
                ModelState.AddModelError(nameof(input.Tin), "Numerical and special character values are not allowed in TIN");
            }

            if (ValidateAge(input.Birthdate))
            {
                ModelState.AddModelError(nameof(input.Birthdate), $"Employee must be 18 years old or above");
            }

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            Employee employee = _mapper.Map<Employee>(input);

            var updated = await _employeeService.Edit(employee);
            
            if(updated == null)
                return NotFound();

            return Ok(_mapper.Map<EmployeeDto>(updated));
        }

        /// <summary>
        /// Refactor this method to go through proper layers and insert employees to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateEmployeeDto input)
        {
            if (!new Regex(@"^[A-Za-z ]+$").IsMatch(input.FullName))
            {
                ModelState.AddModelError(nameof(input.FullName), "Numerical and special character values are not allowed in Full Name");
            }

            if (!new Regex(@"^[0-9-]*$").IsMatch(input.Tin))
            {
                ModelState.AddModelError(nameof(input.Tin), "Numerical and special character values are not allowed in TIN");
            }

            if (ValidateAge(input.Birthdate))
            {
                ModelState.AddModelError(nameof(input.Birthdate), $"Employee must be 18 years old or above");
            }

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }


            Employee newEmployee = new Employee();
            newEmployee = _mapper.Map<Employee>(input);

            await _employeeService.Add(newEmployee);

            return Created($"/api/employees/{newEmployee.Id}", newEmployee.Id);
        }


        /// <summary>
        /// Refactor this method to go through proper layers and perform soft deletion of an employee to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var delete = await _employeeService.Delete(id);

            if (delete == null)
                return NotFound();

            return Ok(id);
        }



        /// <summary>
        /// Refactor this method to go through proper layers and use Factory pattern
        /// </summary>
        /// <param name="id"></param>
        /// <param name="absentDays"></param>
        /// <param name="workedDays"></param>
        /// <returns></returns>
        [HttpPost("{id}/calculate")]
        public async Task<IActionResult> Calculate(CalculateSalaryDto input)
        {
            var employee = await _employeeService.GetById(input.Id);
            if (employee == null)
                return NotFound();

            var type = (EmployeeType) employee.EmployeeTypeId;

            var salary = _employeeService.CalculateSalary(type, input.absentDays, input.workedDays);

            return Ok(salary);

        }


        private bool ValidateAge(DateTime dateOfBirth)
        {
            if (dateOfBirth == null)
                return false;   
            DateTime today = DateTime.Today;
            int age = today.Year - dateOfBirth.Year;
            if (dateOfBirth > today.AddYears(-age))
                age--;

            return age >= 18 ? false : true;
        }

    }
}
