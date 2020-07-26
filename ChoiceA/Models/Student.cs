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
    
        public string Id { get; set; }
        //public virtual ApplicationUser ApplicationUser { get; set; }
        public string Name { set; get; }
        public string Group { set; get; }

        public List<DisciplineStudent> DisciplineStudents { get; set; }

        public Student()
        {

        }
    }
}
