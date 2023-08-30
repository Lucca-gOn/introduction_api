using System.Data.SqlClient;
using webapi.Filmes.Domains;
using webapi.Filmes.Interfaces;

namespace webapi.Filmes.Repositories
{
    public class GeneroRepository : IGeneroRepository
    {
        /// <summary>
        /// String de conexão com o banco de dados que recebe os seguintes parâmetros:
        /// Data Source: Nome do servidor 
        /// Initial Catalog: Nome do banco de dados
        /// Autentificação:
        ///     -Windows: Integrated Security = true
        ///     -SqlServer: User Id = sa; Pwd = Senha
        /// </summary>
        private string StringConexao = "Data Source = NOTE10-S14\\SQLEXPRESS; Initial Catalog = Filmes; User Id= sa; Pwd = Senai@134";
        public void AtualizarIdCorpo(GeneroDomain genero)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //UPDATE Nome_Tabela SET coluna a ser atualizada = valor atualizado ['''WHERE''' ''condição'']
                string queryUpdate = "UPDATE Genero SET Nome = @Nome WHERE IdGenero = @IdGenero ";

                using(SqlCommand cmd = new SqlCommand(queryUpdate,con))
                {
                    cmd.Parameters.AddWithValue("@Nome", genero.Nome);
                    cmd.Parameters.AddWithValue("@IdGenero", genero.IdGenero);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }

            }
        }

        public void AtualizarUrl(int id, GeneroDomain genero)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryUpdateUrl = "UPDATE Genero SET Nome = @Nome WHERE IdGenero = @IdGenero";

                using (SqlCommand cmd = new SqlCommand(queryUpdateUrl,con))
                {
                    cmd.Parameters.AddWithValue("@IdGenero", id);
                    cmd.Parameters.AddWithValue("@Nome", genero.Nome);

                    con.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }

        /// <summary>
        /// Busca um genero por ID
        /// </summary>
        /// <param name="id">Objeto buscado por ID</param>
        GeneroDomain IGeneroRepository.BuscarPorId(int id)
        {
            //Declara a conexão passando a string de conexão como parâmetro
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Declara a query a ser executada
                string querySelectById = "SELECT IdGenero, Nome FROM Genero WHERE IdGenero = @IdGenero";

                // Abre a conexão com o banco de dados
                con.Open();

                // Declara o SqlDataReader rdr para receber os valores do banco de dados
                SqlDataReader rdr;

                // Declara o SqlCommand cmd passando a query que será executada e a conexão como parâmetros
                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    // Passa o valor para o parâmetro @IdGenero
                    cmd.Parameters.AddWithValue("@IdGenero", id);

                    // Executa a query e armazena os dados no rdr
                    rdr = cmd.ExecuteReader();

                    // Verifica se o resultado da query retornou algum registro
                    if (rdr.Read())
                    {
                        // Se sim, instancia um novo objeto generoBuscado do tipo GeneroDomain
                        GeneroDomain generoBuscado = new GeneroDomain
                        {
                            // Atribui à propriedade IdGenero o valor da coluna "IdGenero" da tabela do banco de dados
                            IdGenero = Convert.ToInt32(rdr["IdGenero"]),

                            // Atribui à propriedade Nome o valor da coluna "Nome" da tabela do banco de dados
                            Nome = rdr["Nome"].ToString()
                        };
                        // Retorna o generoBuscado com os dados obtidos
                        return generoBuscado;
                    }
                    // Se não, retorna null
                    return null;
                }
            }
        }

        /// <summary>
        /// Cadastrar um novo gênero
        /// </summary>
        /// <param name="novoGenero">Objeto com as informações que serão cadastradas</param>
        public void Cadastrar(GeneroDomain novoGenero)
        {
            //Declara a SqlConnection passando a string de conexão como pârametro
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //Declara a instrução a ser executada o (querySelectAll) pode ser qualquer nome
                string queryInsert = "INSERT INTO Genero(Nome) VALUES (@Nome)";

                //Declara o SqlCommand passando a query que será executada e a conexão com o banco de dados
                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    //Passa o valor do parâmetro @Nome
                    cmd.Parameters.AddWithValue("@Nome", novoGenero.Nome);

                    //Abre a conexão com o banco de dados
                    con.Open();

                    //Executar a query (queryInsert)
                    cmd.ExecuteNonQuery();
                }
            }
        }


        /// <summary>
        /// Deleta um gênero
        /// </summary>
        /// <param name="id">Objetos que serão excluidos</param>
        public void Deletar(int id)
        {
            //Declara a SqlConnection passando a string de conexão como pârametro
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryDelete = "DELETE FROM Genero WHERE IdGenero = @Id";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Listar todos os objetos gêneros
        /// </summary>
        /// <returns>Lista de objetos (gêneros)</returns>
        public List<GeneroDomain> ListarTodos()
        {
            //Cria uma lista de objetos do tipo gênero
            List<GeneroDomain> listaGeneros = new List<GeneroDomain>();

            //Declara a SqlConnection passando a string de conexão como pârametro
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //Declara a instrução a ser executada o (querySelectAll) pode ser qualquer nome
                string querySelectAll = "SELECT IdGenero, Nome FROM Genero";

                //Abre a conexão com o banco de dados
                con.Open();

                //Declara o SqlDataReader para percorrer a tabela do banco de dados
                SqlDataReader rdr;

                //Criar um novo recurso para executar o SELECT

                //Declara o SqlCommand passando a query que será executada e a conexão com o banco de dados  ou seja, passo primeiro o querySelectAll(query a ser executada) e depois o con (String de conexão)
                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    //ExecuteReader = execute a consulta
                    //Executa a carry e armazena os dados do rdr
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        //Instancia objeto
                        GeneroDomain genero = new GeneroDomain()
                        {
                            //Atribui a propriedade IdGenero ([0]) o valor recebido no rdr
                            IdGenero = Convert.ToInt32(rdr[0]),
                            //Atribui a propriedade Nome o valor recebido no rdr
                            Nome = rdr["Nome"].ToString()
                        };
                        //Adiciona cada objeto dentro da lista 
                        listaGeneros.Add(genero);
                    }
                }
            }

            //Retorna a lista de generos 
            return listaGeneros;
        }
    }
}
