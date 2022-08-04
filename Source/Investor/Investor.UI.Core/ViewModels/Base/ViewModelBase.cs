using CommunityToolkit.Mvvm.ComponentModel;

namespace Investor.UI.Core.ViewModels
{

    /// <summary>
    /// A base class that provides functionality for objects observation, validation,
    /// and other necessary tasks for view models.
    /// </summary>
    public abstract class ViewModelBase : ObservableValidator, IViewModel
    {

    }


    /// <summary>
    /// A base class that provides functionality for objects observation, validation,
    /// and other necessary tasks for view models; wrapping a non-observable model.
    /// </summary>
    /// <typeparam name="TModel">Type of the model.</typeparam>
    public abstract class ViewModelBase<TModel> : ViewModelBase
    {

        protected TModel Model { get; }

        public ViewModelBase(TModel model)
        {
            Model = model;
        }

    }

}
