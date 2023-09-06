using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using senai.inlock.webApi.Repositories;

namespace senai.inlock.webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //Define que o tipo de resposta da API será em formato JSON
    [Produces("application/json")]

    //Método controlador que herda da controller base 
    //Onde será criado os Endpoints (Rotas)
    public class EstudioController : ControllerBase
    {
        /// <summary>
        /// Objeto _estudioRepository que irá receber todos os métodos definidos na interface IEstudioRepository
        /// </summary>
        private IEstudioRepository _estudioRepository {  get; set; }

        /// <summary>
        /// Instancia o objeto _estudioRepository para que haja referencia aos métodos no repositórios 
        /// </summary>
        
        public EstudioController() 
        { 
            _estudioRepository = new EstudioRepository();
        }

        [HttpGet]
        public IActionResult Get ()
        {
            try
            {
                List<EstudioDomain> ListaEstudios = _estudioRepository.ListarTodos();

                return Ok(ListaEstudios);
            }
            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }
        }

        [HttpPost]
        public IActionResult Add (EstudioDomain novoEstudio)
        {
            try
            {
                _estudioRepository.Cadastrar(novoEstudio);

                return StatusCode(201);
            }
            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }
        }

        [HttpDelete]
        public IActionResult Delete (int id) 
        {
            try
            {
                _estudioRepository.Deletar(id);

                return StatusCode(204);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
    }
}
