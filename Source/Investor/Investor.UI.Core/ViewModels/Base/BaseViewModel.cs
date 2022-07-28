using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.ComponentModel;

namespace Investor.UI.Core.ViewModels
{
    /// <summary>
    /// A base class that provides <see cref="INotifyPropertyChanged"/> implementation,
    /// and basic functionality for view models.
    /// </summary>
    public abstract class BaseViewModel : ObservableValidator
    {

    }
}
