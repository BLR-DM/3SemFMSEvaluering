using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMSUserService.Domain
{
    public class Class
    {
        public Class()
        {
            
        }
        public Class(string userId)
        {
            AppUserId = userId;
        }
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
