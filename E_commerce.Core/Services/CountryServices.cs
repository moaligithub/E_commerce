using AutoMapper;
using E_commerce.Core.Interfaces.IUnitOfWork;
using E_commerce.Core.Models;
using E_commerce.Domain.Interfaces.IServices;
using E_commerce.Domain.ViewModels;
using E_commerce.Domain.ViewModels.Country;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.Services
{
    public class CountryServices : ICountryServices
    {
        private readonly IUnitOfWork _repositories;
        private readonly IToastNotification _toastNotification;

        public CountryServices(IUnitOfWork repositories, IToastNotification toastNotification)
        {
            _repositories = repositories;
            _toastNotification = toastNotification;
        }

        public async Task<bool> AddFromViewModelAsync(AddCountryViewModel viewModel)
        {
            Country country = new Country
            {
                Id = Guid.NewGuid(),
                Name = viewModel.Name
            };

            try
            {
                await _repositories.Country.AddAsync(country);
                _repositories.Complete();
            }
            catch
            {
                _toastNotification.AddErrorToastMessage("Sorry, Added failed");
                return false;
            }

            _toastNotification.AddSuccessToastMessage("Added successfully");
            return true;
        }

        public async Task<bool> AnyAsync(Expression<Func<Country, bool>> expression)
            => await _repositories.Country.AnyAsync(expression);

        public async Task<int> DeleteAsync(string id)
        {
            Country country = await _repositories.Country.FindAsync(country => country.Id == Guid.Parse(id));

            if (country == null)
                return 404;

            try
            {
                _repositories.Country.Delete(country);
                _repositories.Complete();
            }
            catch
            {
                return 500;
            }

            return 200;
        }

        public async Task<List<CountryViewModel>> GetAllCountryAsync()
        {
            List<Country> countries = await _repositories.Country.GetAllAsync();
            List<CountryViewModel> Result = countries.Select(country => new CountryViewModel()
            {
                Id = country.Id,
                Name = country.Name,
                CitiesCount = _repositories.City.Count(x => x.CountryId == country.Id),
                BranchsCount = _repositories.Branch.Count(x => x.CountryId == country.Id)
            }).ToList();

            return Result;
        }

        public async Task<int> UpDateAsync(string id, string name)
        {
            Country country = await _repositories.Country.FindAsync(x => x.Id == Guid.Parse(id));

            if (country == null)
                return 404;

            country.Name = name;

            try
            {
                _repositories.Country.UpDate(country);
                _repositories.Complete();
            }
            catch
            {
                return 500;
            }

            return 200;
        }
    }
}