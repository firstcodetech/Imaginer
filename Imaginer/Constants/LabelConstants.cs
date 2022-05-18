using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imaginer.Constants
{
    public class LabelConstants
    {
        private static readonly Dictionary<int, string> _labels = new Dictionary<int, string>
        {
            {1,"Hello" },
            {2, "Search" },
            {3, "Search Image Ex- Tesla Model 3" },
            {4,"Displaying pages" },
            {5,"of" },

        };
        public static Dictionary<int, string> labels { get { return _labels; } }
    }
}
