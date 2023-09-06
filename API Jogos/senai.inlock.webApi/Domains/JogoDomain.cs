namespace senai.inlock.webApi.Domains
{
    /// <summary>
    /// Classe que representa a entidade (tabela) Jogo
    /// </summary>
    public class JogoDomain
    {
        public int IdJogo { get; set; }
        public int IdEstudio { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public DateTime DataLancamento { get; set; }
        public float Valor { get; set; }

        //Refenrecia para a classe Estudio
        public EstudioDomain Estudio { get; set; }
    }
}
