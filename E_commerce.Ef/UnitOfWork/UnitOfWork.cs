using E_commerce.Core.Interfaces.IRepositores;
using E_commerce.Core.Interfaces.IRepositories;
using E_commerce.Core.Interfaces.IUnitOfWork;
using E_commerce.Core.Models;
using E_commerce.Ef.Data;
using E_commerce.Ef.Repositores;
using E_commerce.Ef.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Ef.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        private IBaseRepository<Branch> _branch;
        private IBaseRepository<Cart> _cart;
        private IBaseRepository<Category> _category;
        private IBaseRepository<City> _city;
        private IBaseRepository<Country> _country;
        private IBaseRepository<Order> _order;
        private IBaseRepository<Product> _product;
        private IBaseRepository<Product_Branch> _product_branch;
        private IBaseRepository<Product_Images> _product_image;
        private IBaseRepository<Purchases> _purchases;
        private IBaseRepository<Subscribe> _subscribe;
        private IBaseRepository<WishList> _wishList;
        private IApplicationUserRepository _user;
        private IBaseRepository<UserProperties> _userProperties;
        private IAdminPropertiesRepository _adminProperties;
        private IBaseRepository<AdminActivity> _adminActivity;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        public IBaseRepository<Branch> Branch
        {
            get { return _branch ?? (_branch = new BaseRepository<Branch>(_context));}
        }

        public IBaseRepository<Cart> Cart
        {
            get { return _cart ?? (_cart = new BaseRepository<Cart>(_context)); }
        }

        public IBaseRepository<Category> Category
        {
            get { return _category ?? (_category = new BaseRepository<Category>(_context)); }
        }

        public IBaseRepository<City> City
        {
            get { return _city ?? (_city = new BaseRepository<City>(_context)); }
        }

        public IBaseRepository<Country> Country
        {
            get { return _country ?? (_country = new BaseRepository<Country>(_context)); }
        }

        public IBaseRepository<Order> Order
        {
            get { return _order ?? (_order = new BaseRepository<Order>(_context)); }
        }

        public IBaseRepository<Product> Product
        {
            get { return _product ?? (_product = new BaseRepository<Product>(_context)); }
        }

        public IBaseRepository<Product_Branch> Product_Branch
        {
            get { return _product_branch ?? (_product_branch = new BaseRepository<Product_Branch>(_context)); }
        }

        public IBaseRepository<Product_Images> Product_Images
        {
            get { return _product_image ?? (_product_image = new BaseRepository<Product_Images>(_context)); }
        }

        public IBaseRepository<Purchases> Purchases
        {
            get { return _purchases ?? (_purchases = new BaseRepository<Purchases>(_context)); }
        }

        public IBaseRepository<Subscribe> Subscribe
        {
            get { return _subscribe ?? (_subscribe = new BaseRepository<Subscribe>(_context)); }
        }

        public IBaseRepository<WishList> WishList
        {
            get { return _wishList ?? (_wishList = new BaseRepository<WishList>(_context)); }
        }

        public IApplicationUserRepository User
        {
            get { return _user ?? (_user = new ApplicationUserRepository(_context)); }
        }

        public IBaseRepository<UserProperties> UserProperties
        {
            get { return _userProperties ?? (_userProperties = new BaseRepository<UserProperties>(_context)); }
        }

        public IAdminPropertiesRepository AdminProperties
        {
            get { return _adminProperties ?? (_adminProperties = new AdminPropertiesRepository(_context)); }
        }

        public IBaseRepository<AdminActivity> AdminActivity
        {
            get { return _adminActivity ?? (_adminActivity = new BaseRepository<AdminActivity>(_context)); }
        }

        public void Complete()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}