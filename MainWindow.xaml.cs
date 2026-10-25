using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RBM.Data;

namespace RBM
{
    /// <summary>
    /// メイン画面
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 一覧画面のオブジェクト
        /// </summary>
        private ListWindow? _listWindow;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

        }

        /// <summary>
        /// メイン画面ロード時、Home画面を表示する
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // 起動時はHomeViewを表示
            NavigateToHomeView();
        }

        /// <summary>
        /// Home画面へ遷移
        /// </summary>
        public void NavigateToHomeView()
        {
            var homeView = new HomeView();

            // 今読んでいる本2件を取得
            var recentBooks = App.BookManager.GetRecentBooks(2);
            homeView.RecentBooks.ItemsSource = recentBooks;

            MainContent.Content = homeView;
        }

        /// <summary>
        /// 入力画面へ遷移
        /// </summary>
        public void NavigateToInputView()
        {
            var inputView = new InputView();
            MainContent.Content = inputView;
        }

        /// <summary>
        /// サイドバーのHomeボタン押下時、Home画面を表示する
        /// </summary>
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateToHomeView();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_listWindow == null)
            {
                _listWindow = new ListWindow();

                // 閉じられた際はnullに戻す
                _listWindow.Closing += (s, e) => _listWindow = null;

                // 一覧画面を開く(平衡操作可能）
                _listWindow.Show();

            }
            else
            {
                _listWindow.Activate();
            }
        }
    }
}