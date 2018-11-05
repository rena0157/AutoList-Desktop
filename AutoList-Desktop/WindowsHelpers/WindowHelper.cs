using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutoList_Desktop.WindowsHelpers
{
    public static class WindowHelper
    {
        public static Thickness AddThickness(Thickness t1, Thickness t2)
        {
            return new Thickness(
                t1.Left + t2.Left, t1.Top + t2.Top, t1.Right + t2.Right, t1.Bottom + t2.Bottom);
        }
    }
}
