using System.ComponentModel.DataAnnotations;

namespace webapi.Filmes.Domains
{

    /// <summary>
    /// Classe que representa a entidade (tabela) usuario
    /// </summary>
    public class UsuarioDomain
    {
        public int IdUsuario { get; set; }
        [Required(ErrorMessage ="O campo de email é obrigatorio!")]
        public string Email { get; set; }

        [StringLength(20,MinimumLength =5, ErrorMessage ="O campo deve ter de 5 a 20 caracteres!")]
        [Required(ErrorMessage ="O campo senha é obrigatorio!")]
        public string Senha { get; set; }
        public string Permissao { get; set; }
    }
}
