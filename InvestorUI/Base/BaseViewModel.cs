using PropertyChanged;
using System.ComponentModel;

namespace InvestorUI
{
    /// <summary>
    /// A base class that fires Property Changed events as needed
    /// </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The event that is fired when any child property changes its value
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
    }
}
