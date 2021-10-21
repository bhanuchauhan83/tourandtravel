using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Orders;
using Nop.Data;
using Nop.Services.Catalog;
using Nop.Services.Orders;
using Nop.Widget.TourAndTravelHomePage.Domain;
using Nop.Widget.TourAndTravelHomePage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TourAndTravelHomePage.Services
{
    public class PackageService : IPackageService
    {
        #region Properties
        private readonly IProductTagService _productTagService;
        private readonly IProductService _productService;
        private readonly IRepository<ProductProductTagMapping> _productTagMappingRepository;
        private readonly IRepository<CustomerPackageMapping> _customerPackageMappingRepository;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        #endregion

        #region Constructor
        public PackageService(IProductTagService productTagService,
            IProductService productService,
            IRepository<ProductProductTagMapping> productTagMappingRepository,
            IRepository<CustomerPackageMapping> customerPackageMappingRepository,
            IShoppingCartService shoppingCartService, IWorkContext workContext,
            IStoreContext storeContext)
        {
            _productTagService = productTagService;
            _productService = productService;
            _productTagMappingRepository = productTagMappingRepository;
            _customerPackageMappingRepository = customerPackageMappingRepository;
            _shoppingCartService = shoppingCartService;
            _workContext = workContext;
            _storeContext = storeContext;
        }
        #endregion

        #region Methods
        public async Task<IPagedList<Product>> SearchPackages(string destination, int pageIndex, int pageSize)
        {
            var productTags = await _productTagService.GetAllProductTagsAsync(destination);
            List<int> productIds = new List<int>();
            foreach (var tags in productTags)
            {
                productIds.AddRange(_productTagMappingRepository.Table.Where(x => x.ProductTagId == tags.Id).Select(x => x.ProductId).ToList());
            }
            productIds = productIds.Distinct().ToList();
            var products = await _productService.GetProductsByIdsAsync(productIds.ToArray());
            return new PagedList<Product>(products, pageIndex, pageSize);
        }

        public async Task SaveOrUpdateCustomerPackageMapping(TravellerInfo travellerInfo)
        {
            var exists = _customerPackageMappingRepository.Table.FirstOrDefault(x => x.CustomerID == travellerInfo.CustomerId);
            if (exists == null)
            {
                await _customerPackageMappingRepository.InsertAsync(new CustomerPackageMapping()
                {
                    Adults = travellerInfo.Adults,
                    CartId = travellerInfo.CartId,
                    Children = travellerInfo.Children,
                    CustomerID = travellerInfo.CustomerId,
                    DateOfTravelling = travellerInfo.TravellingDate,
                    LocationFrom = travellerInfo.TravellingFrom,
                    LocationTo = travellerInfo.TravellingTo,
                    OrderId = null,
                    PersonsInfo = travellerInfo.PersonsInfoXML
                });
            }
            else
            {
                exists.Adults = travellerInfo.Adults;
                exists.CartId = travellerInfo.CartId;
                exists.Children = travellerInfo.Children;
                exists.CustomerID = travellerInfo.CustomerId;
                exists.DateOfTravelling = travellerInfo.TravellingDate;
                exists.LocationFrom = travellerInfo.TravellingFrom;
                exists.LocationTo = travellerInfo.TravellingTo;
                exists.OrderId = null;
                exists.PersonsInfo = travellerInfo.PersonsInfoXML;
                await _customerPackageMappingRepository.UpdateAsync(exists);
            }
        }

        public async Task MigrateShoppingCartAsync(Customer fromCustomer, Customer toCustomer)
        {
            var exists = _customerPackageMappingRepository.Table.FirstOrDefault(x => x.CustomerID == fromCustomer.Id && x.OrderId == null);
            if (exists != null)
            {
                var cart = await _shoppingCartService.GetShoppingCartAsync(toCustomer, ShoppingCartType.ShoppingCart, (await _storeContext.GetCurrentStoreAsync()).Id);
                exists.CustomerID = toCustomer.Id;
                exists.CartId = cart.FirstOrDefault().Id;
                await _customerPackageMappingRepository.UpdateAsync(exists);
            }
        }

        public async Task<CustomerPackageMapping> GetCurrentCustomerPackage(Customer customer)
        {
            var cart = await _shoppingCartService.GetShoppingCartAsync(customer, ShoppingCartType.ShoppingCart, (await _storeContext.GetCurrentStoreAsync()).Id);
            return _customerPackageMappingRepository.Table.FirstOrDefault(x => x.CustomerID == customer.Id && x.CartId == cart.FirstOrDefault().Id && x.OrderId == null);
        }

        public async Task UpdatePackage(CustomerPackageMapping package)
        {
            await _customerPackageMappingRepository.UpdateAsync(package);
        }

        public async Task MarkCustomerCartAsOrdered(Customer customer, Order placedOrder)
        {
            var package = _customerPackageMappingRepository.Table.FirstOrDefault(x => x.CustomerID == customer.Id && x.OrderId == null);
            if (package != null)
            {
                package.OrderId = placedOrder.Id;
                await UpdatePackage(package);
            }
        }

        public async Task<CustomerPackageMapping> GetTrvellerDetailsByOrderIdAsync(int orderId)
        {
            return await _customerPackageMappingRepository.Table.FirstOrDefaultAsync(x => x.OrderId == orderId);
        }
        #endregion
    }
}
