using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_web_service1.Models
{
    [Table("post", Schema = "api-web_service1")]
    public class Post
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public int user_Id { get; set; }
    }
}
