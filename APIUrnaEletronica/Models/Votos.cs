using System;
using System.ComponentModel.DataAnnotations;

namespace APIUrnaEletronica.Models
{
    public class Votos
    {
        [Key]
        public int Id { get; set; }
        public int IdCandidato { get; set; }
        public DateTime DataVoto { get; set; }
    }
}
