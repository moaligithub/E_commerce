using AutoMapper;
using E_commerce.Core.Consts;
using E_commerce.Core.Interfaces.IServices;
using E_commerce.Core.Interfaces.IUnitOfWork;
using E_commerce.Core.Models;
using E_commerce.Core.ViewModels;
using E_commerce.Core.ViewModels.Admin;
using E_commerce.Core.ViewModels.AdminActivity;
using E_commerce.Domain.ViewModels.Branch;
using E_commerce.Domain.ViewModels;
using E_commerce.Domain.ViewModels.AdminManager;
using E_commerce.Domain.ViewModels.City;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.Helper
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<CreateAdminViewModel, ApplicationUser>();

            CreateMap<ApplicationUser, AdminEditViewModel>()
                .ForMember(dest => dest.ProfilePicture, src => src.MapFrom(src => src.AdminProperties.ProfilePicture))
                .ForMember(dest => dest.BranchId, src => src.MapFrom(src => src.AdminProperties.Branch.Id))
                .ForMember(dest => dest.CityId, src => src.MapFrom(src => src.City.Id))
                .ForMember(dest => dest.CountryId, src => src.MapFrom(src => src.City.CountryId));
            
            CreateMap<AdminActivity, AdminActivityViewModel>();

            CreateMap<ApplicationUser, AdminDetailsViewModel>()
                .ForMember(dest => dest.FullName, src => src.MapFrom(src => src.FirstName + " " + src.LastName))
                .ForMember(dest => dest.Branch, src => src.MapFrom(src => src.AdminProperties.Branch.Title +
                    " / " + src.AdminProperties.Branch.Country.Name))
                .ForMember(dest => dest.Country, src => src.MapFrom(src => src.City.Country.Name))
                .ForMember(dest => dest.ProfilePicture, src => src.MapFrom(src => src.AdminProperties.ProfilePicture))
                .ForMember(dest => dest.City, src => src.MapFrom(src => src.City.Name))
                .ForMember(dest => dest.AdminActivities, src => src.MapFrom(src => src.AdminProperties.Activities));  
            
            CreateMap<Branch, BranchViewModel>();
            CreateMap<Country, CountryViewModel>();
            CreateMap<City, CityViewModel>();
            CreateMap<AdminEditViewModel, EditAdminManagerViewModel>().ReverseMap();
        }
    }
}
