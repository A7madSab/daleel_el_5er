using DaleelElkheir.DAL.Domain;
using DaleelElkheir.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.BLL.Services.Regions
{
    public class RegionService: IRegionService
    {
        #region Region
        private readonly IUnitOfWork unitOfWork;

        public RegionService(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }
        public City GetRegion(int id)
        {
            return unitOfWork.Repository<City>().GetById(id);
        }

        public List<City> GetRegions(Expression<Func<City, bool>> Predicate)
        {
            return unitOfWork.Repository<City>().Get(Predicate);
        }

        public List<City> GetRegions()
        {
            return unitOfWork.Repository<City>().GetAll();
        }

        public void InsertRegion(City _Region)
        {
            unitOfWork.Repository<City>().Insert(_Region);
            unitOfWork.Save();
        }

        public void UpdateRegion(City _Region)
        {
            unitOfWork.Repository<City>().Update(_Region);
            unitOfWork.Save();
        }
        public void DeleteRegion(int id)
        {
            unitOfWork.Repository<City>().Delete(id);
            unitOfWork.Save();
        }

        #endregion

        #region Governorate
        public Governorate GetGovernorate(int id)
        {
            return unitOfWork.Repository<Governorate>().GetById(id);
        }

        public List<Governorate> GetGovernorates(Expression<Func<Governorate, bool>> Predicate)
        {
            return unitOfWork.Repository<Governorate>().Get(Predicate);
        }

        public List<Governorate> GetGovernorates()
        {
            return unitOfWork.Repository<Governorate>().GetAll();
        }

        public void InsertGovernorate(Governorate _Governorate)
        {
            unitOfWork.Repository<Governorate>().Insert(_Governorate);
            unitOfWork.Save();
        }

        public void UpdateGovernorate(Governorate _Governorate)
        {
            unitOfWork.Repository<Governorate>().Update(_Governorate);
            unitOfWork.Save();
        }
        public void DeleteGovernorate(int id)
        {
            unitOfWork.Repository<Governorate>().Delete(id);
            unitOfWork.Save();
        }

        #endregion

        #region Area
        public List<Area> GetAreas(int _CityID)
        {
            return unitOfWork.Repository<Area>().Get(w => w.CityID == _CityID);
        }

        public Area GetArea(int id)
        {
            return unitOfWork.Repository<Area>().GetById(id);
        }

        public void InsertArea(Area _area)
        {
            unitOfWork.Repository<Area>().Insert(_area);
            unitOfWork.Save();
        }

        public void UpdateArea(Area _area)
        {
            unitOfWork.Repository<Area>().Update(_area);
            unitOfWork.Save();
        }
        public void DeleteArea(int id)
        {
            unitOfWork.Repository<Area>().Delete(id);
            unitOfWork.Save();
        }

        #endregion Area
    }
}
