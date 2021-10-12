using System.Collections.Generic;

namespace APIUrnaEletronica.DTO.DTOVotos
{
    public class DTORetornoVotos
    {
        public string NomeCandidato { get; set; }
        public int IdCandidato { get; set; }
        public string NomeVice { get; set; }
        public string Legenda { get; set; }
        public int QuantidadeVotos { get; set; }
    }
}
