using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace _DbEntities
{
    [Table("Logs")]
    public class Logs
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(), DisplayName("İşlem Tarihi")]
        public DateTime DateTime { get; set; }

        [Column(TypeName = "VARCHAR")]
        [Required, StringLength(20), DisplayName("Kullanıcı Adı")]
        public string UserName { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(100), DisplayName("İşlem")]
        public string ActionName { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(100), DisplayName("Kontrol Adı")]
        public string ControllerName { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(250), DisplayName("Bilgi")]
        public string About { get; set; }

    }
}