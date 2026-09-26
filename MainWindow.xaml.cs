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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DatabaseHelper _databaseHelper = new DatabaseHelper();

        public MainWindow()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // DB初期化
            _databaseHelper.InitializeDatabase();

            // 起動時はHomeViewを表示
            NavigateToHomeVew();
        }

        // サイドバーのHomeボタン押下時
        public void NavigateToHomeVew()
        {
            var homeView = new HomeView();

            // 今読んでいる本を取得
            var recentBooks = _databaseHelper.GetRecentBooks();
            homeView.RecentBooks.ItemsSource = recentBooks;

            MainContent.Content = homeView;
        }

        // サイドバーの入力ボタン押下時
        public void NavigateToInputView()
        {

        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateToHomeVew();
        }
    }
}