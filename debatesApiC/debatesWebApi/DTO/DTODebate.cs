using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace debatesWebApi.DTO
{
    public class DTODebate
    {

        public String Titulo { get; set; }
        public String Tema { get; set; }
        public int Autor { get; set; }
        public bool Estado { get; set; }
        public byte[] ImageByteArray { get; set; }
        public string extensionImage { get; set;}
        public DateTime? FechaPublicacion { get; set; }
        public DateTime? FechaVencimiento { get; set; }
    }
}