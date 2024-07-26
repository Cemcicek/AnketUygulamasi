using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Admin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
    }
    public class AdminLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class AdminRegisterDto
    {
        public string Name { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
    }
}
