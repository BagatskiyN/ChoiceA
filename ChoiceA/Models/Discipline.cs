using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ChoiceA.Data.ChoiceContext;

namespace ChoiceA.Models
{
    public class Discipline 
    {
    public string Id { get; set; }
        public string Title { set; get; }
        public string Annotation { set; get; }
        //
        public string TeacherId { get; set; }
        public Teacher Teacher { set; get; }

        public List<DisciplineStudent> DisciplineStudents { get; set; }
        public Discipline()
        {

        }
    }
}
