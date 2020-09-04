using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChoiceA.Models
{
    public class Message
    {
        public int Id { set; get; }
        [Required]
        public string Text { set; get; }
        public DateTime When { set; get; }
        public string Sign { set; get; }
    }
}
