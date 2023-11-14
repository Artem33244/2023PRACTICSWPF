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
using System.Data;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Calculator : Window
    {
        public Calculator()
        {
            InitializeComponent();

            foreach (UIElement elm in LayoutRoot.Children)
            {
                if (elm is Button button)
                {
                    button.Click += Button_Click;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string str = ((Button)e.OriginalSource)?.Content?.ToString() ?? string.Empty;

            if (str == "CLEAR")
            {
                Clear();
            }
            else if (str == "=")
            {
                EvaluateExpression();
            }
            else
            {
                AppendToExpression(str);
            }
        }

        private void Clear()
        {
            textLabel.Text = "";
        }

        private void EvaluateExpression()
        {
            try
            {
                string value = new DataTable().Compute(textLabel.Text, null)?.ToString() ?? string.Empty;
                textLabel.Text = value;
            }
            catch
            {
                HandleError();
            }
        }

        private void AppendToExpression(string str)
        {
            if (str == "/" && textLabel.Text.EndsWith("/"))
            {
                HandleError();
            }
            else
            {
                textLabel.Text += str;
            }
        }

        private void HandleError()
        {
            textLabel.Text = "Error";
        }
    }
}
