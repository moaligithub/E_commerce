using E_commerce.Core.Interfaces.IRepositores;
using E_commerce.Core.Interfaces.IRepositories;
using E_commerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.Interfaces.IUnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        ///     This propertiy expresses User Repository
        /// </summary>
        public IApplicationUserRepository User { get; }

        /// <summary>
        ///     This propertiy expresses User Properties Repository
        /// </summary>
        public IBaseRepository<UserProperties> UserProperties { get; }

        /// <summary>
        ///     This propertiy expresses Admin Properties Repository
        /// </summary>
        public IAdminPropertiesRepository AdminProperties { get; }

        /// <summary>
        ///     This propertiy expresses Branch Repository
        /// </summary>
        public IBaseRepository<Branch> Branch { get; }

        /// <summary>
        ///     This propertiy expresses Cart Repository
        /// </summary>
        public IBaseRepository<Cart> Cart { get; }

        /// <summary>
        ///     This propertiy expresses Category Repository
        /// </summary>
        public IBaseRepository<Category> Category { get; }

        /// <summary>
        ///     This propertiy expresses City Repository
        /// </summary>
        public IBaseRepository<City> City { get; }

        /// <summary>
        ///     This propertiy expresses Country Repository
        /// </summary>
        public IBaseRepository<Country> Country { get; }

        /// <summary>
        ///     This propertiy expresses Order Repository
        /// </summary>
        public IBaseRepository<Order> Order { get; }

        /// <summary>
        ///     This propertiy expresses Product Repository
        /// </summary>
        public IBaseRepository<Product> Product { get; }

        /// <summary>
        ///     This propertiy expresses Product_Branch Repository
        /// </summary>
        public IBaseRepository<Product_Branch> Product_Branch { get; }

        /// <summary>
        ///     This propertiy expresses Product_Images Repository
        /// </summary>
        public IBaseRepository<Product_Images> Product_Images { get; }

        /// <summary>
        ///     This propertiy expresses Purchases Repository
        /// </summary>
        public IBaseRepository<Purchases> Purchases { get; }

        /// <summary>
        ///     This propertiy expresses Subscribe Repository
        /// </summary>
        public IBaseRepository<Subscribe> Subscribe { get; }

        /// <summary>
        ///     This propertiy expresses WishList Repository
        /// </summary>
        public IBaseRepository<WishList> WishList { get; }

        /// <summary>
        ///     This Property expresses Admin Activity
        /// </summary>
        public IBaseRepository<AdminActivity> AdminActivity { get; }


        /// <summary>
        ///     This Complete method it expresses Save Changes
        /// </summary>
        void Complete();
    }
}
