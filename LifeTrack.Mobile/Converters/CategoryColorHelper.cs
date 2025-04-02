using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeTrack.Mobile.Helpers
{
    public static class CategoryColorHelper
    {
        public static string GetColorForCategory(string colorHex)
        {
            return string.IsNullOrEmpty(colorHex) ? "#CCCCCC" : colorHex;
        }
    }
}
