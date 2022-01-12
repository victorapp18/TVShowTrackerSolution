using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVShowTracker.Webapi.Application.ViewModels.Authenticate
{
    public class ExternalLoginModel
    {
        public string Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public string Full_Name { get; set; }
        public string Photo_Profile { get; set; }
    }   
}