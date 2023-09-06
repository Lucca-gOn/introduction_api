using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using senai.inlock.webApi.Repositories;
using System.Data;

namespace senai.inlock.webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //Define que o tipo de resposta da API será em formato JSON
    [Produces("application/json")]
    public class JogoController : ControllerBase
    {
        /// <summary>
        /// Objeto _jogoRepository que irá receber todos os métodos definidos na interface IJogoRepository
        /// </summary>
        private IJogoRepository _jogoRepository { get; set; }

        /// <summary>
        /// Instancia o objeto _estudioRepository para que haja referencia aos métodos no repositórios 
        /// </summary>

        public JogoController()
        {
            _jogoRepository = new JogoRepository();
        }

        [HttpGet]
        [Authorize(Roles = "admin,cliente")]
        public IActionResult Get()
        {
            try
            {
                List<JogoDomain> ListaJogo = _jogoRepository.ListarTodos();

                return Ok(ListaJogo);
            }
            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Add(JogoDomain novoJogo)
        {
            try
            {
                _jogoRepository.Cadastrar(novoJogo);

                return StatusCode(201);
            }
            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }
        }
        [HttpDelete]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int id)
        {
            try
            {
                _jogoRepository.Deletar(id);

                return StatusCode(204);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
    }
}
