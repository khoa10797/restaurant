using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace restaurant.Models
{
    public class LoginModels
    {
        [Required(ErrorMessage = "Mời nhập tên đăng nhập")]
        public string userName { get; set; }

        [Required(ErrorMessage = "Mời nhập mật khẩu")]
        public string passWord { get; set; }

        public bool rememberMe { get; set; }
    }
}