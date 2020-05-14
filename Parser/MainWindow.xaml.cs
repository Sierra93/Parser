using Parser.Core;
using Parser.Core.Habra;
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

namespace Parser {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        ParserWorker<string[]> parser;

        // При инициализации приложения.
        public MainWindow() {
            InitializeComponent();
            parser = new ParserWorker<string[]>(
                new HabraParser()
            );

            // Инициализирует события.
            parser.OnNewData += Parser_OnNewData;
            parser.OnCompleted += Parser_OnCompleted;
        }

        private void Parser_OnCompleted(object obj) {
            MessageBox.Show("All works done!");
        }

        private void Parser_OnNewData(object arg1, string[] arg2) {
            ListTitles.Items.Add(arg2);
        }

        // Метод кнопки старт.
        private void Button_Click(object sender, RoutedEventArgs e) {
            // Пишет настройки парсера.
            parser.Settings = new HabraSettings(Convert.ToInt32(NumericStart.Text), Convert.ToInt32(NumericEnd.Text));
            parser.Start();
        }

        // Метод кнопки аборт.
        private void Button_Click_1(object sender, RoutedEventArgs e) {
            parser.Abort();
        }
    }
}
