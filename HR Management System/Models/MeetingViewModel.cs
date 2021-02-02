using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR_Management_System.Models
{
    public class MeetingViewModel
    {
        public int MeetingId { get; set; }
        public string CmpName { get; set; }
        public string DeptName { get; set; }
        public string CEO { get; set; }


        [DisplayName("Meeting Date")]
        [Required(ErrorMessage = "Employee Joining Time is required!")]
        
        public string Date { get; set; }

        [DisplayName("Meeting Time")]
        [Required(ErrorMessage = "Employee Joining Time is required!")]
        [DataType(DataType.Time)]
        public Nullable<System.TimeSpan> Time { get; set; }
        public string Message { get; set; }
        public Nullable<int> CmpId { get; set; }
        public Nullable<int> DeptId { get; set; }
        public Nullable<int> EmpId { get; set; }
        public Nullable<int> UserId { get; set; }
        public string C_Name { get; set; }
        public string D_Name { get; set; }
        public string E_Name { get; set; }
        public string E_Image { get; set; }

    }
}