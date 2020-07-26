using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChoiceA.Models
{
    public class CheckedModel
    {
        public Discipline Discipline { get; set; }
        public bool IsChecked { get; set; }
        public CheckedModel()
        {

        }
        public CheckedModel(Discipline discipline, bool _IsChecked)
        {
            Discipline = discipline;
            IsChecked = _IsChecked;
        }
    }
}
