using APIUrnaEletronica.DTO.DTOVotos;
using APIUrnaEletronica.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace APIUrnaEletronica.Controllers
{
    [ApiController]
    public class VotoController : ControllerBase
    {
        private readonly IVotoRepository _votoRepository;

        public VotoController(IVotoRepository votoRepository)
        {
            _votoRepository = votoRepository;
        }

        [HttpPost("RegistrarVotos", Name = "RegistrarVotos")]
        public async Task<IActionResult> RegistrarVotos([FromBody] DTORegistroVotos dto)
        {
            try
            {
                var resposta = await _votoRepository.RegistrarVoto(dto);
                var result = JsonConvert.SerializeObject(resposta);

                return  Ok(result);
            }
            catch (Exception ex)
            {
                var result = JsonConvert.SerializeObject(ex);
                return Ok(result);
            }
        }

        [HttpGet("ObterVotos", Name = "ObterVotos")]
        public async Task<IActionResult> ObterVotos()
        {
            try
            {
                var dto = await _votoRepository.RetornarQuantidadeVotos();

                var result = JsonConvert.SerializeObject(dto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                var result = JsonConvert.SerializeObject(ex);
                return Ok(result);
            }
        }
    }
}