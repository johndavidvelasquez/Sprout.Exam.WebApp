using Sprout.Exam.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business
{
    public class EmployeeFactory : IEmployeeFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public EmployeeFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IEmployee GetEmployee(EmployeeType employeeType)
        {
            switch (employeeType)
            {
                case EmployeeType.Regular:
                    return (IEmployee)_serviceProvider.GetService(typeof(RegularEmployee));
                case EmployeeType.Contractual:
                    return (IEmployee)_serviceProvider.GetService(typeof(ContractualEmployee)); 
                default:
                    throw new ApplicationException(string.Format("EmployeeType '{0}' cannot be created", employeeType));

            }
        }
    }
}

