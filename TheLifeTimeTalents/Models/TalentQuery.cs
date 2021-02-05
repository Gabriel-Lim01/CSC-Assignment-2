using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace TheLifeTimeTalents.Models
{
    public class TalentQuery
    {
        public AppDb Db { get; }

        public TalentQuery(AppDb db)
        {
            Db = db;
        }

        public async Task<Talent> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Id`, `Name`, `ShortName`, `Reknown`, `Bio`, `Url` FROM `Talents` WHERE `Id` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<Talent>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Id`, `Name`, `ShortName`, `Reknown`, `Bio`, `Url` FROM `Talents`";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task DeleteAllAsync()
        {
            using var txn = await Db.Connection.BeginTransactionAsync();
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `Talents`";
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();
        }

        private async Task<List<Talent>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<Talent>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Talent(Db)
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        ShortName = reader.GetString(2),
                        Reknown = reader.GetString(3),
                        Bio = reader.GetString(4),
                        Url = reader.GetString(5),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}