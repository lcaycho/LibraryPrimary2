using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryPrimary2.Models
{
    [Table("t_contacto")]

    public class Contacto
    {   
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id {get; set;}      
        public string? Nombre {get; set;}
        public string? Email {get; set;}
        public string? Telefono {get; set;}
        public string? Pregunta {get; set;}
    }
}