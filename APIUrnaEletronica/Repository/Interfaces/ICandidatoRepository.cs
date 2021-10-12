using APIUrnaEletronica.DTO.DTOCandidatos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIUrnaEletronica.Repository.Interfaces
{
    public interface ICandidatoRepository
    {
        Task<string> RegistrarCandidato(DTORegistroCandidato candidato);
        Task<string> DeletarCandidato(int idCandidato);
        Task<List<DTORetornoListaCandidatos>> RetornarListaCandidatos();
    }
}
