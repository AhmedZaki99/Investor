using AutoMapper;
using CommunityToolkit.Mvvm.Input;
using Investor.Core;
using InvestorData;
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

        private string _localStatus;
        public string LocalStatus
        {
            get => _localStatus;
            set => SetProperty(ref _localStatus, value);
        }


        private ObservableCollection<ICategoryViewModel> _categories;
        public ObservableCollection<ICategoryViewModel> Categories
        {
            get => _categories;
            set => SetProperty(ref _categories, value);
        }

        private ObservableCollection<IAccountViewModel> _accounts;
        public ObservableCollection<IAccountViewModel> Accounts
        {
            get => _accounts;
            set => SetProperty(ref _accounts, value);
        }


        private ObservableCollection<IProductViewModel> _products;
        public ObservableCollection<IProductViewModel> Products
        {
            get => _products;
            set => SetProperty(ref _products, value);
        }

        private IProductViewModel? _selectedProduct;
        public IProductViewModel? SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                // Hook event listeners to notify commands, if new product was selected.
                NotifyCanSaveProductOnFirstEdit(ref _selectedProduct, value);
                NotifyOnProductErrorsChanged(ref _selectedProduct, value);

                if (SetProperty(ref _selectedProduct, value))
                {
                    UpdateProductView();
                }
            }
        }

        public IEnumerable<string?> InputErrors
        {
            get => SelectedProduct?.GetErrors().Select(err => $"*{err.ErrorMessage}") ?? Enumerable.Empty<string>();
        }

        private bool _addingNewProduct;
        public bool AddingNewProduct
        {
            get => _addingNewProduct;
            set
            {
                if (SetProperty(ref _addingNewProduct, value))
                {
                    if (value)
                    {
                        SelectedProduct = new ProductViewModel();
                    }
                    else SelectedProduct = null;
                }
            }
        }


        #endregion

        #region Commands

        public ICommand CloseApplicationCommand { get; private set; }

        public ICommand ToggleAddProductCommand { get; private set; }

        public ICommand GetProductsCommand { get; private set; }
        public IRelayCommand AddProductCommand { get; private set; }
        public IRelayCommand SaveProductCommand { get; private set; }
        public IRelayCommand DeleteProductCommand { get; private set; }


        #endregion


        #region Dependencies

        private readonly IHostApplicationLifetime _applicationLifetime;
        private readonly ILogger<MainViewModel> _logger;

        private readonly IProductClient _productClient;
        private readonly ICategoryClient _categoryClient;
        private readonly IAccountClient _accountClient;
        private readonly IBusinessClient _businessClient;

        private readonly IMapper _mapper;

        #endregion

        #region Constructors

        public MainViewModel(IHostApplicationLifetime applicationLifetime,
                             ILogger<MainViewModel> logger,
                             IProductClient productClient,
                             ICategoryClient categoryClient,
                             IAccountClient accountClient,
                             IBusinessClient businessClient,
                             IMapper mapper)
        {
            // Dependencies.
            _applicationLifetime = applicationLifetime;
            _logger = logger;
            _mapper = mapper;
            _productClient = productClient;
            _categoryClient= categoryClient;
            _accountClient= accountClient;
            _businessClient = businessClient;

            // Properites
            _categories = new();
            _accounts = new();
            _products = new();
            _localStatus = "Ready.";
            _addingNewProduct = false;

            InitializeCommands();
        }

        #endregion

        #region Initialization

        [MemberNotNull(nameof(CloseApplicationCommand))]
        [MemberNotNull(nameof(ToggleAddProductCommand), nameof(AddProductCommand))]
        [MemberNotNull(nameof(GetProductsCommand), nameof(SaveProductCommand), nameof(DeleteProductCommand))]
        private void InitializeCommands()
        {
            CloseApplicationCommand = new RelayCommand(CloseApplication);

            ToggleAddProductCommand = new RelayCommand(ToggleAddProduct);

            GetProductsCommand = new AsyncRelayCommand(GetProductsAsync);
            AddProductCommand = new AsyncRelayCommand(AddProductAsync, CanAddProduct);
            SaveProductCommand = new AsyncRelayCommand(SaveProductAsync, CanSaveProduct);
            DeleteProductCommand = new AsyncRelayCommand(DeleteProductAsync, CanDeleteProduct);
        }

        #endregion

        #region Data

        public async Task FetchData()
        {
            var categories = await _categoryClient.GetAllAsync();
            Categories = new(categories.Select(c => (ICategoryViewModel)new CategoryViewModel(c)));

            var accounts = await _accountClient.GetAllAsync();
            Accounts = new(accounts.Select(a => (IAccountViewModel)new AccountViewModel(a)));
        }

        #endregion


        #region Application Actions

        private void CloseApplication()
        {
            _applicationLifetime.StopApplication();
        }

        #endregion

        #region Products Actions

        private void ToggleAddProduct()
        {
            AddingNewProduct = !AddingNewProduct;
            if (!AddingNewProduct)
            {
                LocalStatus = "Ready.";
            }
        }

        private async Task GetProductsAsync()
        {
            LocalStatus = "Getting products...";
            try
            {
                var products = await _productClient.GetAllAsync();
                Products = new(products.Select(p => new ProductViewModel(p)));

                LocalStatus = "Up to date.";
            }
            catch (ApiConnectionException ex)
            {
                LogApiConnectionError(ex);
                LocalStatus = "Failed to get products, please check your internet connection and try again.";
            }
        }

        private async Task AddProductAsync()
        {
            if (SelectedProduct is null)
            {
                throw new NullReferenceException("Couldn't resolve the product reference to add..");
            }

            LocalStatus = "Creating product...";
            try
            {
                var dto = _mapper.Map<ProductCreateInputDto>(SelectedProduct.GetModel());
                await FakeData(dto);

                var product = await _productClient.CreateAsync(dto);

                AddingNewProduct = false;
                LocalStatus = $"Product has been created successfully with id: {product.Id}.";

                await GetProductsAsync();
            }
            catch (ApiConnectionException ex)
            {
                LogApiConnectionError(ex);
                LocalStatus = "Failed to create the product, please check your internet connection and try again.";
            }
        }

        private async Task FakeData(ProductCreateInputDto dto)
        {
            var businesses = await _businessClient.GetAllAsync();

            dto.BusinessId = businesses.First().Id;
            dto.Category = null;
        }

        private async Task SaveProductAsync()
        {
            if (SelectedProduct is null)
            {
                throw new NullReferenceException("Couldn't resolve the product reference to save changes to..");
            }

            LocalStatus = "Saving product...";
            try
            {
                var dto = _mapper.Map<ProductUpdateInputDto>(SelectedProduct.GetModel());

                dto.Category = null;

                var product = await _productClient.SaveChangesAsync(SelectedProduct.GetModel().Id, dto);
                if (product is not null)
                {
                    LocalStatus = "Changes saved successfully.";
                }
                else LocalStatus = "Something went wrong while trying to save changes..";

                OnPropertyChanged(nameof(SelectedProduct));
            }
            catch (ApiConnectionException ex)
            {
                LogApiConnectionError(ex);
                LocalStatus = "Failed to save changes to the product, please check your internet connection and try again.";
            }
        }

        private async Task DeleteProductAsync()
        {
            if (SelectedProduct is null)
            {
                throw new NullReferenceException("Couldn't resolve the product reference to delete..");
            }

            LocalStatus = "Deleting product...";
            try
            {
                var result = await _productClient.DeleteAsync(SelectedProduct.GetModel().Id);
                if (result)
                {
                    SelectedProduct = null;
                    LocalStatus = "Product has been deleted successfully.";

                    await GetProductsAsync();
                }
                else LocalStatus = "Something went wrong while trying to delete the product..";
            }
            catch (ApiConnectionException ex)
            {
                LogApiConnectionError(ex);
                LocalStatus = "Failed to delete the product, please check your internet connection and try again.";
            }
        }


        #region CanExecute Actions

        private bool CanAddProduct() => SelectedProduct is not null && !SelectedProduct.HasErrors;
        private bool CanSaveProduct() => SelectedProduct is not null && SelectedProduct.IsModified && !SelectedProduct.HasErrors;
        private bool CanDeleteProduct() => SelectedProduct is not null;

        #endregion

        #endregion


        #region Helper Methods

        private void LogApiConnectionError(ApiConnectionException ex)
        {
            _logger.LogWarning(
                "Failed to connect to the Api server, failure type: {failureType}, status code: {statusCode}, error message: {errMsg}",
                ex.FailureType, ex.StatusCode is null ? "N/A" : ex.StatusCode, ex.Message);
        }


        #region Selected Product On Change

        private void NotifyCanSaveProductOnFirstEdit(ref IProductViewModel? oldProduct, IProductViewModel? newProduct)
        {
            if (oldProduct == newProduct) return;

            void productModifiedHandler(object? sender, PropertyChangedEventArgs e)
            {
                // Call handler only once..
                if (sender is IProductViewModel product)
                {
                    product.PropertyChanged -= productModifiedHandler;
                }
                SaveProductCommand.NotifyCanExecuteChanged();
                LocalStatus = "Changes made not yet saved!";
            }

            if (newProduct?.IsModified == false)
            {
                newProduct.PropertyChanged += productModifiedHandler;
            }
            else if (oldProduct is not null)
            {
                oldProduct.PropertyChanged -= productModifiedHandler;
            }
        }
        
        private void NotifyOnProductErrorsChanged(ref IProductViewModel? oldProduct, IProductViewModel? newProduct)
        {
            if (oldProduct == newProduct) return;

            void productErrorsHandler(object? sender, DataErrorsChangedEventArgs e)
            {
                UpdateProductView();
            }

            if (newProduct is not null)
            {
                newProduct.ErrorsChanged += productErrorsHandler;
            }
            else if (oldProduct is not null)
            {
                oldProduct.ErrorsChanged -= productErrorsHandler;
            }
        }

        private void UpdateProductView()
        {
            SaveProductCommand.NotifyCanExecuteChanged();
            DeleteProductCommand.NotifyCanExecuteChanged();
            AddProductCommand.NotifyCanExecuteChanged();
            OnPropertyChanged(nameof(InputErrors));
        }
        
        #endregion

        #endregion

    }
}
