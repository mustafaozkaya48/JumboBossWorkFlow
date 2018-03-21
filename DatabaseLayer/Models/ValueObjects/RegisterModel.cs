using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _DataBaseLayer.Models.ValueObjects
{
     public class RegisterModel
    {
        [Required, StringLength(30), DisplayName("Ad")]
        public string Name { get; set; }
        [Required, StringLength(30), DisplayName("Soyad")]
        public string Surname { get; set; }
        [Required, StringLength(60), DataType(DataType.EmailAddress), EmailAddress(ErrorMessage = "Lütfen geçerli bir E-Posta giriniz")]
        public string EMail { get; set; }
        [Required, StringLength(14, ErrorMessage = "Telefon Numarası alanı uzunluğu hane 11 olan bir dize olmalıdır."), DisplayName("Telefon Numarası")]
        public string PhoneNumber { get; set; }
        [Required, StringLength(16), MinLength(6, ErrorMessage = "Şifreniz en az 6 karaktere sahip olmalıdır"), DataType(DataType.Password),DisplayName("Şifre")]
        public string Password { get; set; }
        [Required, DisplayName("Şifre Tekrar"), StringLength(16), MinLength(6, ErrorMessage = "Şifreniz en az 6 karaktere sahip olmalıdır"), DataType(DataType.Password), Compare("Password", ErrorMessage = "Şifreler aynı değil, lütfen kontrol ediniz.")]
        public string ConfirmPassword { get; set; }
    }
}
