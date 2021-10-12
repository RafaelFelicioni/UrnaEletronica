using APIUrnaEletronica.Data;
using APIUrnaEletronica.DTO.DTOCandidatos;
using APIUrnaEletronica.Models;
using APIUrnaEletronica.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIUrnaEletronica.Repository
{
    public class CandidatoRepository : ICandidatoRepository
    {
        private APIUrnaContext _apiUrnaContext { get; set; }

        public CandidatoRepository(APIUrnaContext apiUrnaContext)
        {
            _apiUrnaContext = apiUrnaContext;
        }

        public Task<string> DeletarCandidato(int idCandidato)
        {
            var buscaCandidato = _apiUrnaContext.Candidatos.Where(x => x.IdCandidato == idCandidato).FirstOrDefault();

            if (buscaCandidato == null) 
            {
                throw new Exception("Erro: candidato informado não existe no banco de dados");
            }

            _apiUrnaContext.Candidatos.Remove(buscaCandidato);
            _apiUrnaContext.SaveChanges();
            
            return Task.FromResult("Sucesso: candidato deletado com sucesso!");
        }

        public Task<string> RegistrarCandidato(DTORegistroCandidato candidato)
        {
            var buscaCandidatoPorNome = _apiUrnaContext.Candidatos.Where(x => x.NomeCandidato == candidato.NomeCandidato).FirstOrDefault();
            var buscaCandidatoPorCodigo = _apiUrnaContext.Candidatos.Where(x => x.CodigoCandidato == candidato.CodigoCandidato).FirstOrDefault();

            if (buscaCandidatoPorNome != null)
            {
                throw new Exception("Erro: candidato informado já existe no banco de dados, mude o nome e tente novamente.");
            }
            else if (buscaCandidatoPorCodigo != null)
            {
                throw new Exception("Erro: candidato informado já existe no banco de dados, mude o código e tente novamente.");
            }
            else
            {
                _apiUrnaContext.Candidatos.Add(new Candidatos()
                {
                    CodigoCandidato = candidato.CodigoCandidato,
                    DataRegistro = DateTime.Now,
                    Legenda = candidato.Legenda,
                    NomeCandidato = candidato.NomeCandidato,
                    NomeVice = candidato.NomeDoVice
                });
                _apiUrnaContext.SaveChanges();
                return Task.FromResult("Sucesso: candidato cadastrado com sucesso.");
            }
        }

        public Task<List<DTORetornoListaCandidatos>> RetornarListaCandidatos()
        {
            var dtoCandidato = new List<DTORetornoListaCandidatos>();
            var buscaCandidato = _apiUrnaContext.Candidatos.ToList();

            if (buscaCandidato.Count == 0) 
            {
                throw new Exception("Erro: não há candidatos cadastrados.");
            }
            foreach (var item in buscaCandidato)
            {
                dtoCandidato.Add(new DTORetornoListaCandidatos()
                {
                    IdCandidato = item.IdCandidato,
                    CodigoCandidato = item.CodigoCandidato,
                    Legenda = item.Legenda,
                    NomeCandidato = item.NomeCandidato,
                    NomeVice = item.NomeVice
                });
            }
            return Task.FromResult(dtoCandidato);
        }
    }
}
