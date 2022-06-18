using AutoMapper;
using E_commerce.Core.Interfaces.IUnitOfWork;
using E_commerce.Core.Models;
using E_commerce.Domain.Interfaces.IServices;
using E_commerce.Domain.ViewModels.City;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.Services
{
    public class CityServices : ICityServices
    {
        private readonly IUnitOfWork _repositories;
        private readonly IMapper _mapper;
        private readonly IToastNotification _toastNotification;

        public CityServices(IUnitOfWork repositories, IMapper mapper, IToastNotification toastNotification)
        {
            _repositories = repositories;
            _mapper = mapper;
            _toastNotification = toastNotification;
        }

        public async Task<bool> AnyAsync(Expression<Func<City, bool>> expression)
            => await _repositories.City.AnyAsync(expression);

        public async Task<bool> CreateCityFromViewModel(AddCityViewModel viewModel, string countryid)
        {
            City city = new City
            {
                Id = Guid.NewGuid(),
                Name = viewModel.Name,
                CountryId = Guid.Parse(countryid)
            };

            try
            {
                await _repositories.City.AddAsync(city);
                _repositories.Complete();
            }
            catch
            {
                _toastNotification.AddErrorToastMessage("Craete failed");
                return false;
            }

            _toastNotification.AddSuccessToastMessage("Craete successfully");
            return true;
        }

        public async Task<int> DeleteAsync(string id)
        {
            City city = await _repositories.City.FindAsync(x => x.Id == Guid.Parse(id));

            if (city == null)
                return 404;

            try
            {
                _repositories.City.Delete(city);
                _repositories.Complete();
            }
            catch
            {
                return 500;
            }

            return 200;
        }

        public async Task<List<CityViewModel>> GetCitiesInCountryByCountryId(Guid CountryId)
        {
            List<City> cities = await _repositories.City.FindAllAsync(city => city.CountryId == CountryId);

            return _mapper.Map<List<CityViewModel>>(cities);
        }

        public async Task<int> UpDateAsync(string id, string name)
        {
            City city = await _repositories.City.FindAsync(x => x.Id == Guid.Parse(id));

            if (city == null)
                return 404;

            try
            {
                city.Name = name;
                _repositories.City.UpDate(city);
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
