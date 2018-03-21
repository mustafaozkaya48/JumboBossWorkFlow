using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _DataBaseLayer.Models
{
    [Table("aspnet_Users")]
    public class User
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(),StringLength(25), Column(TypeName = "VARCHAR"), DisplayName("Ad")]
        public string Name { get; set; }
        [Required(), StringLength(25), Column(TypeName = "VARCHAR"), DisplayName("Soyad")]
        public string SurName  { get; set; }

        [Required(), StringLength(60), Column(TypeName = "VARCHAR"), DisplayName("E-Posta")]
        public string EMail { get; set; }

        [Required(), StringLength(16), Column(TypeName = "VARCHAR"), DisplayName("Şifre")]
        public string Password { get; set; }

        [Required(), StringLength(11), Column(TypeName = "VARCHAR"), DisplayName("Telefon Numarası")]
        public string PhoneNumber { get; set; }

        public bool CheckMail { get; set; }
        public bool ChenckPhoneNumber { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(350)]
        public string About { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(350)]
        public string CompanyInfo { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(350)]
        public string Address { get; set; }

        [Column(TypeName = "VARCHAR")]
        public string ProfilPicture { get; set; }

    }
}

