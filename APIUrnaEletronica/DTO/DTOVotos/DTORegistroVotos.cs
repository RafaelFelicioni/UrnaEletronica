using System;

namespace APIUrnaEletronica.DTO.DTOVotos
{
    public class DTORegistroVotos
    {
        public int Id { get; set; }
        public int IdCandidato { get; set; }
        public DateTime DataDoVoto { get; set; }
    }
}
