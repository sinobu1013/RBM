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
    /// InputView.xaml の相互作用ロジック
    /// </summary>
    public partial class InputView : UserControl
    {
        public InputView()
        {
            InitializeComponent();
        }

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

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox)?.SelectAll();
        }

        private void PageCountTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!uint.TryParse(e.Text, out var _))
            {
                e.Handled = true;
                return;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // MainWindowを取得して画面切り替えのメソッドを呼ぶ
            if (Window.GetWindow(this) is MainWindow mainWindow)
            {
                mainWindow.NavigateToHomeVew();
            }
        }
    }
}
