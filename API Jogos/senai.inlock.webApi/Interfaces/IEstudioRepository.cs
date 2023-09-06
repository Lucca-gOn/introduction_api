using senai.inlock.webApi.Domains;

namespace senai.inlock.webApi.Interfaces
{
    public interface IEstudioRepository
    {
        /// <summary>
        /// Interface responsável pelo repositório EstudioRepository
        /// Definir os métodos que serão implementados pelo EstudioRepository
        /// </summary>
        void Cadastrar(EstudioDomain novoEstudio);
        
        /// <summary>
        /// Listar todos os objetos Estudio cadastrados
        /// </summary>
        /// <returns>Lista com os objetos</returns>
        List<EstudioDomain> ListarTodos();
       
        /// <summary>
        /// Deletar um objeto Estudio
        /// </summary>
        /// <param name="id">Id do objeto que será deletado</param>
        void Deletar(int id);
    }
}
