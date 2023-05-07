using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTime.SQLite
{
    public static class SQLiteExtension
    {
        public static int InsertDenco(this SQLiteCommand command, int no, string name, string type, string attr,
    int maxap, int maxhp, string skill = null)
        {
            var skillstr = skill == null ? "null" : $"'{skill}'";
            command.CommandText = "INSERT INTO denco(no, name, type, attribute, maxap, maxhp, skill) VALUES(" +
                $"{no}, '{name}', '{type}', '{attr}', {maxap}, {maxhp}, {skillstr})";
            return command.ExecuteNonQuery();
        }

        public static string DumpQuery(this SQLiteDataReader reader)
        {
            var i = 0;
            var sb = new StringBuilder();
            while (reader.Read())
            {
                if (i == 0)
                {
                    sb.AppendLine(string.Join("\t", reader.GetValues().AllKeys));
                    sb.AppendLine(new string('=', 8 * reader.FieldCount));
                }
                sb.AppendLine(string.Join("\t", Enumerable.Range(0, reader.FieldCount).Select(x => reader.GetValue(x))));
                i++;
            }

            return sb.ToString();
        }
    }
}
