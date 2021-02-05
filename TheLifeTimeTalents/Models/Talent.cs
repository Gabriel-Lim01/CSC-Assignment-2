using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;

namespace TheLifeTimeTalents.Models
{
    public class Talent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Reknown { get; set; }
        public string Bio { get; set; }
        public string Url { get; set; }

        internal AppDb Db { get; set; }

        public Talent()
        {
        }

        internal Talent(AppDb db)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `Talents` (`Name`, `ShortName`,`Reknown`, `Bio`, `Url`) VALUES (@name, @shortName,@reknown,@bio,@url);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            Id = (int)cmd.LastInsertedId;
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `Talents` SET `Name` = @name, `ShortName` = @shortName, `Reknown` = @reknown, `Bio` = @bio, `Url` = @url WHERE `Id` = @id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `Talents` WHERE `Id` = @id;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = Id,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@name",
                DbType = DbType.String,
                Value = Name,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@shortName",
                DbType = DbType.String,
                Value = ShortName,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@reknown",
                DbType = DbType.String,
                Value = Reknown,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@bio",
                DbType = DbType.String,
                Value = Bio,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@url",
                DbType = DbType.String,
                Value = Url,
            });

        }

    }

}

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace TalentsSearch.Models
//{
//    public class Talent
//    {
//        public int Id { get; set; }
//        public string Name { get; set; }
//        public string ShortName { get; set; }
//        public string Reknown { get; set; }
//        public string Bio { get; set; }
//    }
//}
