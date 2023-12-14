﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cheremushkinae_107d2.Model
{
    public class LEARN_DICT
    {
        public int ID_learning_word { get; set; }
        public int ID_user { get; set; }
        public string Word_in_English { get; set; }
        public string Word_in_Russian { get; set; }
        public string Using_example { get; set; }
        public int Right_answers_count { get; set; }

        public virtual User User { get; set; }
    }
}
