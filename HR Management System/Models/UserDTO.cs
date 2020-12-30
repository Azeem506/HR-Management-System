using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR_Management_System.Models
{
    public class UserDTO
    {
        public string Login { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string CPassword { get; set; }
    }
}