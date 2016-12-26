using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Containers;

namespace WebApplication1.Models
{
    public class Administrator : IAdministrator
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Title { get; set; }

        public double Salary { get; set; }
    }
}