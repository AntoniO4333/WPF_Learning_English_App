using Cheremushkinae_107d2.Model;
using Cheremushkinae_107d2.ViewModel;
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
using MyGlobalVarAndSettings;

namespace Cheremushkinae_107d2
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();

            Closing += SettingsWindow_Closing;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void SettingsWindow_Closing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
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

        private void LearnngWindow_Click(object sender, RoutedEventArgs e)
        {
            if (GlobalSettings.SavedLearnWordsCount != 0)
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
            else
            {
                MessageBox.Show("You have no words in your dictionary. Add some to learn!");
                AddNewWordWindow_Click(sender, e);
            }
        }

        private void AddNewWordWindow_Click(object sender, RoutedEventArgs e)
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

        private void ShowStatistics_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("number of words studied: " + DataWorker.GetAllUserLearningWords(GlobalSettings.SavedUserID).Count + "\n"
                + "number of words learned: " + DataWorker.GetAllKnowWords(GlobalSettings.SavedUserID).Count);
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            GlobalSettings.SavedUsername = null;
            GlobalSettings.SavedUserID = 0;
            MainWindow mainWindow = Owner as MainWindow;
            AddNewWordWindow addNewWordWindow = new AddNewWordWindow();
            addNewWordWindow.UsernameLabel.Visibility = Visibility.Collapsed;
            LearningWindow learningWindow = new LearningWindow();
            learningWindow.UsernameLabel.Visibility = Visibility.Collapsed;
            if (mainWindow != null)
            {
                mainWindow.SignInMain.Visibility = Visibility.Visible;
                mainWindow.SignUpMain.Visibility = Visibility.Visible;
                mainWindow.UsernameLabel.Visibility = Visibility.Collapsed;
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

        private void DeleteAllDictionary_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            List<LearnDict> AllUserLearningWordsDict = DataWorker.GetAllUserLearningWords(GlobalSettings.SavedUserID);
            if (result == MessageBoxResult.Yes)
            {
                for(int i  = 0; i < AllUserLearningWordsDict.Count; i++)
                {
                    string word_in_eng = AllUserLearningWordsDict[i].Word_in_English;
                    DataWorker.DeleteLearningWordInDB(word_in_eng);
            }
                MessageBox.Show("dictionary deleted successfuly");
            }
        }

        private void DeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show(DataWorker.DeleteUserInDB(GlobalSettings.SavedUsername));
                MainWindow mainWindow = Owner as MainWindow;
                if (mainWindow != null)
                {
                    mainWindow.SignInMain.Visibility = Visibility.Visible;
                    mainWindow.SignUpMain.Visibility = Visibility.Visible;
                    mainWindow.UsernameLabel.Visibility = Visibility.Collapsed;
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
        }
    }
}
