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
        public MainWindow()
        {
            InitializeComponent();

            // 表の更新
            upDateTable();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var dbHelper = new DatabaseHelper();
            dbHelper.InitializeDatabase();
        }

        private void save_button_Click(object sender, RoutedEventArgs e)
        {
            var dbHelper = new DatabaseHelper();
            dbHelper.saveBookInfo(book_title.Text, book_author.Text);

            // 表の描画を更新
            upDateTable();
        }

        private void upDateTable()
        {
            var dbHelper = new DatabaseHelper();
            booksTable.ItemsSource = dbHelper.getBookInfo();
        }
    }
}