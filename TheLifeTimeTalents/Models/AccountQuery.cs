using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace TheLifeTimeTalents.Models
{
    public class AccountQuery
    {
        public AppDb Db { get; }

        public AccountQuery(AppDb db)
        {
            Db = db;
        }

        public async Task<Account> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Id`, `Email`, `Username`, `Password`, `AccountType` FROM `Users` WHERE `Id` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<Account> FindOneAsyncViaEmail(string email)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Id`, `Email`, `Username`, `Password`, `AccountType` FROM `Users` WHERE `Email` = @email";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@email",
                DbType = DbType.String,
                Value = email,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<Account>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Id`, `Email`, `Username`, `Password`, `AccountType` FROM `Users`";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task DeleteAllAsync()
        {
            using var txn = await Db.Connection.BeginTransactionAsync();
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `Users`";
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();
        }

        private async Task<List<Account>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<Account>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Account(Db)
                    {
                        Id = reader.GetInt32(0),
                        Email = reader.GetString(1),
                        Username = reader.GetString(2),
                        Password = reader.GetString(3),
                        AccountType = reader.GetInt32(4),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}
