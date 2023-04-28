using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_web_service1.Models
{
    [Table("follows", Schema = "api-web_service1")]
    public class Follow
    {
        public int Id_utente { get; set; }
        public int Id_utente_seguito { get; set; }
    }
}
