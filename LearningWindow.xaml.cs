using Cheremushkinae_107d2.Model;
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
            Loaded += LearningWindow_Loaded;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }


        private void LearningWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LearningProcess(sender, e);
        }

        private void LearningProcess(object sender, RoutedEventArgs e)
        {
            List<LearnDict> AllUserLearningWordsDict = DataWorker.GetAllUserLearningWords(GlobalSettings.SavedUserID);
            if (AllUserLearningWordsDict.Count == 0)
            {
                MessageBox.Show("You have learned all the added words! Let's add more!");
                AddNewWordLearning_Click(sender, e);
            } else
            {
                ShowTranslate.Visibility = Visibility.Visible;
                CurrentWord.Visibility = Visibility.Visible;
                Right.Visibility = Visibility.Collapsed;
                NotRight.Visibility = Visibility.Collapsed;
                CurrentTranslateWord.Visibility = Visibility.Collapsed;
                ExampleUsingThisWord.Visibility = Visibility.Collapsed;
                MotivationPhrase.Content = "Do you remember the translation of this word?";
                Random random = new Random();
                bool lang = (random.Next(0, 2) == 1); // 0 - eng, 1 - rus
                int RandomWordIndex = random.Next(0, AllUserLearningWordsDict.Count);
                LearnDict learnDict = AllUserLearningWordsDict[RandomWordIndex];
                GlobalSettings.SavedCurrentWord = learnDict;
                if (!lang)
                {
                    CurrentWord.Content = learnDict.Word_in_English;
                    CurrentTranslateWord.Content = learnDict.Word_in_Russian;
                    ExampleUsingThisWord.Content = learnDict.Using_example;
                } else
                {
                    CurrentWord.Content = learnDict.Word_in_Russian;
                    CurrentTranslateWord.Content = learnDict.Word_in_English;
                    ExampleUsingThisWord.Content = learnDict.Using_example;
                }
            }
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

        

        private void ShowTranslate_Click(object sender, RoutedEventArgs e)
        {
            ShowTranslate.Visibility = Visibility.Collapsed;
            Right.Visibility = Visibility.Visible;
            NotRight.Visibility = Visibility.Visible;
            CurrentTranslateWord.Visibility = Visibility.Visible;
            ExampleUsingThisWord.Visibility = Visibility.Visible;
            MotivationPhrase.Content = "You were right?";
        }
        private void IKnow_Click(object sender, RoutedEventArgs e)
        {
            MotivationPhrase.Content = "Nice!";
            LearnDict learnDict = GlobalSettings.SavedCurrentWord;
            learnDict.Right_answers_count += 1;
            DataWorker.UpdateLearningWordInDB(learnDict.Word_in_English, learnDict.Word_in_Russian, learnDict.Using_example, learnDict.Right_answers_count);
            GlobalSettings.SavedCurrentWord = null;
            LearningProcess(sender, e);
        }
        private void NotRight_Click(object sender, RoutedEventArgs e)
        {
            MotivationPhrase.Content = "Next time you remember this word!";
            LearnDict learnDict = GlobalSettings.SavedCurrentWord;
            DataWorker.UpdateLearningWordInDB(learnDict.Word_in_English, learnDict.Word_in_Russian, learnDict.Using_example, 0);
            GlobalSettings.SavedCurrentWord = null;
            LearningProcess(sender, e);
        }
    }
}
