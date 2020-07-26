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
        public string Id { get; set; }
        //public virtual ApplicationUser ApplicationUser { get; set; }
        public string Name { set; get; }
        //
        public List<Discipline> Disciplines { get; set; }
    }
}
