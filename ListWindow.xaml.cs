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
using System.Windows.Shapes;

namespace RBM
{
    /// <summary>
    /// 一覧画面
    /// </summary>
    public partial class ListWindow : Window
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ListWindow()
        {
            InitializeComponent();
            Title = "書籍一覧";
        }

        /// <summary>
        /// 表を読み込み時に、全読書情報を取得
        /// </summary>
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            AllBooksList.ItemsSource = App.BookManager.GetBooks();
        }

        /// <summary>
        /// 「メイン画面を表示」ボタン押下で、メイン画面を最前面に表示
        /// </summary>
        private void ActiveMainWindowButton_Click(object sender, RoutedEventArgs e)
        {
            // メインウィンドウを最前面に移動
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                // もしメインウィンドウが最小化されていたら、元のサイズに戻す
                if (mainWindow.WindowState == WindowState.Minimized)
                {
                    mainWindow.WindowState = WindowState.Normal;
                }

                // 最前面に表示してフォーカスを当てる
                mainWindow.Activate();
            }
        }
    }
}
