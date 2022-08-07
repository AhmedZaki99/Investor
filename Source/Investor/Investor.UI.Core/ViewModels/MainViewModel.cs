using CommunityToolkit.Mvvm.Input;
using Investor.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;

namespace Investor.UI.Core.ViewModels
{
    /// <summary>
    /// The bottom-level view model for application main view.
    /// </summary>
    public class MainViewModel : ViewModelBase, IMainViewModel
    {

        #region Private Fields



        #endregion

        #region Observable Properties

        private ObservableCollection<IBrandViewModel> _brands;
        public ObservableCollection<IBrandViewModel> Brands
        {
            get => _brands;
            set => SetProperty(ref _brands, value);
        }

        private IBrandViewModel? _selectedBrand;
        public IBrandViewModel? SelectedBrand
        {
            get => _selectedBrand;
            set
            {
                NotifyCanSaveBrandOnFirstEdit(ref _selectedBrand, value);
                NotifyOnBrandErrorsChanged(ref _selectedBrand, value);

                if (SetProperty(ref _selectedBrand, value))
                {
                    UpdateBrandView();
                }
            }
        }

        public IEnumerable<string?> InputErrors
        {
            get => SelectedBrand?.GetErrors().Select(err => $"*{err.ErrorMessage}") ?? Enumerable.Empty<string>();
        }


        private bool _addingNewBrand;
        public bool AddingNewBrand
        {
            get => _addingNewBrand;
            set
            {
                if (SetProperty(ref _addingNewBrand, value))
                {
                    if (value)
                    {
                        SelectedBrand = new BrandViewModel(new("New brand"));
                    }
                    else SelectedBrand = null;
                }
            }
        }

        private string _localStatus;
        public string LocalStatus
        {
            get => _localStatus;
            set => SetProperty(ref _localStatus, value);
        }

        #endregion

        #region Commands

        public ICommand CloseApplicationCommand { get; private set; }

        public ICommand ToggleAddBrandCommand { get; private set; }

        public ICommand GetBrandsCommand { get; private set; }
        public IRelayCommand AddBrandCommand { get; private set; }
        public IRelayCommand SaveBrandCommand { get; private set; }
        public IRelayCommand DeleteBrandCommand { get; private set; }


        #endregion


        #region Dependencies

        private readonly IHostApplicationLifetime _applicationLifetime;
        private readonly IBrandEndpoint _brandEndpoint;
        private readonly ILogger<MainViewModel> _logger;

        #endregion

        #region Constructors

        public MainViewModel(IHostApplicationLifetime applicationLifetime, IBrandEndpoint brandEndpoint, ILogger<MainViewModel> logger)
        {
            // Dependencies.
            _applicationLifetime = applicationLifetime;
            _brandEndpoint = brandEndpoint;
            _logger = logger;

            // Properites
            _brands = new();
            _localStatus = "Ready.";
            _addingNewBrand = false;

            InitializeCommands();
        }

        #endregion

        #region Initialization

        [MemberNotNull(nameof(CloseApplicationCommand))]
        [MemberNotNull(nameof(ToggleAddBrandCommand), nameof(AddBrandCommand))]
        [MemberNotNull(nameof(GetBrandsCommand), nameof(SaveBrandCommand), nameof(DeleteBrandCommand))]
        private void InitializeCommands()
        {
            CloseApplicationCommand = new RelayCommand(CloseApplication);

            ToggleAddBrandCommand = new RelayCommand(ToggleAddBrand);

            GetBrandsCommand = new AsyncRelayCommand(GetBrandsAsync);
            AddBrandCommand = new AsyncRelayCommand(AddBrandAsync, CanAddBrand);
            SaveBrandCommand = new AsyncRelayCommand(SaveBrandAsync, CanSaveBrand);
            DeleteBrandCommand = new AsyncRelayCommand(DeleteBrandAsync, CanDeleteBrand);
        }

        #endregion


        #region Application Actions

        private void CloseApplication()
        {
            _applicationLifetime.StopApplication();
        }

        #endregion

        #region Brands Actions

        private void ToggleAddBrand()
        {
            AddingNewBrand = !AddingNewBrand;
        }

        private async Task GetBrandsAsync()
        {
            LocalStatus = "Getting brands...";
            try
            {
                var brands = await _brandEndpoint.PaginateAsync();

                Brands = new(brands.Select(b => new BrandViewModel(b)));
                LocalStatus = "Up to date.";
            }
            catch (ApiConnectionException ex)
            {
                LogApiConnectionError(ex);
                LocalStatus = "Failed to get brands, please check your internet connection and try again.";
            }
        }

        private async Task AddBrandAsync()
        {
            if (SelectedBrand is null)
            {
                throw new NullReferenceException("Couldn't resolve the brand reference to add..");
            }

            LocalStatus = "Creating brand...";
            try
            {
                var brand = await _brandEndpoint.CreateAsync(SelectedBrand.GetModel());

                AddingNewBrand = false;
                LocalStatus = $"Brand has been created successfully with id: {brand.Id}.";

                await GetBrandsAsync();
            }
            catch (ApiConnectionException ex)
            {
                LogApiConnectionError(ex);
                LocalStatus = "Failed to create the brand, please check your internet connection and try again.";
            }
        }

        private async Task SaveBrandAsync()
        {
            if (SelectedBrand is null)
            {
                throw new NullReferenceException("Couldn't resolve the brand reference to save changes to..");
            }

            LocalStatus = "Saving brand...";
            try
            {
                var result = await _brandEndpoint.SaveChangesAsync(SelectedBrand.GetModel());
                if (result)
                {
                    LocalStatus = "Changes saved successfully.";
                }
                else LocalStatus = "Something went wrong while trying to save changes..";

                OnPropertyChanged(nameof(SelectedBrand));
            }
            catch (ApiConnectionException ex)
            {
                LogApiConnectionError(ex);
                LocalStatus = "Failed to save changes to the brand, please check your internet connection and try again.";
            }
        }

        private async Task DeleteBrandAsync()
        {
            if (SelectedBrand is null)
            {
                throw new NullReferenceException("Couldn't resolve the brand reference to delete..");
            }

            LocalStatus = "Deleting brand...";
            try
            {
                var result = await _brandEndpoint.DeleteAsync(SelectedBrand.GetModel().Id);
                if (result)
                {
                    SelectedBrand = null;
                    LocalStatus = "Brand has been deleted successfully.";

                    await GetBrandsAsync();
                }
                else LocalStatus = "Something went wrong while trying to delete the brand..";
            }
            catch (ApiConnectionException ex)
            {
                LogApiConnectionError(ex);
                LocalStatus = "Failed to delete the brand, please check your internet connection and try again.";
            }
        }


        #region CanExecute Actions

        private bool CanAddBrand() => SelectedBrand is not null && !SelectedBrand.HasErrors;
        private bool CanSaveBrand() => SelectedBrand is not null && SelectedBrand.IsModified && !SelectedBrand.HasErrors;
        private bool CanDeleteBrand() => SelectedBrand is not null;

        #endregion

        #endregion


        #region Helper Methods

        private void LogApiConnectionError(ApiConnectionException ex)
        {
            _logger.LogWarning(
                "Failed to connect to the Api server, failure type: {failureType}, status code: {statusCode}, error message: {errMsg}",
                ex.FailureType, ex.StatusCode is null ? "N/A" : ex.StatusCode, ex.Message);
        }


        #region Selected Brand On Change

        private void NotifyCanSaveBrandOnFirstEdit(ref IBrandViewModel? oldBrand, IBrandViewModel? newBrand)
        {
            if (oldBrand == newBrand) return;

            void brandModifiedHandler(object? sender, PropertyChangedEventArgs e)
            {
                if (sender is IBrandViewModel brand)
                {
                    brand.PropertyChanged -= brandModifiedHandler;
                }
                SaveBrandCommand.NotifyCanExecuteChanged();
                LocalStatus = "Changes made not yet saved!";
            }

            if (newBrand?.IsModified == false)
            {
                newBrand.PropertyChanged += brandModifiedHandler;
            }
            else if (oldBrand is not null)
            {
                oldBrand.PropertyChanged -= brandModifiedHandler;
            }
        }
        
        private void NotifyOnBrandErrorsChanged(ref IBrandViewModel? oldBrand, IBrandViewModel? newBrand)
        {
            if (oldBrand == newBrand) return;

            void brandErrorsHandler(object? sender, DataErrorsChangedEventArgs e)
            {
                UpdateBrandView();
            }

            if (newBrand is not null)
            {
                newBrand.ErrorsChanged += brandErrorsHandler;
            }
            else if (oldBrand is not null)
            {
                oldBrand.ErrorsChanged -= brandErrorsHandler;
            }
        }

        private void UpdateBrandView()
        {
            SaveBrandCommand.NotifyCanExecuteChanged();
            DeleteBrandCommand.NotifyCanExecuteChanged();
            AddBrandCommand.NotifyCanExecuteChanged();
            OnPropertyChanged(nameof(InputErrors));
        }
        
        #endregion

        #endregion

    }
}
