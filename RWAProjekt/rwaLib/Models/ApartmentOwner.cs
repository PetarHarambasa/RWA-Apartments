using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rwaLib.Models
{
    [Serializable]
    public class ApartmentOwner
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ApartmentOwner()
        {

        }
        public ApartmentOwner(string name)
        {
            Name = name;
        }
    }
}
