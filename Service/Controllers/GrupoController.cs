using BAL.RegraNegocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Metadados;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrupoController : Controller
    {
        private GrupoRegraNegocio grupoRegraNegocio;

        public GrupoController(GrupoRegraNegocio grupoRegraNegocio)
        {
            this.grupoRegraNegocio = grupoRegraNegocio;
        }

        [HttpGet("Consultar")]
        public async Task<IActionResult> Consultar(int id)
        {
            try
            {
                var grupo = await grupoRegraNegocio.ConsultarPorIdAsync(id);
                if(grupo == null)
                {
                    return StatusCode(500, "Ocorreu um erro e não foi possível encontrar o grupo informado.");
                }
                return Ok(grupo);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("Inserir")]
        public async Task<IActionResult> Inserir(Grupo grupo)
        {
            try
            {
                if(await grupoRegraNegocio.InserirAsync(grupo))
                    return Ok("Grupo inserido com sucesso.");
                return StatusCode(500, "Ocorreu um erro ao tentar inserir o grupo.");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("Editar")]
        public async Task<IActionResult> Editar(Grupo grupo)
        {
            try
            {
                if (await grupoRegraNegocio.AlterarAsync(grupo))
                    return Ok("Grupo alterado com sucesso.");
                return StatusCode(500, "Ocorreu um erro ao tentar alterar o grupo.");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("Deletar")]
        public async Task<IActionResult> Deletar(Grupo grupo)
        {
            try
            {
                if(await grupoRegraNegocio.ExcluirAsync(grupo)){
                    return Ok("Grupo removido com sucesso.");
                }

                return StatusCode(500, "Ocorreu um erro ao tentar remover o grupo.");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> Listar()
        {
            try
            {
                return Ok(await grupoRegraNegocio.Listar());
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
