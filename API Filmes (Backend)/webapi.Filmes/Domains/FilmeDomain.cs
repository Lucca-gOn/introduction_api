using System.ComponentModel.DataAnnotations;

namespace webapi.Filmes.Domains
{

    /// <summary>
    /// CLasse que representa a entidade (Tabela) Filmes
    /// </summary>
    public class FilmeDomain
    {
        public int IdFilme { get; set; }
        public int IdGenero { get; set; }

        [Required(ErrorMessage = "O titulo do filme é obrigatório!")]
        public string? Titulo { get; set; }
        


        //Refenrecia para a classe genero
        public GeneroDomain Genero { get; set; }

    }
}
