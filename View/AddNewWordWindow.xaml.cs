﻿using Cheremushkinae_107d2.Model;
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
    /// Логика взаимодействия для AddNewWordWindow.xaml
    /// </summary>


    public partial class AddNewWordWindow : Window
    {
        public AddNewWordWindow()
        {
            InitializeComponent();
            if (GlobalSettings.SavedUsername != null)
            {
                this.UsernameLabel.Content = GlobalSettings.SavedUsername;
            }
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

        private void AddNewWordWindow_Closing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void AddThisWord_Click(object sender, RoutedEventArgs e)
        {
            if ((WordInEnglishTextBox.Text != "") && (WordInRussianTextBox.Text != ""))
            {
                MessageBox.Show(DataWorker.AddNewWordInDB(WordInEnglishTextBox.Text, WordInRussianTextBox.Text, ExampleTextBox.Text));
                WordInEnglishTextBox.Text = "";
                WordInRussianTextBox.Text = "";
                ExampleTextBox.Text = "";
            } else
            {
                MessageBox.Show("Enter english word and translation");
            }
        }
    }
}
