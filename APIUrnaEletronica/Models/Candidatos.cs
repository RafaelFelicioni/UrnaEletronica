using System;
using System.ComponentModel.DataAnnotations;

namespace APIUrnaEletronica.Models
{
    public class Candidatos
    {
        [Key]
        public int IdCandidato { get; set; }
        public int CodigoCandidato { get; set; }
        public string NomeCandidato { get; set; }
        public string NomeVice { get; set; }
        public string Legenda { get; set; }
        public DateTime DataRegistro { get; set; }
    }
}
