using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Filmes.Domains;
using webapi.Filmes.Interfaces;
using webapi.Filmes.Repositories;

namespace webapi.Filmes.Controllers
{
    //Define que a rota de uma requisição, será no seguinte formato 
    //Dominio/api/nomeController
    //Ex: https://localhost:5000/api/genero
    [Route("api/[controller]")]

    //Define que é um controlador de API
    [ApiController]

    //Define que o tipo de resposta da API será em formato JSON
    [Produces("application/json")]

    //Método controlador que herda da controller base 
    //Onde será criado os Endpoints (Rotas)
    public class GeneroController : ControllerBase
    {
        /// <summary>
        /// Objeto _generoRepository que irá receber todos os métodos definidos na interface IGeneroRepository
        /// </summary>
        private IGeneroRepository _generoRepository { get; set; }

        /// <summary>
        /// Instancia o objeto _generoRepository para que haja referencia aos métodos no repositórios 
        /// </summary>
        public GeneroController()
        {
            _generoRepository = new GeneroRepository();


        }

        /// <summary>
        /// Endpoint que aciona o método ListarTodos no repositório.
        /// </summary>
        /// <returns>A resposta para o usuario(front-end)</returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                //Cria uma lista que recebe os dados da requisição
                List<GeneroDomain> listaGeneros = _generoRepository.ListarTodos();

                //Retonar a lista no formato JSON com o status code ok(200)
                return Ok(listaGeneros);
            }
            catch (Exception erro)
            {
                //Retorna um status code BadRequest(400) e a mensagem do erro 
                return BadRequest(erro.Message);
            }
        }
        //---------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Endpoint que aciona o método de cadastro Gênero 
        /// </summary>
        /// <param name="novoGenero">Objeto recebido na requisição</param>
        /// <returns>A resposta para o usuario(front-end)</returns>
        [HttpPost]
        public IActionResult Post(GeneroDomain novoGenero)
        {

            try
            {
                //Fazendo a chamada para o método cadastrar, passando o objeto como parâmetro;
                _generoRepository.Cadastrar(novoGenero);

                //Retorna um status code 201 - Created
                return StatusCode(201);
            }
            catch (Exception erro)
            {
                //Retorna um status code BadRequest(400) e a mensagem do erro 
                return BadRequest(erro.Message);
            }


        }

        //-------------------------------------------------------------------------------------------------------//

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                _generoRepository.Deletar(id);

                return StatusCode(204);

            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }


        }
        /// <summary>
        /// Endpoint que aciona o método de buscar por id
        /// </summary>
        /// <param name="id">Id do objeto a ser buscado</param>
        /// <returns>Status Code e objeto caso encontrado</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                // Cria um objeto generoBuscado que irá receber o gênero buscado no banco de dados
                GeneroDomain generoBuscado = _generoRepository.BuscarPorId(id);

                // Verifica se nenhum gênero foi encontrado
                if (generoBuscado == null)
                {
                    // Caso não seja encontrado, retorna um status code 404 - Not Found com a mensagem personalizada
                    return NotFound("Nenhum gênero foi encontrado!");
                }

                // Caso seja encontrado, retorna o gênero buscado com um status code 200 - Ok
                return Ok(generoBuscado);
            }
            catch (Exception erro)
            {
                // Retorna um status 400 - BadRequest e o código do erro
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Endpoint que aciona o método atualizar por ID
        /// </summary>
        /// <param name="id">Id do objeto a ser atualizado</param>
        /// <returns>Status Code e objeto caso atualizado</returns>
        
        [HttpPut]
        public IActionResult UpdateById(GeneroDomain genero)
        {

            try
            {
                GeneroDomain generoBuscado = _generoRepository.BuscarPorId(genero.IdGenero);

                if (generoBuscado == null)
                {
                    return NotFound("Gênero não encontrado!");
                }

                _generoRepository.AtualizarIdCorpo(genero);

                return Ok("Genero Atualizado");
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }


        /// <summary>
        /// Endpoint que aciona o método atualizar por URL
        /// </summary>
        /// <param name="id">Id do objeto a ser atualizado</param>
        /// <returns>Status Code e objeto caso atualizado</returns>
        [HttpPatch("{id}")]
        public IActionResult UpdateByUrl(int id, GeneroDomain genero)
        {
            try
            {
                GeneroDomain generoBuscado = _generoRepository.BuscarPorId(id);
                if (generoBuscado == null)
                {
                    return NotFound("Gênero não encontrado!");
                }

                _generoRepository.AtualizarUrl(id,genero);
                return Ok("Genero atualizado");
            }
            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }
        }
    }
}
