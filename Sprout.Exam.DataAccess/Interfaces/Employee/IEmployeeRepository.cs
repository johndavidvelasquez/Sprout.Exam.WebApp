using Sprout.Exam.DataAccess.Interfaces.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Interfaces.Employee
{
    public interface IEmployeeRepository : IGenericRepository<Models.Employee>
    {
    }
}
