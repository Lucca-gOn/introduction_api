using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using System.Data.SqlClient;

namespace senai.inlock.webApi.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        /// <summary>
        /// String de conexão com o banco de dados que recebe os seguintes parâmetros:
        /// Data Source: Nome do servidor 
        /// Initial Catalog: Nome do banco de dados
        /// Autentificação:
        ///     -Windows: Integrated Security = true
        ///     -SqlServer: User Id = sa; Pwd = Senha
        /// </summary>
        private string StringConexao = "Data Source = NOTE08-S14; Initial Catalog = inlock_games_manha; User Id= sa; Pwd = Senai@134";
        public void Cadastrar(JogoDomain novoJogo)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryAdd = "INSERT INTO Jogo(IdEstudio,Nome,Descricao,DataLancamento,Valor) VALUES (@IdEstudio,@Nome,@Descricao,@DataLancamento,@Valor)";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryAdd, con))
                {
                    cmd.Parameters.AddWithValue("@IdEstudio", novoJogo.IdEstudio);
                    cmd.Parameters.AddWithValue("@Nome", novoJogo.Nome);
                    cmd.Parameters.AddWithValue("@Descricao", novoJogo.Descricao);
                    cmd.Parameters.AddWithValue("@DataLancamento", novoJogo.DataLancamento);
                    cmd.Parameters.AddWithValue("Valor", novoJogo.Valor);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryDelete = "DELETE FROM Jogo WHERE IdJogo = @Id";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    cmd.ExecuteNonQuery();
                }

            }
        }

        public List<JogoDomain> ListarTodos()
        {

            //Cria uma lista de objetos do tipo Jogo
            List<JogoDomain> listaJogos = new List<JogoDomain>();


            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string querySelectJogos = "SELECT Jogo.IdJogo, Jogo.IdEstudio, Jogo.Nome, Jogo.Descricao, Jogo.DataLancamento, Jogo.Valor, Estudio.Nome, Estudio.IdEstudio FROM jogo INNER JOIN Estudio ON Jogo.IdEstudio = Estudio.IdEstudio";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectJogos, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        //Instancia objeto
                        JogoDomain jogo = new JogoDomain()
                        {
                            IdJogo = Convert.ToInt32(rdr[0]),

                            IdEstudio = Convert.ToInt32(rdr[1]),

                            Nome = rdr["Nome"].ToString(),

                            Descricao = rdr["Descricao"].ToString(),

                            DataLancamento = Convert.ToDateTime(rdr["DataLancamento"]),

                            //O método Convert.ToSingle é usado para converter um valor do seu tipo de dados atual para o tipo de dados float;
                            Valor = Convert.ToSingle(rdr["Valor"]),

                            Estudio = new EstudioDomain()
                            {
                                IdEstudio = Convert.ToInt32(rdr[0]),
                                Nome = rdr["Nome"].ToString()
                            }
                        };

                        listaJogos.Add(jogo);

                    }
                    return listaJogos;
                }

            }
        }
    }
}
