using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using BlackJack.ConnectionSettings;
using BlackJack.DAL.Dapper;
using BlackJack.DAL.Entities;
using BlackJack.Utitlity.Utilities;
using Dapper;


namespace BlackJack.DAL.Repositories
{
    public class UserRepository:DpGenericRepository<User>
    {
        private static string _tableName = "Users";

        private readonly string _connectionString = ConnectionStrings.Connection;
        private IDbConnection _connection;

        public async Task<IEnumerable<dynamic>> GetSomeEntitiesAsync(int gameId)
        {
            try
            {
                using (_connection = new SqlConnection(_connectionString))
                {
                    _connection.Open();
                    var listOfUsers = await _connection.QueryAsync("SELECT * FROM " + _tableName + " WHERE GameId=@game", new { game = gameId });
                    _connection.Close();
                    return listOfUsers;
                }
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "UserRepository");
                return null;
            }


        }

        public User GetUserByNameAndGame(int gameId, string userName)
        {
            try
            {
                User user = new User();
                using (_connection = new SqlConnection(_connectionString))
                {
                    _connection.Open();
                    user = _connection.QuerySingle<User>($"SELECT * FROM {_tableName} WHERE GameId=@game AND Name=@Name",
                        new { game = gameId, Name = userName });
                    _connection.Close();
                }

                return user;
            }
            catch (Exception exception)
            {
                Console.Write(exception);
                throw;
            }
        }
    }
}
