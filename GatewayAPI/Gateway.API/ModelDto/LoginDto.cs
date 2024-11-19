using System.ComponentModel.DataAnnotations;

namespace Gateway.API.ModelDto
{
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
