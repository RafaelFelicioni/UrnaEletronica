using APIUrnaEletronica.DTO.DTOCandidatos;
using APIUrnaEletronica.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace APIUrnaEletronica.Controllers
{
    [ApiController]
    public class CandidatoController : ControllerBase
    {
        private readonly ICandidatoRepository _candidatoRepository;

        public CandidatoController(ICandidatoRepository candidatoRepository)
        {
            _candidatoRepository = candidatoRepository;
        }

        [HttpPost("RegistrarCandidato", Name = "RegistrarCandidato")]
        public async Task<IActionResult> RegistrarCandidato([FromBody] DTORegistroCandidato dto)
        {
            try
            {
                var Resposta = await _candidatoRepository.RegistrarCandidato(dto);
                string result = JsonConvert.SerializeObject(Resposta);
                return Ok(result);
            }
            catch (Exception ex)
            {
                string result = JsonConvert.SerializeObject(ex);
                return Ok(result);
            }
        }

        [HttpDelete("DeletarCandidato", Name = "DeletarCandidato")]
        public async Task<IActionResult> DeletarCandidato([FromBody] DTODeletarCandidato dto)
        {
            try
            {
                var idCandidato = dto.IdCandidato;
                var resposta = await _candidatoRepository.DeletarCandidato(idCandidato);
                string result = JsonConvert.SerializeObject(resposta);
                return Ok(result);
            }
            catch (Exception ex)  
            {
                string result = JsonConvert.SerializeObject(ex);
                return Ok(result);
            }
        }

        [HttpGet("ObterCandidatos", Name = "ObterCandidatos")]
        public async Task<IActionResult> ObterCandidatos()
        {
            try
            {
                var dto = await _candidatoRepository.RetornarListaCandidatos();
                string result = JsonConvert.SerializeObject(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                string result = JsonConvert.SerializeObject(ex);
                return Ok(result);
            }
        }
    }
}