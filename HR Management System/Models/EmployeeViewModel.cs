using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR_Management_System.Models
{
    public class EmployeeViewModel
    {
        public int EmpId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Nullable<int> CmpId { get; set; }
        public Nullable<int> DeptId { get; set; }
        public Nullable<int> RoleId { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> SalaryId { get; set; }
        public string EmpImage { get; set; }
        
        public Nullable<bool> IsHOD { get; set; }
        public string R_Type { get; set; }


    }
}