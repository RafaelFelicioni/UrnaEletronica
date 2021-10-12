using APIUrnaEletronica.Data;
using APIUrnaEletronica.DTO.DTOVotos;
using APIUrnaEletronica.Models;
using APIUrnaEletronica.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIUrnaEletronica.Repository
{
    public class VotoRepository : IVotoRepository
    {
        private APIUrnaContext _apiUrnaContext { get; set; }

        public VotoRepository(APIUrnaContext apiUrnaContext)
        {
            _apiUrnaContext = apiUrnaContext;
        }

        public Task<string> RegistrarVoto(DTORegistroVotos idCandidato)
        {
            _apiUrnaContext.Votos.Add(new Votos()
            {
                DataVoto = DateTime.Now,
                IdCandidato = idCandidato.IdCandidato
            });

            _apiUrnaContext.SaveChanges();

            return Task.FromResult("Sucesso: voto registrado com sucesso!");
        }

        public Task<List<DTORetornoVotos>> RetornarQuantidadeVotos()
        {
            var retornoVotosDB = _apiUrnaContext.Votos.ToList();
            var retornoCandidatoDB = _apiUrnaContext.Candidatos.ToList();
            if (retornoVotosDB.Count == 0 && retornoCandidatoDB.Count == 0)
            {
                throw new Exception("Erro: nenhum candidato registrado");
            }

            var idsRetornados = new List<int>();
            var retorno = new List<DTORetornoVotos>();
            var candidatoComVotos = new List<Candidatos>();
            var candidatosSemVotos = new List<Candidatos>();
            var canidadatoComVotosIds = new List<int>();

            if (retornoVotosDB.Count > 0)
            {
                foreach (var voto in retornoVotosDB)
                {
                    var candidatoComVotoValidacao = retornoCandidatoDB.Where(x => x.IdCandidato == voto.IdCandidato).FirstOrDefault();
                   
                    if (candidatoComVotoValidacao != null) {
                        candidatoComVotos.Add(candidatoComVotoValidacao);
                    }
                }

                var votosNulos = retornoVotosDB.Where(x => x.IdCandidato == 0).Count();
                if (votosNulos > 0)
                {
                    retorno.Add(new DTORetornoVotos()
                    {
                        NomeCandidato = "Votos Nulos",
                        QuantidadeVotos = votosNulos
                    });
                }
            }
           
            if (candidatoComVotos.Count > 0)
            {
                canidadatoComVotosIds = candidatoComVotos.Select(x => x.IdCandidato).ToList();
                foreach (var item in candidatoComVotos)
                {
                    if (!idsRetornados.Contains(item.IdCandidato))
                    {
                        retorno.Add(new DTORetornoVotos()
                        {
                            Legenda = item.Legenda,
                            IdCandidato = item.IdCandidato,
                            NomeCandidato = item.NomeCandidato,
                            NomeVice = item.NomeVice,
                            QuantidadeVotos = retornoVotosDB.Where(x => x.IdCandidato == item.IdCandidato).Count()
                        });
                        idsRetornados.Add(item.IdCandidato);
                    }
                }
            }

            var candidatoSemVoto = retornoCandidatoDB.Where(x => !canidadatoComVotosIds.Contains(x.IdCandidato)).ToList();
            if (candidatoSemVoto.Count > 0)
            {
                foreach (var item in candidatoSemVoto)
                {
                    if (!idsRetornados.Contains(item.IdCandidato))
                    {
                        retorno.Add(new DTORetornoVotos()
                        {
                            Legenda = item.Legenda,
                            IdCandidato = item.IdCandidato,
                            NomeCandidato = item.NomeCandidato,
                            NomeVice = item.NomeVice,
                            QuantidadeVotos = 0
                        });
                        idsRetornados.Add(item.IdCandidato);
                    }
                }
            }

            var retornoDistinct = retorno.Distinct().OrderByDescending(x => x.QuantidadeVotos).ToList();
            return Task.FromResult(retornoDistinct);
        }
    }
}
