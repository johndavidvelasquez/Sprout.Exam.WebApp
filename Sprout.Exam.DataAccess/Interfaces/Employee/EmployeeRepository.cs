using Sprout.Exam.DataAccess.Interfaces.GenericRepository;
using Sprout.Exam.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Interfaces.Employee
{
    public class EmployeeRepository : GenericRepository<Models.Employee>, IEmployeeRepository
    {
        public EmployeeRepository(SproutExamDbContext context) : base(context)
        {

        }
    }
}
