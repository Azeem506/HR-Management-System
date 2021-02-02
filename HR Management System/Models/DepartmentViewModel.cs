using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR_Management_System.Models
{
    public class DepartmentViewModel
    {
        public int DeptId { get; set; }
       
        public string DeptName { get; set; }
        public Nullable<int> CmpId { get; set; }
        public Nullable<int> UserId { get; set; }
        public string CmpName { get; set; }
    }
}