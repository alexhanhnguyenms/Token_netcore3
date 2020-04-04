using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Token_netcore3.Models
{
    /// <summary>
    /// Đối tượng request từ client lên
    /// </summary>
    public class AuthenticateModel
    {
        /// <summary>
        /// Tài khoản
        /// </summary>
        [Required]
        public string Username { get; set; }
        /// <summary>
        /// mật khẩu
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
