using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using BlackJack.ConnectionSettings;
using BlackJack.DAL.Dapper;
using BlackJack.DAL.Entities;
using BlackJack.Utitlity.Utilities;
using Dapper.Contrib.Extensions;

namespace BlackJack.DAL.Repositories
{
    public class GameRepository:DpGenericRepository<Game>
    {
        private readonly string _connectionString = ConnectionStrings.Connection;
        private IDbConnection _connection;

        public int CreateAndKnowId(Game itemGame)
        {
            try
            {
                using (_connection = new SqlConnection(_connectionString))
                {
                    _connection.Insert(itemGame);
                }

                int id = itemGame.Id;
                return id;
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "GameRepository");
                return 0;
            }

        }
    }
}
