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
    /// Home画面
    /// </summary>
    public partial class HomeView : UserControl
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public HomeView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 追加ボタン押下時、入力画面を表示する
        /// </summary>
        private void AddBookButton_Click(object sender, RoutedEventArgs e)
        {
            // MainWindowを取得して画面切り替えのメソッドを呼ぶ
            MainWindow? mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.NavigateToInputView();
            }
        }
    }
}
