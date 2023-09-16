using Sprout.Exam.DataAccess.Interfaces.Employee;
using Sprout.Exam.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Interfaces.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SproutExamDbContext _context;
        public UnitOfWork(SproutExamDbContext context)
        {
            _context = context;
            Employees = new EmployeeRepository(_context);
        }

        public IEmployeeRepository Employees { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
