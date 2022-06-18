using AutoMapper;
using E_commerce.Core.Interfaces.IUnitOfWork;
using E_commerce.Core.Models;
using E_commerce.Domain.ViewModels.Branch;
using E_commerce.Domain.Interfaces;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.Services
{
    public class BranchServices : IBranchServices
    {
        private readonly IUnitOfWork _repositories;
        private readonly IMapper _mapper;
        private readonly IToastNotification _toastNotification;

        public BranchServices(IUnitOfWork repositories, IMapper mapper, IToastNotification toastNotification)
        {
            _repositories = repositories;
            _mapper = mapper;
            _toastNotification = toastNotification;
        }

        public async Task<bool> AnyAsync(Expression<Func<Branch, bool>> expression) 
            => await _repositories.Branch.AnyAsync(expression);

        public async Task<bool> CreateBranchFromViewModel(AddBranchViewModel viewModel, string countryid)
        {
            Branch branch = new Branch
            {
                Id = Guid.NewGuid(),
                Title = viewModel.Title,
                CountryId = Guid.Parse(countryid)
            };

            try
            {
                await _repositories.Branch.AddAsync(branch);
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
            Branch branch = await _repositories.Branch.FindAsync(x => x.Id == Guid.Parse(id));

            if (branch == null)
                return 404;

            try
            {
                _repositories.Branch.Delete(branch);
                _repositories.Complete();
            }
            catch
            {
                return 500;
            }

            return 200;
        }

        public async Task<List<BranchViewModel>> GetBranchsInCountryByCountryIdIdAsync(Guid CountryId)
        {
            List<Branch> branches = await _repositories.Branch.FindAllAsync(branch
                => branch.CountryId == CountryId);

            return _mapper.Map<List<BranchViewModel>>(branches);
        }

        public async Task<int> UpDateAsync(string id, string name)
        {
            Branch branch = await _repositories.Branch.FindAsync(x => x.Id == Guid.Parse(id));

            if (branch == null)
                return 404;

            try
            {
                branch.Title = name;
                _repositories.Branch.UpDate(branch);
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