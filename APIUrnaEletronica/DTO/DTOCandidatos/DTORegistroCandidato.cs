using System;

namespace APIUrnaEletronica.DTO.DTOCandidatos
{
    public class DTORegistroCandidato
    {
        public int CodigoCandidato { get; set; }
        public string NomeCandidato { get; set; }
        public string NomeDoVice { get; set; }
        public string Legenda { get; set; }
        public DateTime DataRegistro { get; set; }
    }
}
