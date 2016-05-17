using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmer.Social
{
    public class SocialExeception:Exception
    {
        public SocialExeception(string msg)
            : base(msg)
        {

        }
    }
}
