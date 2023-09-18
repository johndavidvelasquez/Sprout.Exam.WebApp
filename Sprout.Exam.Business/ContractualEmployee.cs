using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business
{
    public class ContractualEmployee : IEmployee
    {
        private decimal PerDaySalary = 500; // Monthly
        public decimal CalculateSalary(decimal days)
        {
            return Math.Round(this.PerDaySalary * days, 2);
        }
    }
}
