using BAL.RegraNegocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UsuarioController(UserManager<IdentityUser> userManager,
           SignInManager<IdentityUser> signInManager,
           RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        [HttpPost("Login")]
        public async Task<ObjectResult> Post(UsuarioViewModel usuarioViewModel)
        {

            // Recupera o usuário
            var result = await this.signInManager.PasswordSignInAsync(usuarioViewModel.Login, usuarioViewModel.Senha, false, false);

            if (result.Succeeded)
            {
                var user = await this.signInManager.UserManager.FindByNameAsync(usuarioViewModel.Login);
                var roles = await userManager.GetRolesAsync(user);
                var token = AutenticacaoRegraNegocio.GenerateToken(user);

                return Ok(new UsuarioViewModel()
                {
                    Permissao = roles.FirstOrDefault(),
                    Token = token
                });
            }
            else
            {
                return StatusCode(500, "Usuário ou senha incorretos");
            }
        }

        [HttpPost("CriarUsuario")]
        [Authorize]
        public async Task<ObjectResult> CriarUsuario(UsuarioViewModel usuarioViewModel)
        {
            try
            {
                if (usuarioViewModel != null)
                {
                    if (!String.IsNullOrWhiteSpace(usuarioViewModel.Login) && !String.IsNullOrWhiteSpace(usuarioViewModel.Email) && !String.IsNullOrWhiteSpace(usuarioViewModel.Senha))
                    {
                        var novoUsuario = new IdentityUser()
                        {
                            UserName = usuarioViewModel.Login,
                            Email = usuarioViewModel.Email
                        };

                        var result = await userManager.CreateAsync(novoUsuario, usuarioViewModel.Senha);
                        if (result.Succeeded)
                        {
                            if (!String.IsNullOrWhiteSpace(usuarioViewModel.Permissao))
                            {
                                var resultRole = await userManager.AddToRoleAsync(novoUsuario, usuarioViewModel.Permissao);
                                if (resultRole.Succeeded)
                                {
                                    return Ok("Usuário cadastradado com sucesso.");
                                }
                            }
                            return Ok("Usuário cadastradado sem permissão.");

                        }

                        if (result.Errors.FirstOrDefault().Code == "DuplicateUserName")
                        {
                            return StatusCode(500, "Ocorreu um erro ao tentar salvar o usuário, esse usuário já existe.");
                        }

                        return StatusCode(500, "Ocorreu um erro ao tentar salvar o usuário, formulário incompleto.");
                    }

                }
                return StatusCode(500, "Não foi possível cadastrar o usuário, formulário em branco.");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("ListarUsuarios")]
        [Authorize]
        public async Task<ActionResult> ListarUsuarios()
        {
            try
            {
                return Ok(userManager.Users.ToList());
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("CriarFuncao")]
        [Authorize]
        public async Task<ActionResult> CriarFuncao(string role)
        {
            try
            {
                return Ok(await roleManager.CreateAsync(new IdentityRole() { Name = role }));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        //[Authorize]
        //[HttpPut("Editar")]
        //public async Task<IActionResult> Editar(UsuarioViewModel usuarioViewModel)
        //{
        //    try
        //    {
        //        if (usuarioViewModel != null)
        //        {
        //            var usuario = await userManager.FindByIdAsync(usuarioViewModel.Id);

        //            if(usuarioViewModel.Email != usuario.Email)
        //                await userManager.ChangeEmailAsync()
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(500);
        //    }
        //}

        [Authorize]
        [HttpDelete("Deletar")]
        public async Task<IActionResult> Deletar(string id)
        {
            try
            {
                var usuario = await userManager.FindByIdAsync(id);
                if (usuario == null)
                    return StatusCode(500, "Usuário não encontrado");
                var result = await userManager.DeleteAsync(usuario);

                if (result.Succeeded)
                    return Ok("Usuário deletado com sucesso.");

                return StatusCode(500, "Ocorreu um erro ao deletar o usuário");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        //[Authorize]
        //[HttpGet("Consultar")]
        //public async Task<IActionResult> Consultar(string id)
        //{
        //    try
        //    {
        //        var usuario = await userManager.FindByIdAsync(id);
        //        var roles = await userManager.GetRolesAsync(usuario);

        //        var retorno = new UsuarioViewModel()
        //        {
        //            Id = usuario.Id,
        //            Email = usuario.Email,
        //            Permissao = roles.FirstOrDefault()
        //        };
        //        return Ok(retorno);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}

    }
}
