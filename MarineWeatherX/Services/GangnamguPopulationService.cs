using MarineWeatherX.Interfaces;
using MarineWeatherX.Models;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MarineWeatherX.Services
{
    // 해당 서비스는 GangnamguPopulation 타입을 가지는 데이터 베이스를 조작하는 코드이다.
    class GangnamguPopulationService : IDatabase<GangnamguPopulation>
    {
        private readonly WpfProjectDatabaseContext? _wpfProjectDatabaseContext;

        public GangnamguPopulationService(WpfProjectDatabaseContext wpfProjectDatabaseContext)
        {
            this._wpfProjectDatabaseContext = wpfProjectDatabaseContext;
        }

        public void Create(GangnamguPopulation entity)
        {
            this._wpfProjectDatabaseContext?.GangnamguPopulations.Add(entity);
            this._wpfProjectDatabaseContext?.SaveChanges();
        }

        public void Delete(int? id)
        {
            var validData = this._wpfProjectDatabaseContext.GangnamguPopulations.FirstOrDefault(c => c.Id == id);
            if (validData != null)
            {
                this._wpfProjectDatabaseContext.GangnamguPopulations.Remove(validData);
                this._wpfProjectDatabaseContext.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public List<GangnamguPopulation>? Get()
        {
            return this._wpfProjectDatabaseContext?.GangnamguPopulations.ToList();
        }

        public GangnamguPopulation? GetDetail(int? id)
        {
            var validData = this._wpfProjectDatabaseContext?.GangnamguPopulations.FirstOrDefault(c => c.Id == id);

            if (validData != null)
            {
                return validData;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public void Update(GangnamguPopulation entity)
        {
            this._wpfProjectDatabaseContext?.GangnamguPopulations.Update(entity);
            this._wpfProjectDatabaseContext?.SaveChanges();
        }
    }
}
