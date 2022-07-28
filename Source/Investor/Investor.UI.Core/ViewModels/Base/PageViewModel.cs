namespace Investor.UI.Core.ViewModels
{
    /// <summary>
    /// A base class that provides basic functionality for page view models.
    /// </summary>
    public abstract class PageViewModel : BaseViewModel
    {

        #region Unchanged Properties

        public BaseViewModel Parent { get; }

        #endregion

        #region Observable Properties

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        #endregion


        #region Constructors

        public PageViewModel(BaseViewModel parent, string title = "")
        {
            Parent = parent;

            _title = title;
            _isSelected = false;
        }

        #endregion

        #region Command Actions



        #endregion

    }
}
