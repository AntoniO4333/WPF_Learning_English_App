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
using System.Windows.Shapes;

namespace Cheremushkinae_107d2
{
    /// <summary>
    /// Логика взаимодействия для AddNewWordWindow.xaml
    /// </summary>
    public partial class AddNewWordWindow : Window
    {
        public AddNewWordWindow()
        {
            InitializeComponent();
            Closing += AddNewWordWindow_Closing;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void BackToMain_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Owner as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.Show();
                this.Hide();
            }
            else
            {
                mainWindow = new MainWindow();
                mainWindow.Owner = this;
                mainWindow.Show();
                this.Hide();
            }
        }

        private void StartLearning_Click(object sender, RoutedEventArgs e)
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


        private void Settings_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddNewWordWindow_Closing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
