using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rwaLib.Models
{
    public class ApartmentStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }

        public ApartmentStatus()
        {

        }
        public ApartmentStatus(string name, string nameEng)
        {
            Name = name;
            NameEng = nameEng;
        }
    }
}
