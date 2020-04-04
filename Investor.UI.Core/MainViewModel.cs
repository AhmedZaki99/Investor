using System.Collections.Generic;
using System.Windows.Input;

namespace Investor.UI.Core
{
    public class MainViewModel : BaseViewModel
    {
        
        #region Public Properties

        /// <summary>
        /// The current active page.
        /// </summary>
        public PageViewModel ActivePage { get; set; }

        /// <summary>
        /// Pages included in the main view.
        /// </summary>
        public List<PageViewModel> Pages { get; set; }

        /// <summary>
        /// Change the view to a given page.
        /// </summary>
        public ICommand ChangePageCommand { get; private set; }

        #endregion


        #region Constructor

        public MainViewModel()
        {
            InitializeCommands();

            InitializePages();
        }

        #endregion

        #region Initialization

        private void InitializeCommands()
        {
            ChangePageCommand = new RelayCommand(ChangePage);

        }

        private void InitializePages()
        {
            Pages = new List<PageViewModel>()
            {
                new DashboardViewModel(),
                new OperationsViewModel(),
                new TransactionsViewModel(),
                new InventoryViewModel(),
                new InvoicesViewModel()
            };
        }

        #endregion


        #region Commands Actions

        private void ChangePage(object obj)
        {
            var page = (PageViewModel)obj;

            if (ActivePage == page) return;
            if (ActivePage != null)
            {
                ActivePage.IsActive = false;
            }
            page.IsActive = true;

            ActivePage = page;
        }

        #endregion

    }
}
