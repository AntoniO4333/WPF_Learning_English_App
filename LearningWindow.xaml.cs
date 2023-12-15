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
    /// Логика взаимодействия для LearningWindow.xaml
    /// </summary> kn.lm,ASDV
    public partial class LearningWindow : Window
    {
        public LearningWindow()
        {
            InitializeComponent();
            if (GlobalSettings.SavedUsername != null)
            {
                this.UsernameLabel.Content = GlobalSettings.SavedUsername;
            }
            Closing += LearningWindow_Closing;
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

        private void AddNewWordLearning_Click(object sender, RoutedEventArgs e)
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


        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = Owner as SettingsWindow;
            if (settingsWindow != null)
            {
                settingsWindow.Show();
                this.Hide();
            }
            else
            {
                settingsWindow = new SettingsWindow();
                settingsWindow.Owner = this;
                settingsWindow.Show();
                this.Hide();
            }
        }

        private void LearningWindow_Closing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void IKnow_Click(object sender, RoutedEventArgs e)
        {
            CurrentWord.Content = "This is english word";
        }
    }
}
