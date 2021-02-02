using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR_Management_System.Models
{
    public class CompanyDTO
    {
        public string CmpId{ get; set; }
        public string CmpName { get; set; }
        public string userName { get; set; }
        public string Password { get; set; }

        public string CPassword { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Image { get; set; }


    }
}