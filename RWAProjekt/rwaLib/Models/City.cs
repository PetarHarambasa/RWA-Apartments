using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rwaLib.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public City()
        {

        }
        public City(string name)
        {
            Name = name;
        }
    }
}
