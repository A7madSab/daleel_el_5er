using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DaleelElkheir.DAL.Domain;
using DaleelElkheir.DAL.Repository;

namespace DaleelElkheir.BLL.Services.DeviceTokens
{
    public class DeviceTokenService : IDeviceTokenService
    {
        private readonly IUnitOfWork unitOfWork;
        
        public DeviceTokenService(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }
        public DeviceToken GetDeviceToken(int id)
        {
            return unitOfWork.Repository<DeviceToken>().GetById(id);
        }

        public List<DeviceToken> GetDeviceToken(Expression<Func<DeviceToken, bool>> Predicate)
        {
            return unitOfWork.Repository<DeviceToken>().Get(Predicate);
        }

        public List<DeviceToken> GetDeviceTokens()
        {
            return unitOfWork.Repository<DeviceToken>().GetAll();
        }

        public void InsertDeviceToken(DeviceToken _DeviceToken)
        {
            unitOfWork.Repository<DeviceToken>().Insert(_DeviceToken);
            unitOfWork.Save();
        }

        public void UpdateDeviceToken(DeviceToken _DeviceToken)
        {
            unitOfWork.Repository<DeviceToken>().Update(_DeviceToken);
            unitOfWork.Save();
        }
        public void DeleteDeviceToken(int id)
        {
            unitOfWork.Repository<DeviceToken>().Delete(id);
            unitOfWork.Save();
        }

    }
}
