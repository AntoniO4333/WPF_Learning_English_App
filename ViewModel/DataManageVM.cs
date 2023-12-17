﻿using Cheremushkinae_107d2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Cheremushkinae_107d2.ViewModel
{
    internal class DataManageVM : INotifyPropertyChanged
    {
        // all learning words
        // private List<LearnDict> allLearnWords = DataWorker.GetAllLearningWords();
        //public List<LearnDict> AllLearnWords
        //{
        //  get { return allLearnWords; }
        //    set
        //    {
        //        allLearnWords = value;
        //        NotifyPropertyChanged("AllLearnWords");
        //    }
        //}

        // all know words
        private List<KnowDict> allKnowWords = DataWorker.GetAllKnowWords(GlobalSettings.SavedUserID);
        public List<KnowDict> AllKnowWords
        {
            get { return allKnowWords; }
            set
            {
                allKnowWords = value;
                NotifyPropertyChanged("AllKnowWords");
            }
        }

        public string PublicUsername { get; set; }
        public string PublicPassword { get; set; }
        public string PublicEmail { get; set; }

        #region COMMANDS TO ADD
        private RelayCommand addNewUser;
        public RelayCommand AddNewUser
        {
            get
            {
                return addNewUser ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if ((PublicUsername == null) || (PublicUsername.Replace(" ", string.Empty).Length == 0) ||
                    (PublicPassword == null) || (PublicPassword.Replace(" ", string.Empty).Length == 0) ||
                    (PublicEmail == null) || (PublicEmail.Replace(" ", string.Empty).Length == 0)
                    )
                    {
                        SetRedBlockControl(wnd, "YourNameTextBox");
                        SetRedBlockControl(wnd, "YourPasswordTextBox");
                        SetRedBlockControl(wnd, "YourEmailTextBox");
                    } else
                    {
                        GlobalSettings.SavedUsername = PublicUsername;
                        resultStr = DataWorker.CreateUserInDB(PublicUsername, PublicPassword, PublicEmail);
                        MessageBox.Show(resultStr);
                    }
                }
                );
            }
        }
        #endregion

        #region COMMANDS TO DELETE
        private RelayCommand deleteUser;
        public RelayCommand DeleteUser
        {
            get
            {
                return deleteUser ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "Ti kak v nastroyki bez registratsii zashel?";
                    if (GlobalSettings.SavedUsername == null)
                    {
                        MessageBox.Show(resultStr);
            }
                    else
                    {
                        MessageBox.Show(GlobalSettings.SavedUsername);
                        resultStr = DataWorker.DeleteUserInDB(GlobalSettings.SavedUsername);
                        GlobalSettings.SavedUsername = null;
                        GlobalSettings.SavedUserID = 0;
                        MessageBox.Show(resultStr);
                    }
                }
                );
            }
        }
        #endregion
        private void SetRedBlockControl(Window wnd, string blockName)
        {
            Control block = wnd.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
