using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq_anonimous
{
    class Plane
    {
        public int Id { get; set; }
        public int Speed { get; set; }
        public string Marka { get; set; }
        public int ModelYear { get; set; }
        public Category Category { get; set; }
        public int CompanyId { get; set; }
    }


    enum Category
    {
        Cargo,
        Passengers,
        Military,
        Fireplane,

    }
}
