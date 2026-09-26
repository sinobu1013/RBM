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
    /// <summary>
    /// データベースを制御するクラス
    /// </summary>
    public class DatabaseHelper
    {
        /// <summary>
        /// データベース接続用の文字列（DBファイルの名前）
        /// </summary>
        private readonly string _connectionString = "Data Source=RBM.db";

        /// <summary>
        /// データベースの初期化（テーブルがなければ作成）
        /// </summary>
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

        /// <summary>
        /// １冊の本の情報をデータベースに保存する
        /// </summary>
        /// <param name="book">保存する本の情報</param>
        /// <param name="errorMessage">例外が発生したときにエラーメッセージが格納される</param>
        /// <returns>成功した場合Trueが返る</returns>
        public bool SaveBookInfo(Book book, out string errorMessage)
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

        /// <summary>
        /// すべての本のリストを返す
        /// </summary>
        /// <returns>すべての本情報のリスト</returns>
        public List<Book> GetAllBookInfo()
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
        /// 直近に登録された2件の本情報を取得する
        /// </summary>
        /// <returns>最初の２件だけの本情報</returns>
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
