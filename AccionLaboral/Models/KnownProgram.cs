using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_AccionLaboral.Models
{
    public class KnownProgram
    {
        public int KnownProgramId { get; set; }
        public int Name { get; set;  }
        public int ClientId { get; set; }

        public Client Client { get; set; }
    }
}
