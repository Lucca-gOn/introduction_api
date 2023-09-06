using System.Data.SqlClient;
using webapi.Filmes.Domains;
using webapi.Filmes.Interfaces;

namespace webapi.Filmes.Repositories
{
    public class FilmeRepository : IFilmeRepository
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
        public void AtualizarIdCorpo(FilmeDomain filme)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //UPDATE Nome_Tabela SET coluna a ser atualizada = valor atualizado ['''WHERE''' ''condição'']
                string queryUpdate = "UPDATE Filme SET Titulo = @Titulo, IdGenero = @IdGenero WHERE IdFilme = @IdFilme ";

                using (SqlCommand cmd = new SqlCommand(queryUpdate, con))
                {
                    cmd.Parameters.AddWithValue("@Titulo", filme.Titulo);
                    cmd.Parameters.AddWithValue("@IdFilme", filme.IdFilme);
                    cmd.Parameters.AddWithValue("@IdGenero", filme.IdGenero);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }

            }
        }

        public void AtualizarUrl(int id, FilmeDomain filme)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //UPDATE Nome_Tabela SET coluna a ser atualizada = valor atualizado ['''WHERE''' ''condição'']
                string queryUpdateUrl = "UPDATE Filme SET Titulo = @Titulo, IdGenero = @IdGenero WHERE IdFilme = @IdFilme ";

                using (SqlCommand cmd = new SqlCommand(queryUpdateUrl, con))
                {
                    cmd.Parameters.AddWithValue("@Titulo", id);
                    cmd.Parameters.AddWithValue("@IdFilme", filme.IdFilme);
                    cmd.Parameters.AddWithValue("@IdGenero", filme.IdGenero);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }

            }
        }

        /// <summary>
        /// Busca um  por ID
        /// </summary>
        /// <param name="id">Objeto buscado por ID</param>
        FilmeDomain IFilmeRepository.BuscarPorId(int id)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string querySelectById = "SELECT Filme.IdFilme, Filme.IdGenero, Filme.Titulo, Genero.Nome, Genero.IdGenero FROM filme LEFT JOIN Genero ON Filme.IdGenero = Genero.IdGenero WHERE IdFilme = @IdFilme";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@IdFilme", id);

                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        FilmeDomain filmeBuscado = new FilmeDomain
                        {
                            IdFilme = Convert.ToInt32(rdr["IdFilme"]),
                            IdGenero = Convert.ToInt32(rdr["IdGenero"]),
                            Titulo = rdr["Titulo"].ToString(),

                            Genero = new GeneroDomain()
                            {
                                //Atribui a propriedade IdGenero ([0]) o valor recebido no rdr
                                IdGenero = Convert.ToInt32(rdr["IdGenero"]),
                                //Atribui a propriedade Nome o valor recebido no rdr
                                Nome = rdr["Nome"].ToString()
                            }
                        }; 
                        return filmeBuscado;
                    }
                    return null;
                }

            }
        }

        /// <summary>
        /// Cadastrar um novo filme
        /// </summary>
        /// <param name="novoFilme">Objeto com as informações que serão cadastradas</param>
        public void Cadastrar(FilmeDomain novoFilme)
        {
            //Declara a SqlConnection passando a string de conexão como pârametro
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryInsert = "INSERT INTO Filme(IdGenero,Titulo) VALUES (@IdGenero,@Titulo)";

                //Declara o SqlCommand passando a query que será executada e a conexão com o banco de dados
                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    //Passa o valor do parâmetro @Nome
                    cmd.Parameters.AddWithValue("@IdGenero", novoFilme.IdGenero);
                    cmd.Parameters.AddWithValue("@Titulo", novoFilme.Titulo);

                    //Abre a conexão com o banco de dados
                    con.Open();

                    //Executar a query (queryInsert)
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryDelete = "DELETE FROM Filme WHERE IdFilme = @Id";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Listar todos os objetos filme
        /// </summary>
        /// <returns>Lista de objetos (filme)</returns>
        public List<FilmeDomain> ListarTodos()
        {
            //Cria uma lista de objetos do tipo gênero
            List<FilmeDomain> listaFilmes = new List<FilmeDomain>();

            //Declara a SqlConnection passando a string de conexão como pârametro
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string querySelectAll = "SELECT Filme.IdFilme, Filme.IdGenero, Filme.Titulo, Genero.Nome, Genero.IdGenero FROM filme INNER JOIN Genero ON Filme.IdGenero = Genero.IdGenero";

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
                        FilmeDomain filme = new FilmeDomain()
                        {
                            //Atribui a propriedade IdFilme ([0]) o valor recebido no rdr
                            IdFilme = Convert.ToInt32(rdr[0]),
                            //Atribui a propriedade IdGenero ([0]) o valor recebido no rdr
                            IdGenero = Convert.ToInt32(rdr[1]),
                            //Atribui a propriedade Titulo o valor recebido no rdr
                            Titulo = rdr["Titulo"].ToString(),

                            Genero = new GeneroDomain()
                            {
                                //Atribui a propriedade IdGenero ([0]) o valor recebido no rdr
                                IdGenero = Convert.ToInt32(rdr["IdGenero"]),
                                //Atribui a propriedade Nome o valor recebido no rdr
                                Nome = rdr["Nome"].ToString()
                            }



                        };



                        //Adiciona cada objeto dentro da lista 
                        listaFilmes.Add(filme);
                    }
                }

                return listaFilmes;
            }

        }


    }
}
