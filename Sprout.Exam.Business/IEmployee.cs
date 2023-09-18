using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business
{
    public interface IEmployee
    {
        decimal CalculateSalary(decimal absentDays, decimal workedDays);
    }
}
