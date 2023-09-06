using webapi.Filmes.Domains;

namespace webapi.Filmes.Interfaces
{
    /// <summary>
    /// Interface responsável pelo repositório GeneroRepository
    /// Definir os métodos que serão implementados pelo GeneroRepository
    /// </summary>
    
    public interface IGeneroRepository
    {
        /// <summary>
        /// Cadastrar um novo gênero
        /// </summary>
        /// <param name="novoGenero">Cadastra um novo gênero de filme</param>
        //tipoRetorno NomeMetodo(tipoParâmetro nomeParâmetro) 
        void Cadastrar(GeneroDomain novoGenero);
        
        //-----------------------------------------------------------------------------------------------//

        /// <summary>
        /// Listar todos os objetos cadastrados
        /// </summary>
        /// <returns>Lista com os objetos</returns>
        List<GeneroDomain> ListarTodos();

        //-----------------------------------------------------------------------------------------------//

        /// <summary>
        /// Atualizar um objeto existente passando seu id pelo corpo da requisição
        /// </summary>
        /// <param name="genero">Objeto atualizado (novas informações)</param>
        void AtualizarIdCorpo(GeneroDomain genero);
        //passar o id e o objeto no atualizar url

        //-----------------------------------------------------------------------------------------------//

        /// <summary>
        /// Atualizar objeto existente passando o seu id pela url
        /// </summary>
        /// <param name="id">Id do objeto que será atualizado</param>
        /// <param name="genero">Objeto atualizado(novas informações)</param>
        void AtualizarUrl(int id, GeneroDomain genero);

        //-----------------------------------------------------------------------------------------------//

        /// <summary>
        /// Deletar um objeto
        /// </summary>
        /// <param name="id">Id do objeto que será deletado</param>
        void Deletar(int id);
        //buscar o genero por id

        //-----------------------------------------------------------------------------------------------//

        /// <summary>
        /// Busca um objeto através do seu id
        /// </summary>
        /// <param name="id">Id do objeto a ser buscado</param>
        /// <returns>Objeto buscado</returns>
        GeneroDomain BuscarPorId(int id);
        //-----------------------------------------------------------------------------------------------//
    }
}
