using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Containers;

namespace WebApplication1.Models
{
    public class Teacher : ITeacher
    {
        //IAdministrator _adm;
        //public Teacher(IAdministrator tdm)
        //{
        //    _adm = tdm;
        //}
        public string FirstName
        {
            get
            {
                return name1;
            }

            set
            {
                name1 = "David";
            }
        }

        public string LastName { get; set; }

        public string Title { get; set; }

        public double Salary { get; set; }


        string name1;
        string name2 = "Dryer";
        string position = "Principal";
        double money = 100.00;

       
    }
}