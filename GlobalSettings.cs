using Cheremushkinae_107d2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cheremushkinae_107d2
{
    public class GlobalSettings
    {
        public static int SavedUserID { get; set; }
        public static int SavedLearnWordsCount { get; set; }
        public static string SavedUsername { get; set; }
        public static LearnDict SavedCurrentWord { get; set; }
    }
}
