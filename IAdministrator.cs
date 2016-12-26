using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Containers
{
    public interface IAdministrator
    {
        string FirstName { get; set; }

        string LastName { get; set; }

        string Title { get; set; }

        double Salary { get; set; }
    }
}
