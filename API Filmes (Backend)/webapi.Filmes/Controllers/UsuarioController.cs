using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using webapi.Filmes.Domains;
using webapi.Filmes.Interfaces;
using webapi.Filmes.Repositories;

namespace webapi.Filmes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository;

        public UsuarioController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        [HttpPost]
        public IActionResult Login(UsuarioDomain usuario)
        {

            try
            {
                UsuarioDomain usuarioBuscado = _usuarioRepository.Login(usuario.Email, usuario.Senha);
                if (usuarioBuscado == null)
                {
                    return NotFound("Email ou senha inválidos!");
                }

                //Caso encontre o usuario, prossegue para a criação do token

                //1º- Definir as informações (claims) que serão fornecidos no token (PAYLOAD)
                var claims = new[]
                {
                    //Formato da Claim
                    //JTI serve para a identificação de ID (identificador)
                    new Claim(JwtRegisteredClaimNames.Jti,usuarioBuscado.IdUsuario.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email,usuarioBuscado.Email),
                    new Claim(ClaimTypes.Role, usuarioBuscado.Permissao),

                    //Existe a possibilidade de criar uma claim personalizada
                    new Claim("Claim Personalizada", "Valor da Claim Personalizada")
                };

                //2º- Definir a chave de acesso ao token
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("filmes-chave-autenticacao-webapi-dev"));

                //3º- Definir as credenciais do token (HEADER)
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                //4º- Gerar o token 
                var token = new JwtSecurityToken
                (
                    //emissor do token (ver em program)
                    issuer:"webapi.Filmes",

                    //Destinatario do token
                    audience:"webapi.Filmes",

                    //Dados definidos nas claims(informações)
                    claims : claims,

                    //tempo de expiração
                    expires: DateTime.Now.AddMinutes(5),

                    //credenciais token
                    signingCredentials: creds


                );
                    
                //5º - Retornar o token criado
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }
        }
    }
}
