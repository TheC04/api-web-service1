using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_web_service1.Models
{
    [Table("users", Schema = "api-web_service1")]
    public class User
    {
        public int Id { get; set; }
        public string NomeUtente { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
    }
}
