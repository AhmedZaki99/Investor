using InvestorData;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Investor.UI.Core.ViewModels
{
    public class ProductViewModel : ViewModelBase<Product>, IProductViewModel
    {

        #region Public Properties

        public bool IsModified { get; private set; }

        #endregion


        #region Observable Properties

        #region Basic Information

        public bool IsService
        {
            get => Model.IsService;
            set => SetProperty(Model.IsService, value, Model, (model, val) => model.IsService = val, true);
        }

        [Required(ErrorMessage = "Product name is required.")]
        [MaxLength(200, ErrorMessage = "{0} length shouldn't exceed {1} characters.")]
        public string Name
        {
            get => Model.Name;
            set => SetProperty(Model.Name, value, Model, (model, val) => model.Name = val, true);
        }

        [MaxLength(60, ErrorMessage = "{0} length shouldn't exceed {1} characters.")]
        public string? Code
        {
            get => Model.Code;
            set => SetProperty(Model.Code, value, Model, (model, val) => model.Code = val, true);
        }

        private ICategoryViewModel? _category;
        public ICategoryViewModel? Category
        {
            get 
            {
                if (_category is not null)
                {
                    return _category;
                }
                var category = Model.Category is not null
                    ? new CategoryViewModel(Model.Category)
                    : null;

                SetProperty(ref _category, category);
                return category;
            } 
            set
            {
                if (SetProperty(ref _category, value))
                {
                    Model.Category = value?.GetModel();
                    Model.CategoryId = value?.GetModel().Id;
                }
            }
        }

        #endregion


        #region Sales Information

        private IAccountViewModel? _salesAccount;
        public IAccountViewModel? SalesAccount
        {
            get => _salesAccount ??= Model.SalesInformation?.Account is not null 
                ? new AccountViewModel(Model.SalesInformation.Account) 
                : null;
            set
            {
                _salesAccount = value;

                Model.SalesInformation = value is null
                ? null
                : new TradingInfo
                {
                    Account = value.GetModel(),
                    AccountId = value.GetModel().Id
                };
            }
        }

        public decimal? SalesPrice
        {
            get => Model.SalesInformation?.Price;
            set => SetProperty(Model.SalesInformation?.Price, value, Model, (model, val) =>
            {
                if (model.SalesInformation is not null && val.HasValue)
                {
                    model.SalesInformation.Price = val.Value;
                }
            }, true);
        }

        [MaxLength(1000, ErrorMessage = "{0} length shouldn't exceed {1} characters.")]
        public string? SalesDescription
        {
            get => Model.SalesInformation?.Description;
            set => SetProperty(Model.SalesInformation?.Description, value, Model, (model, val) =>
            {
                if (model.SalesInformation is not null)
                {
                    model.SalesInformation.Description = val;
                }
            }, true);
        }

        #endregion

        #region Purchasing Information

        private IAccountViewModel? _purchasingAccount;
        public IAccountViewModel? PurchasingAccount
        {
            get => _purchasingAccount ??= Model.PurchasingInformation?.Account is not null 
                ? new AccountViewModel(Model.PurchasingInformation.Account) 
                : null;
            set
            {
                _purchasingAccount = value;

                Model.PurchasingInformation = value is null
                ? null
                : new TradingInfo
                {
                    Account = value.GetModel(),
                    AccountId = value.GetModel().Id
                };
            }
        }

        public decimal? PurchasingPrice
        {
            get => Model.PurchasingInformation?.Price;
            set => SetProperty(Model.PurchasingInformation?.Price, value, Model, (model, val) =>
            {
                if (model.PurchasingInformation is not null && val.HasValue)
                {
                    model.PurchasingInformation.Price = val.Value;
                }
            }, true);
        }

        [MaxLength(1000, ErrorMessage = "{0} length shouldn't exceed {1} characters.")]
        public string? PurchasingDescription
        {
            get => Model.PurchasingInformation?.Description;
            set => SetProperty(Model.PurchasingInformation?.Description, value, Model, (model, val) =>
            {
                if (model.PurchasingInformation is not null)
                {
                    model.PurchasingInformation.Description = val;
                }
            }, true);
        }

        #endregion


        #region Inventory Information

        // UNDONE: Add ScaleUnit.

        private IAccountViewModel? _inventoryAccount;
        public IAccountViewModel? InventoryAccount
        {
            get => _inventoryAccount ??= Model.InventoryDetails?.InventoryAccount is not null 
                ? new AccountViewModel(Model.InventoryDetails.InventoryAccount) 
                : null;
            set
            {
                _inventoryAccount = value;

                Model.InventoryDetails = value is null
                ? null
                : new InventoryInfo
                {
                    InventoryAccount = value.GetModel(),
                    InventoryAccountId = value.GetModel().Id
                };
            }
        }

        public double? Quantity
        {
            get => Model.InventoryDetails?.Quantity;
            set => SetProperty(Model.InventoryDetails?.Quantity, value, Model, (model, val) =>
            {
                if (model.InventoryDetails is not null && val.HasValue)
                {
                    model.InventoryDetails.Quantity = val.Value;
                }
            }, true);
        }

        public double? ReorderPoint
        {
            get => Model.InventoryDetails?.ReorderPoint;
            set => SetProperty(Model.InventoryDetails?.ReorderPoint, value, Model, (model, val) =>
            {
                if (model.InventoryDetails is not null && val.HasValue)
                {
                    model.InventoryDetails.ReorderPoint = val.Value;
                }
            }, true);
        }

        [MaxLength(1000, ErrorMessage = "{0} length shouldn't exceed {1} characters.")]
        public string? SKU
        {
            get => Model.InventoryDetails?.SKU;
            set => SetProperty(Model.InventoryDetails?.SKU, value, Model, (model, val) =>
            {
                if (model.InventoryDetails is not null)
                {
                    model.InventoryDetails.SKU = val;
                }
            }, true);
        }

        #endregion

        #endregion


        #region Constructor

        public ProductViewModel() : this(new())
        { }

        public ProductViewModel(Product model) : base(model)
        {
            IsModified = false;
        }

        #endregion


        #region Overridden Methods

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            IsModified = true;

            base.OnPropertyChanged(e);
        }

        #endregion

    }
}
