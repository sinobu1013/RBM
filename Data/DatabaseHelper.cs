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
                        PageCount INTEGER,
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

        public bool saveBookInfo(Book book, out string errorMessage)
        {
            // 入力データの準備
            string? startDate = book.StartDate?.ToString("yyyy-MM-dd");
            string? endDate = book.EndDate?.ToString("yyyy-MM-dd");

            try
            {
                using (var connection = new SqliteConnection(_connectionString))
                {
                    var command = connection.CreateCommand();
                    connection.Open();
                    command.CommandText = "INSERT INTO Books (Title, Author, PageCount, StartDate, EndDate) VALUES(" +
                        " @Title," +
                        " @Author," +
                        " @PageCount," +
                        " @StartDate," +
                        " @EndDate);";

                    command.Parameters.AddWithValue("@Title", book.Title);
                    command.Parameters.AddWithValue("@Author", book.Author);
                    command.Parameters.AddWithValue("@PageCount", book.PageCount);
                    command.Parameters.AddWithValue("@StartDate", (object?)startDate ?? DBNull.Value);
                    command.Parameters.AddWithValue("@EndDate", (object?)endDate ?? DBNull.Value);

                    command.ExecuteNonQuery();
                }

                errorMessage = string.Empty;
                return true;
            } catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
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
