using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using System.Data.SqlClient;

namespace senai.inlock.webApi.Repositories
{
    public class EstudioRepository : IEstudioRepository
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
        public void Cadastrar(EstudioDomain novoEstudio)
        {
            using (SqlConnection con = new SqlConnection(StringConexao)) 
            {
                string queryAdd = "INSERT INTO Estudio(Nome) VALUES (@Nome)";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryAdd, con)) 
                {
                    cmd.Parameters.AddWithValue("@Nome", novoEstudio.Nome);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(StringConexao)) 
            {
                string queryDelete = "DELETE FROM Estudio WHERE IdEstudio = @Id";

                con.Open();

                using(SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    cmd.ExecuteNonQuery();
                }
            
            }
        }

        /// <summary>
        /// Lista todos objetos estudio
        /// </summary>
        /// <returns></returns>
        public List<EstudioDomain> ListarTodos()
        {
            List<EstudioDomain> ListaEstudio = new List<EstudioDomain>();

            using (SqlConnection con = new SqlConnection(StringConexao)) 
            {
                string querySelectEstudio = "SELECT IdEstudio, Nome FROM Estudio ";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectEstudio, con))
                {
                    //ExecuteReader = execute a consulta
                    //Executa a carry e armazena os dados do rdr
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        //Instancia objeto
                        EstudioDomain estudio = new EstudioDomain()
                        {
                            //Atribui a propriedade IdEstudio ([0]) o valor recebido no rdr
                            IdEstudio = Convert.ToInt32(rdr[0]),
                            //Atribui a propriedade Nome o valor recebido no rdr
                            Nome = rdr["Nome"].ToString()
                        };
                        //Adiciona cada objeto dentro da lista 
                        ListaEstudio.Add(estudio);
                    }
                }
            }
            return ListaEstudio;
        }
    }
}
