using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business
{
    public class RegularEmployee : IEmployee
    {
        private decimal Tax = 0.12m;
        private decimal BasicSalary = 20000; // Monthly
        public decimal CalculateSalary(decimal days)
        {
            decimal daysWorked = 23 - days;
            return Math.Round(this.BasicSalary - (this.BasicSalary / daysWorked) - (this.BasicSalary * this.Tax), 2);
        }
    }
}
