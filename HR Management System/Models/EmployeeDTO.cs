using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR_Management_System.Models
{
    public class EmployeeDTO
    {
        public int EmpId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        //public DateTime JoiningDate { get; set; }
    }
}