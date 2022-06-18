using E_commerce.Core.Consts;
using E_commerce.Core.Interfaces.IRepositores;
using E_commerce.Core.Interfaces.IServices;
using E_commerce.Core.Interfaces.IUnitOfWork;
using E_commerce.Core.Models;
using E_commerce.Core.ViewModels.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.Services
{
    public class StatsHelper
    {
        private readonly IUnitOfWork _repository;

        public StatsHelper(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<int> RelativeStatsForAdminActionInYear(int year, string ActionType, string AdminId)
        {

            int allCountActive = await _repository.AdminActivity.CountAsync(active
                => active.DateTime.Year == year
                && active.AdminId == Guid.Parse(AdminId));

            if (allCountActive == 0)
                return 0;

            int countActive = await _repository.AdminActivity.CountAsync(active
                => active.DateTime.Year == year
                && active.TypeActive == ActionType
                && active.AdminId == Guid.Parse(AdminId));

            return Convert.ToInt32(((float)countActive / (float)allCountActive) * 100);
        }

        public async Task<List<int>> StatsForAdminActionToProductsInYearAsync(int year, string ActionType, string AdminId)
        {
            List<int> Stats = new List<int>();
            int AllStats = await _repository.AdminActivity.CountAsync(add => add.DateTime.Year == year 
            && add.TypeActive == ActionType
            && add.AdminId == Guid.Parse(AdminId));

            if (AllStats == 0)
            {
                for (int i = 0; i < 12; i++)
                {
                    Stats.Add(0);
                }
                return Stats;
            }

            if(year == DateTime.Now.Year)
            {
                for (int i = 1; i <= DateTime.Now.Month; i++)
                {
                    int countAdded = await _repository.AdminActivity.CountAsync(add
                        => add.DateTime.Year == year
                        && add.DateTime.Month == i
                        && add.TypeActive == ActionType
                        && add.AdminId == Guid.Parse(AdminId));

                    int stat = Convert.ToInt32(((float)countAdded / (float)AllStats) * 100);
                    Stats.Add(stat);
                }
                for (int i = 0; i < 12 - DateTime.Now.Month; i++)
                {
                    Stats.Add(0);
                }

                return Stats;
            }
           
            if (year != DateTime.Now.Year)
            {
                for (int i = 1; i <= 12; i++)
                {
                    int countAdded = await _repository.AdminActivity.CountAsync(add
                       => add.DateTime.Year == year
                       && add.DateTime.Month == i
                       && add.TypeActive == ActionType
                       && add.AdminId == Guid.Parse(AdminId));

                    int stat = Convert.ToInt32(((float)countAdded / (float)AllStats) * 100);
                    Stats.Add(stat);
                }
                
                return Stats;
            }

            throw new Exception();
        }
    }
}
