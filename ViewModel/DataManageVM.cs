using Cheremushkinae_107d2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using MyGlobalVarAndSettings;

namespace Cheremushkinae_107d2.ViewModel
{
    internal class DataManageVM : INotifyPropertyChanged
    {

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
