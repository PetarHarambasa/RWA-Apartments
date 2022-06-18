using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rwaLib.Models
{   
    [Serializable]
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public int TypeId { get; set; }

        public Tag()
        {

        }

        public Tag(string name, string nameEng, int typeId)
        {
            TypeId = typeId;
            Name = name;
            NameEng = nameEng;
        }
    }
}
