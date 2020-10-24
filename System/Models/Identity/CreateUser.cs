using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace System.Models.Identity
{
    public class CreateUser
    {
        [Required]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Display(Name = "Путь")]
        public string RedirectPath { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} должен быть не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        public List<string> Roles { get; set; }
    }
}