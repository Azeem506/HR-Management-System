using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR_Management_System.Models
{
    public class SalaryViewmodell
    {
        public int SalaryId { get; set; }
        
        public string Date { get; set; }
  
        public Nullable<System.TimeSpan> Time { get; set; }
        public Nullable<double> Basic { get; set; }
        public Nullable<double> Allowance { get; set; }
        public Nullable<double> Medical { get; set; }
        public Nullable<double> Tax { get; set; }
        public Nullable<double> LabourWelfare { get; set; }
        public Nullable<double> Fund { get; set; }
        public Nullable<double> NetSalary { get; set; }
        public Nullable<int> CmpId { get; set; }
        public Nullable<int> DeptId { get; set; }
        public Nullable<int> EmpId { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> RoleId { get; set; }
        public string C_Name { get; set; }
        public string D_Name { get; set; }
        public string E_Name { get; set; }
        public string E_Image { get; set; }

        public string RType { get; set; }


        //public string U_Id { get; set; }




    }
}