using RBM.Data;
using RBM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RBM
{
    /// <summary>
    /// 入力画面
    /// </summary>
    public partial class InputView : UserControl
    {
        /// <summary>
        /// データベース制御用
        /// </summary>
        private DatabaseHelper _databaseHelper;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="databaseHelper">メイン画面から引き継ぐデータベース制御インスタンス</param>
        public InputView(DatabaseHelper databaseHelper)
        {
            InitializeComponent();

            _databaseHelper = databaseHelper;
        }

        /// <summary>
        /// 画面読み込み時にカーソルと入力ボックス内の初期化
        /// </summary>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // カーソルをタイトルのテキストボックスに移動
            TitleTextBox.Focus();

            // 読書開始日が空欄の場合、今日の日にちを入力
            if (StartDatePicker.SelectedDate == null)
            {
                StartDatePicker.SelectedDate = DateTime.Today;
            }
        }

        /// <summary>
        /// テキストボックスにカーソルが入った際に全選択をする
        /// </summary>
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox)?.SelectAll();
        }

        /// <summary>
        /// ページ数の入力には正の整数のみを入力可とする
        /// </summary>
        private void PageCountTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!uint.TryParse(e.Text, out var _))
            {
                e.Handled = true;
                return;
            }
        }

        /// <summary>
        /// キャンセルボタン押下時、入力を破棄してHome画面へ遷移する
        /// </summary>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // MainWindowを取得して画面切り替えのメソッドを呼ぶ
            MainWindow? mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.NavigateToHomeView();
            }
        }

        /// <summary>
        /// 登録ボタン押下時、入力値を検証してDBへ保存しHome画面へ遷移する
        /// </summary>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Book book = new Book();
            
            // 入力値の格納
            book.Title = TitleTextBox.Text;
            book.Author = AuthorTextBox.Text;
            book.StartDate = StartDatePicker.SelectedDate;

            int pageCount = 0;
            if (uint.TryParse(PageCountTextBox.Text, out var count))
            {
                pageCount = (int)count;
            }
            book.PageCount = pageCount;

            // タイトルが空だったときにエラーダイアログを表示
            if (string.IsNullOrEmpty(book.Title))
            {
                MessageBox.Show("タイトルを入力してください");
                return;
            }

            // 情報を登録
            if (_databaseHelper.SaveBookInfo(book, out string errorMessage))
            {
                // MainWindowを取得して画面切り替えのメソッドを呼ぶ
                MainWindow? mainWindow = Window.GetWindow(this) as MainWindow;
                if (mainWindow != null)
                {
                    mainWindow.NavigateToHomeView();
                }
            }
            else
            {
                MessageBox.Show($"{errorMessage}");
            }
        }
    }
}
