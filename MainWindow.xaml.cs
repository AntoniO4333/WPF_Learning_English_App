using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Cheremushkinae_107d2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Closing += MainWindow_Closing;
            this.ResizeMode = ResizeMode.NoResize;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

        }

        private void SettingsMain_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void SignInMain_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SignUpMain_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StartLearningMain_Click(object sender, RoutedEventArgs e)
        {
            LearningWindow learningWindow = Owner as LearningWindow;
            if (learningWindow != null)
            {
                learningWindow.Show();
                this.Hide();
            }
            else
            {
                learningWindow = new LearningWindow();
                learningWindow.Owner = this;
                learningWindow.Show();
                this.Hide();
            }
        }

        private void AddNewWordMain_Click(object sender, RoutedEventArgs e)
        {
            AddNewWordWindow addNewWordWindow = Owner as AddNewWordWindow;
            if (addNewWordWindow != null)
            {
                addNewWordWindow.Show();
                this.Hide();
            }
            else
            {
                addNewWordWindow = new AddNewWordWindow();
                addNewWordWindow.Owner = this;
                addNewWordWindow.Show();
                this.Hide();
            }
        }


        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
