﻿using E_commerce.Core.Const;
using E_commerce.Core.Interfaces.IRepositores;
using E_commerce.Core.Interfaces.IServices;
using E_commerce.Core.Services;
using E_commerce.Ef.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace E_commerce.Ef.Repositores
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Sync Methods
        public T GetById(object Id) => _context.Set<T>().Find(Id);

        public void Add(T entity) => _context.Set<T>().Add(entity);
         
        public void UpDate(T entity) => _context.Set<T>().Update(entity);
        
        public void Delete(T entity) => _context.Set<T>().Remove(entity);

        // Overloads For Method => GitAll()
        public List<T> GetAll() => _context.Set<T>().ToList();

        public List<T> GetAll(Expression<Func<T, object>>[] Include)
        {
            IQueryable<T> query = _context.Set<T>();

            for (int i = 0; i < Include.Length; i++)
                query = query.Include(Include[i]);

            return query.ToList();
        }

        public List<T> GetAll(Expression<Func<T, object>> Order, string By)
            => OrderHelper(Order, By, _context.Set<T>()).ToList();

        public List<T> GetAll(Expression<Func<T, object>>[] Include, Expression<Func<T, object>> Order, string By)
        {
            IQueryable<T> query = _context.Set<T>();

            for (int i = 0; i < Include.Length; i++)
                query = query.Include(Include[i]);

            return OrderHelper(Order, By, query).ToList();
        }


        // Overloads For Method => Find()
        public T Find(Expression<Func<T, bool>> match) => _context.Set<T>().SingleOrDefault(match);

        public T Find(Expression<Func<T, bool>> match, Expression<Func<T, object>>[] Include)
        {
            IQueryable<T> query = _context.Set<T>().Where(match);

            for (int i = 0; i < Include.Length; i++)
                query = query.Include(Include[i]);

            return query.SingleOrDefault();
        }

        // Overloads For Method => FindAll()         
        public List<T> FindAll(Expression<Func<T, bool>> match) => _context.Set<T>().Where(match).ToList();
       
        public List<T> FindAll(Expression<Func<T, bool>> match, Expression<Func<T, object>>[] Include)
        {
            IQueryable<T> query = _context.Set<T>().Where(match);

            for (int i = 0; i < Include.Length; i++)
                query = query.Include(Include[i]);

            return query.ToList();
        }
        
        public List<T> FindAll(Expression<Func<T, bool>> match, Expression<Func<T, object>> Order, string By)
            => OrderHelper(Order, By, _context.Set<T>().Where(match)).ToList();

        public List<T> FindAll(Expression<Func<T, bool>> match, Expression<Func<T, object>>[] Include, Expression<Func<T, object>> Order, string By)
        {
            IQueryable<T> query = _context.Set<T>().Where(match);

            for (int i = 0; i < Include.Length; i++)
                query = query.Include(Include[i]);

            return OrderHelper(Order, By, query).ToList();
        }
       
        public bool Any(Expression<Func<T, bool>> expression) => _context.Set<T>().Any(expression);

        // Overloads For Method => Count()

        public int Count() => _context.Set<T>().Count();

        public int Count(Expression<Func<T, bool>> math) 
            => _context.Set<T>().Where(math).Count();

        // Async Methods        
        public async Task<T> GetByIdAsync(object Id) => await _context.Set<T>().FindAsync(Id);
        
        public async Task AddAsync(T entity) => await _context.Set<T>().AddAsync(entity);

        // Overloads For Method => GitAllAsync()        
        public async Task<List<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();
        
        public async Task<List<T>> GetAllAsync(Expression<Func<T, object>>[] Include)
        {
            IQueryable<T> query = _context.Set<T>();

            for (int i = 0; i < Include.Length; i++)
                query = query.Include(Include[i]);

            return await query.ToListAsync();
        }
        
        public async Task<List<T>> GetAllAsync(Expression<Func<T, object>> Order, string By)
            => await OrderHelper(Order, By, _context.Set<T>()).ToListAsync();
        
        public async Task<List<T>> GetAllAsync(Expression<Func<T, object>>[] Include, Expression<Func<T, object>> Order, string By)
        {
            IQueryable<T> query = _context.Set<T>();

            for (int i = 0; i < Include.Length; i++)
                query = query.Include(Include[i]);

            return await OrderHelper(Order, By, query).ToListAsync();
        }

        // Overloads For Method => FindAsync()        
        public async Task<T> FindAsync(Expression<Func<T, bool>> match) 
            => await _context.Set<T>().SingleOrDefaultAsync(match);
        
        public async Task<T> FindAsync(Expression<Func<T, bool>> match, Expression<Func<T, object>>[] Include)
        {
            IQueryable<T> query = _context.Set<T>().Where(match);

            for (int i = 0; i < Include.Length; i++)
                query = query.Include(Include[i]);

            return await query.SingleOrDefaultAsync();
        }

        // Overloads For Method => FindAllAsync()        
        public async Task<List<T>> FindAllAsync(Expression<Func<T, bool>> match) 
            => await _context.Set<T>().Where(match).ToListAsync();
        
        public async Task<List<T>> FindAllAsync(Expression<Func<T, bool>> match, Expression<Func<T, object>>[] Include)
        {
            IQueryable<T> query = _context.Set<T>().Where(match);

            for (int i = 0; i < Include.Length; i++)
                query = query.Include(Include[i]);

            return await query.ToListAsync();
        }
        
        public async Task<List<T>> FindAllAsync(Expression<Func<T, bool>> match, Expression<Func<T, object>> Order, string By)
            => await OrderHelper(Order, By, _context.Set<T>().Where(match)).ToListAsync();
        
        public async Task<List<T>> FindAllAsync(Expression<Func<T, bool>> match, Expression<Func<T, object>>[] Include, Expression<Func<T, object>> Order, string By)
        {
            IQueryable<T> query = _context.Set<T>().Where(match);

            for (int i = 0; i < Include.Length; i++)
                query = query.Include(Include[i]);

            return await OrderHelper(Order, By, query).ToListAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression) => await _context.Set<T>().AnyAsync(expression);

        // Overloads For Method => CountAsync()

        public async Task<int> CountAsync() => await _context.Set<T>().CountAsync();

        public async Task<int> CountAsync(Expression<Func<T, bool>> match) 
            => await _context.Set<T>().Where(match).CountAsync();

        // Helpers
        public IQueryable<T> OrderHelper(Expression<Func<T, object>> Order, string By, IQueryable<T> Query)
        {
            if (By == "Asc" || By == null)
                Query = Query.OrderBy(Order);
            else if (By == "Des")
                Query = Query.OrderByDescending(Order);
            else
                throw new Exception($"The Value Should be By = \"Asc\" or \"Des\" Only, Use The <OrderBy> Class " +
                    @"Class Path => ~\E_commerce\E_commerce.Core\Const\Order.cs");

            return Query;
        }
    }
}