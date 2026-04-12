using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace RBM.Data
{
    public class DatabaseHelper
    {
        // データベース接続用の文字列（DBファイルの名前）
        private readonly string _connectionString = "Data Source=RBM.db";

        public void InitializeDatabase()
        {
            // データベースへの接続
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                // テーブル作成（テーブルがなければ）
                var createTableSql = @"
                    CREATE TABLE IF NOT EXISTS Books (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Title TEXT NOT NULL,
                        Author TEXT,
                        StartDate TEXT,
                        EndDate TEXT
                    );";

                // SQMLデータベースに送信
                using (var command = new SqliteCommand(createTableSql, connection))
                {
                    command.ExecuteNonQuery();  // SELECT以外のSQL（変更や作成）を実行する命令
                }
            }
        }

        public void saveBookInfo(string title, string author, DateTime? startDate=null, DateTime? endDate=null)
        {
            System.Diagnostics.Debug.WriteLine(title, ", ", author);

            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                StringBuilder bulider = new StringBuilder();
                bulider.Append($"INSERT INTO Books (Title, Author) VALUES('{title}', '{author}');");
                connection.Open();
                command.CommandText = bulider.ToString();
                command.ExecuteNonQuery();
            }
        }
    }
}
