using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementCollection.Models.ViewModels
{
    public class LoginModel
    { 
        public string Email { get; set; }

        public string Password { get; set; }

        public bool error { get; set; }
    }
}
