using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proyecto_AccionLaboral.Models
{
    public class KnownLanguage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  
        public int KnownLanguageId { get; set; }
        public double Percentage { get; set; }
        public int LanguageId { get; set; }
        public int LanguageLevelId { get; set; }
        public int ClientId { get; set; }

        public Language Language { get; set; }
        public LanguageLevel LanguageLevel { get; set; }
        public Client Client { get; set; }
        
    }
}
