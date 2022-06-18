using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rwaLib.Models
{
    [Serializable]
    public class TagType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }

        public TagType()
        {

        }

        public TagType(string name, string nameEng)
        {
            Name = name;
            NameEng = nameEng;
        }
    }
}
