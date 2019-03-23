using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestorUI
{
    public abstract class PageViewModel : BaseViewModel
    {

        private bool _isActive;




        public string Header { get; set; }


        /// <summary>
        /// Indicates if the Page is currently active.
        /// </summary>
        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                OnStatusChanged?.Invoke(this, null);
            }
        }

        /// <summary>
        /// Triggered when the Page status is changed (Active or not).
        /// </summary>
        public event EventHandler OnStatusChanged;
    }
}
