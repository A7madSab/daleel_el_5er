using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.BLL.Services.Regions
{
    public interface IRegionService
    {
        #region RegionServices
        City GetRegion(int id);

        List<City> GetRegions(Expression<Func<City, bool>> Predicate);

        List<City> GetRegions();

        void InsertRegion(City _Region);

        void UpdateRegion(City _Region);

        void DeleteRegion(int id);
        #endregion

        #region GovernorateServices
        Governorate GetGovernorate(int id);

        List<Governorate> GetGovernorates(Expression<Func<Governorate, bool>> Predicate);

        List<Governorate> GetGovernorates();

        void InsertGovernorate(Governorate _Governorate);

        void UpdateGovernorate(Governorate _Governorate);

        void DeleteGovernorate(int id);
        #endregion

        #region Area 
        List<Area> GetAreas(int _CityID);
        Area GetArea(int id);
        void InsertArea(Area _area);
        void UpdateArea(Area _area);
        void DeleteArea(int id);
        #endregion Area

    }
}
