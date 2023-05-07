using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTime.SQLite
{
    public static class SQLiteTest
    {
        public static void Test()
        {
            var sqlConnectionSb = new SQLiteConnectionStringBuilder { DataSource = @"C:\Users\akira\OneDrive\デスクトップ\sqliteTest.db" };

            using (var cn = new SQLiteConnection(sqlConnectionSb.ToString()))
            {
                cn.Open();

                using (var cmd = new SQLiteCommand(cn))
                {
                    //テーブル作成
                    //cmd.CommandText = "CREATE TABLE IF NOT EXISTS denco(" +
                    //    "no INTEGER NOT NULL PRIMARY KEY," +
                    //    "name TEXT NOT NULL," +
                    //    "type TEXT NOT NULL," +
                    //    "attribute TEXT NOT NULL," +
                    //    "maxap INTEGER NOT NULL," +
                    //    "maxhp INTEGER NOT NULL," +
                    //    "skill TEXT)";
                    //cmd.ExecuteNonQuery();

                    //データ追加
                    //cmd.InsertDenco(2, "為栗メロ", "アタッカー", "eco", 310, 300, "きゃのんぱんち");
                    //cmd.InsertDenco(3, "新阪ルナ", "ディフェンダー", "cool", 220, 360, "ナイトライダー");
                    //cmd.InsertDenco(4, "恋浜みろく", "トリックスター", "heat", 300, 360, "ダブルアクセス");
                    //cmd.InsertDenco(8, "天下さや", "アタッカー", "cool", 400, 240);
                    //cmd.InsertDenco(13, "新居浜いずな", "ディフェンダー", "heat", 290, 336, "重連壁");
                    //cmd.InsertDenco(31, "新居浜ありす", "ディフェンダー", "heat", 270, 350, "ハッピーホリデイ");

                    //MaxAPが300以上のでんこをMaxHPで降順ソート
                    cmd.CommandText = "SELECT * FROM denco WHERE maxap >= 300 ORDER BY maxhp desc";
                    using (var reader = cmd.ExecuteReader())
                    {
                        var dump = reader.DumpQuery();
                        Console.WriteLine(dump);
                    }

                    //LIKE句比較（ここがLINQとちょっと違う）
                    cmd.CommandText = "SELECT * FROM denco WHERE name LIKE '新居浜%'";//新居浜～で始まる名前を抽出
                    using (var reader = cmd.ExecuteReader())
                    {
                        var dump = reader.DumpQuery();
                        Console.WriteLine(dump);
                    }
                }
            }
        }
    }
}
