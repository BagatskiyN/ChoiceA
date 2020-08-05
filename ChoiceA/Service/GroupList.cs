using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChoiceA
{
    public class GroupList
    {
        private static GroupList instance;
        public List<string> Groups { get; private set; }
        private GroupList()
        {
            Groups = new List<string>()
            {  
                "KN1",
                "KN2",
                "KN3"
            };
        }

        public static GroupList getInstance()
        {
            if (instance == null)
                instance = new GroupList();
            return instance;
        }
    }
}
