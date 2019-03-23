using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace InvestorUI
{
    public class MainViewModel : BaseViewModel
    {

        /// <summary>
        /// The current active page.
        /// </summary>
        public PageViewModel CurrentPage { get; set; }

        /// <summary>
        /// Pages included in the main view.
        /// </summary>
        public List<PageViewModel> Pages { get; set; }

        /// <summary>
        /// Change the view to a given page.
        /// </summary>
        public ICommand ChangePageCommand { get; private set; }



        public MainViewModel()
        {
            InitializeCommands();

            InitializePages();
        }


        private void InitializeCommands()
        {
            ChangePageCommand = new RelayCommand(page => ChangePage((PageViewModel)page), page => page is PageViewModel);
            
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



        private void ChangePage(PageViewModel page)
        {
            if (CurrentPage != null) CurrentPage.IsActive = false;
            page.IsActive = true;

            CurrentPage = page;
        }
    }
}
