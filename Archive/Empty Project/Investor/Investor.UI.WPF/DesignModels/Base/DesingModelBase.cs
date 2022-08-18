using Investor.UI.Core.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Investor.UI.WPF
{

    /// <summary>
    /// An empty view model that acts as a base for design-time instances,
    /// and contains dummy implemetnation for <see cref="IViewModel"/> interface.
    /// </summary>
    public abstract class DesingModelBase : IViewModel
    {
        public bool HasErrors => throw new NotImplementedException();

#pragma warning disable 67
        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
#pragma warning restore 67

        public IEnumerable<ValidationResult> GetErrors(string? propertyName = null)
        {
            throw new NotImplementedException();
        }

        IEnumerable INotifyDataErrorInfo.GetErrors(string? propertyName)
        {
            throw new NotImplementedException();
        }
    }


    /// <summary>
    /// An empty view model that acts as a base for design-time instances,
    /// and contains dummy implemetnation for <see cref="IViewModel"/> interface,
    /// wrapping a model of type <see cref="TModel"/>.
    /// </summary>
    public abstract class DesingModelBase<TModel> : DesingModelBase, IViewModel<TModel>
    {
        public TModel GetModel()
        {
            throw new NotImplementedException();
        }
    }

}
