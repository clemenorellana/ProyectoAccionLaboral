using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_AccionLaboral.Models
{
    public class Country
    {
        public int CountryId { get; set; }
        public string Name { get; set; }

        public ICollection<City> Cities { get; set; }
    }
}