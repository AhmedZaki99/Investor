using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Investor.UI.Core.ViewModels
{

    /// <summary>
    /// Provides a top-level abstraction for view models in general.
    /// </summary>
    public interface IViewModel : INotifyPropertyChanged, INotifyPropertyChanging, INotifyDataErrorInfo
    {
        /// <inheritdoc cref="INotifyDataErrorInfo.GetErrors(string?)"/>
        new IEnumerable<ValidationResult> GetErrors(string? propertyName = null);
    }

    
    /// <summary>
    /// Provides a top-level abstraction for view models in general, wrapping a model of type <see cref="TModel"/>.
    /// </summary>
    public interface IViewModel<TModel> : IViewModel
    {
        TModel GetModel();
    }

}
