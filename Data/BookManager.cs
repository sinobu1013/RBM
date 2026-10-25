using RBM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBM.Data
{
    /// <summary>
    /// メモリ上の全読書履歴の保持と、DatabaseHelperへの操作指示を一括して行う仲介クラス
    /// </summary>
    public class BookManager
    {
        private DatabaseHelper _databaseHelper = new DatabaseHelper();
        private ObservableCollection<Book> _books = new ObservableCollection<Book>();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BookManager()
        {
            // 初期化
            Initialize();
        }

        /// <summary>
        /// 初期化。全読書情報をデータベースから取り出しておく
        /// </summary>
        public void Initialize()
        {
            _databaseHelper.InitializeDatabase();

            // 全読書データを取得
            _books.Clear();
            List<Book> books = _databaseHelper.GetAllBookInfo();
            foreach (Book book in books)
            {
                _books.Add(book);
            }
        }

        /// <summary>
        /// 全読書情報を返す
        /// </summary>
        /// <returns>全読書情報</returns>
        public ObservableCollection<Book> GetBooks()
        {
            return _books;
        }

        /// <summary>
        /// 直近に登録された任意の数の本情報を取得する
        /// </summary>
        /// <param name="count">取り出したい本情報の個数</param>
        /// <returns>最初のcount件だけの本情報</returns>
        public List<Book> GetRecentBooks(int count = 2)
        {
            return _books.Where(b => b.StartDate.HasValue && !b.EndDate.HasValue).
                OrderByDescending(b => b.StartDate).Take(count).ToList();
        }

        /// <summary>
        /// １冊の本の情報をデータベースに保存する
        /// </summary>
        /// <param name="book">保存する本の情報</param>
        /// <param name="errorMessage">例外が発生したときにエラーメッセージが格納される</param>
        /// <returns>成功した場合Trueが返る</returns>
        public bool SaveBookInfo(Book book, out string errorMessage)
        {
            bool isSuccess = _databaseHelper.SaveBookInfo(book, out errorMessage);

            if (isSuccess)
            {
                _books.Add(book);
            }

            return isSuccess;
        }
    }
}
