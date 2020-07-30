using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChoiceA.Models
{
    public class Teacher
    {
        public int Id { get; set; }


        [ForeignKey("ApplicationUser")]
        public string Name { set; get; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public List<Discipline> Disciplines { get; set; }
    }
}
