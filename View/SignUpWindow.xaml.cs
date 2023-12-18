using Cheremushkinae_107d2.ViewModel;
using Cheremushkinae_107d2.Model;
using System.ComponentModel;
using System.Windows;
using MyGlobalVarAndSettings;

namespace Cheremushkinae_107d2
{
    /// <summary>
    /// Логика взаимодействия для SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window
    {
        private static SignUpWindow _instance;

        private SignUpWindow()
        {
            InitializeComponent();
            DataContext = new DataManageVM();
            Closing += SignUpWindow_Closing;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        public static SignUpWindow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SignUpWindow();
                }
                return _instance;
            }
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
                MessageBox.Show(DataWorker.CreateUserInDB(dataContext.PublicUsername, dataContext.PublicPassword, dataContext.PublicEmail));
                MainWindow mainWindow = Owner as MainWindow;
                // на всякий случай, вдруг пользователь захочет удалить аккаунт и создать новый
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
            }
        }
    }

}

