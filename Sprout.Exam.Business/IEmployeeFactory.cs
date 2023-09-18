using Sprout.Exam.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business
{
    public interface IEmployeeFactory
    {
        IEmployee GetEmployee(EmployeeType employeeType);
    }
}
