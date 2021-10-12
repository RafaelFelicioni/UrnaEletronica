using APIUrnaEletronica.DTO.DTOVotos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIUrnaEletronica.Repository.Interfaces
{
    public interface IVotoRepository
    {
        Task<string> RegistrarVoto(DTORegistroVotos idCandidato);
        Task<List<DTORetornoVotos>> RetornarQuantidadeVotos();
    }
}
