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

namespace Cheremushkinae_107d2
{
    /// <summary>
    /// Логика взаимодействия для SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        public SignInWindow()
        {
            InitializeComponent();
            Closing += SignInWindow_Closing;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            if(DataWorker.SignInDB(this.YourNameTextBox.Text, this.YourPasswordTextBox.Text) == "Вход произведен успешно")
            {
                MessageBox.Show("Вход произведен успешно");
                MainWindow mainWindow = Owner as MainWindow;
                AddNewWordWindow addNewWordWindow = new AddNewWordWindow();
                addNewWordWindow.UsernameLabel.Content = GlobalSettings.SavedUsername;
                LearningWindow learningWindow = new LearningWindow();
                learningWindow.UsernameLabel.Content = GlobalSettings.SavedUsername;
                if (mainWindow != null)
                {
                    mainWindow.SignInMain.Visibility = Visibility.Collapsed;
                    mainWindow.SignUpMain.Visibility = Visibility.Collapsed;
                    mainWindow.UsernameLabel.Visibility = Visibility.Visible;
                    mainWindow.UsernameLabel.Content = GlobalSettings.SavedUsername;
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
            } else
            {
                MessageBox.Show("Пользователя с таким ником не существует либо введен неверный пароль");

            }
        }

        private void GoToSignUpWindow_Click(object sender, RoutedEventArgs e)
        {
            SignUpWindow signUpWindow = Owner as SignUpWindow;
            if (signUpWindow != null)
            {
                signUpWindow.Show();
                this.Hide();
            }
            else
            {
                signUpWindow = new SignUpWindow();
                signUpWindow.Owner = this;
                signUpWindow.Show();
                this.Hide();
            }
        }

        private void SignInWindow_Closing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
