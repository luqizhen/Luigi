using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.ViewModel
{
    public class ViewModelLocator
    {
        private static MainViewModel mainViewModel;

        public static MainViewModel MainViewModel
        {
            get
            {
                if (mainViewModel == null)
                {
                    mainViewModel = new MainViewModel();
                }
                return mainViewModel;
            }
        }
    }
}
