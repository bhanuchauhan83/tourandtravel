using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Orders;
using Nop.Widget.TourAndTravelHomePage.Domain;
using Nop.Widget.TourAndTravelHomePage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TourAndTravelHomePage.Services
{
    public interface IPackageService
    {
        Task<IPagedList<Product>> SearchPackages(string destination, int pageIndex, int pageSize);
        Task SaveOrUpdateCustomerPackageMapping(TravellerInfo travellerInfo);
        Task MigrateShoppingCartAsync(Customer fromCustomer, Customer toCustomer);
        Task<CustomerPackageMapping> GetCurrentCustomerPackage(Customer customer);
        Task UpdatePackage(CustomerPackageMapping package);
        Task MarkCustomerCartAsOrdered(Customer customer, Order placedOrder);
        Task<CustomerPackageMapping> GetTrvellerDetailsByOrderIdAsync(int orderId);
    }
}
