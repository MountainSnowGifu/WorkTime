using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTime.SQLite
{
    internal class SQLiteHelper
    {
        internal readonly static SQLiteConnectionStringBuilder _sqlConnectionSb;
        internal readonly static int _commandTimeout = 5;
        internal readonly static string filePath = @"C:\Users\akira\OneDrive\デスクトップ\sqliteTest.db";
        static SQLiteHelper()
        {
            _sqlConnectionSb = new SQLiteConnectionStringBuilder
            {
                DataSource = filePath,
                Version = 3,
                LegacyFormat = false,
                SyncMode = SynchronizationModes.Off,
                JournalMode = SQLiteJournalModeEnum.Memory
            };

            //_sqlConnectionSb = new SQLiteConnectionStringBuilder
            //{
            //    DataSource = filePath,
            //    Version = 3,
            //    LegacyFormat = false,
            //    SyncMode = SynchronizationModes.Normal,
            //    JournalMode = SQLiteJournalModeEnum.Wal
            //};
        }

        internal static void Query(
                                       string sql,
                                       SQLiteParameter[] parameters,
                                       Action<SQLiteDataReader> action)
        {
            using (var connection = new SQLiteConnection(_sqlConnectionSb.ToString()))
            {
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.CommandTimeout = _commandTimeout;
                    connection.Open();
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            action(reader);
                        }
                    }
                }
            }
        }

        internal static void Execute(
                                    string sql,
                                    SQLiteParameter[] parameters)
        {
            using (var connection = new SQLiteConnection(_sqlConnectionSb.ToString()))
            {
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.CommandTimeout = _commandTimeout;
                    connection.Open();
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    command.ExecuteNonQuery();
                }
            }
        }

        internal static void ExecuteNoneQueryWithTransaction(string connectString, string[] sqls)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectString))
            {
                connection.Open();
                SQLiteTransaction trans = connection.BeginTransaction();

                try
                {
                    foreach (string sql in sqls)
                    {
                        using (SQLiteCommand cmd = connection.CreateCommand())
                        {
                            cmd.Transaction = trans;

                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                        }
                    }
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }
            }
        }
    }
}
