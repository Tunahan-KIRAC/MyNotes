using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.BusinessLayer.ValueObject
{
    public class RegisterViewModel
    {
        [DisplayName("Adi"), Required(ErrorMessage = "{0} alani bos gecilemez"), StringLength(30, ErrorMessage = "{0} max {1} karakter olmali")]
        public string Name { get; set; }
        [DisplayName("Soyadi"), Required(ErrorMessage = "{0} alani bos gecilemez"), StringLength(30, ErrorMessage = "{0} max {1} karakter olmali")]
        public string Lastname { get; set; }
        [DisplayName("Kullanici Adi"), Required(ErrorMessage = "{0} alani bos gecilemez"), StringLength(30, ErrorMessage = "{0} max {1} karakter olmali")]
        public string Username { get; set; }
        [DisplayName("Email"), Required(ErrorMessage = "{0} alani bos gecilemez"), StringLength(100, ErrorMessage = "{0} max {1} karakter olmali"), EmailAddress(ErrorMessage = "{0} alani icin gecerli bir email adresi giriniz.")]

        public string Email { get; set; }
        [DisplayName("Sifre"), Required(ErrorMessage = "{0} alani bos gecilemez"), StringLength(30, MinimumLength = 3, ErrorMessage = "{0} max {1} min {2} karakter olmali"), DataType(DataType.Password)]
        public string Password { get; set; }
        [DisplayName("Sifre"), Required(ErrorMessage = "{0} alani bos gecilemez"), StringLength(30, MinimumLength = 3, ErrorMessage = "{0} max {1} min {2} karakter olmali"), DataType(DataType.Password), Compare("Password", ErrorMessage = "{0} ile {1} uyusmuyor...")]
        public string RePassword { get; set; }
    }
}
