using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cheremushkinae_107d2.Model
{
    public class USER
    {
        public int ID_user { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public virtual ICollection<LearnDict> LearnDict { get; set; }
        public virtual ICollection<KnowDict> KnowDict { get; set; }
    }
}
