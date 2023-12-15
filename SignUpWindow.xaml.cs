using Cheremushkinae_107d2.ViewModel;
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
    /// Логика взаимодействия для SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window
    {
        public SignUpWindow()
        {
            InitializeComponent();
            DataContext = new DataManageVM();
            Closing += SignUpWindow_Closing;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen; 
        }

        

        private void GoToSignInWindow_Click(object sender, RoutedEventArgs e)
        {
            SignInWindow signInWindow = Owner as SignInWindow;
            if (signInWindow != null)
            {
                signInWindow.Show();
                this.Hide();
            }
            else
            {
                signInWindow = new SignInWindow();
                signInWindow.Owner = this;
                signInWindow.Show();
                this.Hide();
            }
        }

        private void SignUpWindow_Closing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {

            DataManageVM dataContext = DataContext as DataManageVM;
            if (!((dataContext.PublicUsername == null) || (dataContext.PublicUsername.Replace(" ", string.Empty).Length == 0) ||
                    (dataContext.PublicPassword == null) || (dataContext.PublicPassword.Replace(" ", string.Empty).Length == 0) ||
                    (dataContext.PublicEmail == null) || (dataContext.PublicEmail.Replace(" ", string.Empty).Length == 0)
                    ))
            {
                // Убираю кнопки регистрации и входа в аккаунт, показываю ник в MainWindow
                MainWindow mainWindow = Owner as MainWindow;
                mainWindow.UsernameLabel.Visibility = Visibility.Visible;
                mainWindow.UsernameLabel.Content = dataContext.PublicUsername;
                mainWindow.SignUpMain.Visibility = Visibility.Collapsed;
                mainWindow.SignInMain.Visibility = Visibility.Collapsed;
                // показываю ник в AddNewWordWindow
                AddNewWordWindow addNewWordWindow = new AddNewWordWindow();
                addNewWordWindow.UsernameLabel.Content = dataContext.PublicUsername;
                // показываю ник в LearningWindow
                LearningWindow learningWindow = new LearningWindow();
                learningWindow.UsernameLabel.Content = dataContext.PublicUsername;
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
        }
    }
}
