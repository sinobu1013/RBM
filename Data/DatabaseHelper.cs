using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using RBM.Models;

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

        public List<Book> getBookInfo()
        {
            var booksList = new List<Book>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = @"SELECT * FROM Books";
                connection.Open();
                using (var readeer = command.ExecuteReader())
                {
                    while (readeer.Read())
                    {
                        var book = new Book();
                        book.Title = readeer["Title"].ToString();
                        book.Author = readeer["Author"] as string;
                        booksList.Add(book);
                    }

                }
            }

            return booksList;
        }

        /// <summary>
        /// 最初の２件だけを取得するメソッド
        /// </summary>
        /// <returns></returns>
        public List<Book> GetRecentBooks()
        {
            var booksList = new List<Book>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = @"SELECT * FROM Books ORDER BY Id DESC LIMIT 2;";
                connection.Open();
                using (var readeer = command.ExecuteReader())
                {
                    while (readeer.Read())
                    {
                        var book = new Book();
                        book.Title = readeer["Title"].ToString();
                        book.Author = readeer["Author"] as string;
                        booksList.Add(book);
                    }
                }
            }

            return booksList;
        }
    }
}
