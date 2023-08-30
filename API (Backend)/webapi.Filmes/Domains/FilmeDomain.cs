using System.ComponentModel.DataAnnotations;

namespace webapi.Filmes.Domains
{
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
