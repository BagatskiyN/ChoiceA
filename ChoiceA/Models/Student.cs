using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static ChoiceA.Data.ChoiceContext;

namespace ChoiceA.Models
{
    public class Student
    {
    
        public int Id { get; set; }
     
        [ForeignKey("ApplicationUser")]
        public string Name { set; get; } 
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string Group { set; get; }

        public List<DisciplineStudent> DisciplineStudents { get; set; }

        public Student()
        {

        }
    }
}
