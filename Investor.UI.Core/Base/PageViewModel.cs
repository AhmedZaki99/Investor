using System;

namespace Investor.UI.Core
{
    public abstract class PageViewModel : BaseViewModel
    {

        #region Private Fields

        private bool _isActive;

        #endregion


        #region Public Properties
        
        /// <summary>
        /// Gets or sets the header of this pages.
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates if the Page is currently active or not.
        /// </summary>
        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                OnStatusChanged(this, new EventArgs());
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Triggered when the Page status is changed (Active or not).
        /// </summary>
        public event EventHandler StatusChanged;

        #endregion


        #region Constructor

        public PageViewModel()
        {
            
        }

        #endregion


        #region Events Default Handlers

        protected virtual void OnStatusChanged(object sender, EventArgs e)
        {
            StatusChanged?.Invoke(sender, e);
        }

        #endregion

    }
}
