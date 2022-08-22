using BAL.RegraNegocio;
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
    public class NotaController : Controller
    {
        private NotaRegraNegocio notaRegraNegocio;

        public NotaController(NotaRegraNegocio notaRegraNegocio)
        {
            this.notaRegraNegocio = notaRegraNegocio;
        }

        [HttpGet("Consultar")]
        public async Task<IActionResult> Consultar(int id)
        {
            try
            {
                return Ok(await notaRegraNegocio.ConsultarPorIdAsync(id));


            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("Inserir")]
        public async Task<IActionResult> Inserir(Nota nota)
        {
            try
            {
                if (await notaRegraNegocio.InserirAsync(nota))
                    return Ok("Nota inserida com sucesso.");
                return StatusCode(500, "Ocorreu um erro ao tentar inserir a nota.");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("Editar")]
        public async Task<IActionResult> Editar(Nota nota)
        {
            try
            {
                if (await notaRegraNegocio.AlterarAsync(nota))
                    return Ok("Nota alterada com sucesso.");
                return StatusCode(500, "Ocorreu um erro ao tentar alterar a nota.");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("Deletar")]
        public async Task<IActionResult> Deletar(Nota nota)
        {
            try
            {
                return Ok(await notaRegraNegocio.ExcluirAsync(nota));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> ListarPorGrupo(int grupo)
        {
            try
            {
                return Ok(await notaRegraNegocio.Listar(grupo));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("DemostrativoPatrimonio")]
        public async Task<IActionResult> ListarNotasValidas()
        {
            try
            {
                return Ok(await notaRegraNegocio.DemostrativoPatrimonio());
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
