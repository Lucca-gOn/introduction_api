using webapi.Filmes.Domains;

namespace webapi.Filmes.Interfaces
{
    public interface IFilmeRepository
    {
        /// <summary>
        /// Interface responsável pelo repositório FilmeRepository
        /// Definir os métodos que serão implementados pelo FilmeRepository
        /// </summary>
        void Cadastrar(FilmeDomain novoFilme);

        //-----------------------------------------------------------------------------------------------//

        /// <summary>
        /// Listar todos os objetos cadastrados
        /// </summary>
        /// <returns>Lista com os objetos</returns>
        List<FilmeDomain> ListarTodos();

        //-----------------------------------------------------------------------------------------------//

        /// <summary>
        /// Atualizar um objeto existente passando seu id pelo corpo da requisição
        /// </summary>
        /// <param name="genero">Objeto atualizado (novas informações)</param>
        void AtualizarIdCorpo(FilmeDomain filme);
        //passar o id e o objeto no atualizar url

        //-----------------------------------------------------------------------------------------------//

        /// <summary>
        /// Atualizar objeto existente passando o seu id pela url
        /// </summary>
        /// <param name="id">Id do objeto que será atualizado</param>
        /// <param name="genero">Objeto atualizado(novas informações)</param>
        void AtualizarUrl(int id, FilmeDomain filme);

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
        FilmeDomain BuscarPorId(int id);
        //-----------------------------------------------------------------------------------------------//

    }
}
