using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.BLL.Services.DeviceTokens
{
    public interface IDeviceTokenService
    {
        DeviceToken GetDeviceToken(int id);
        List<DeviceToken> GetDeviceToken(Expression<Func<DeviceToken, bool>> Predicate);

        List<DeviceToken> GetDeviceTokens();
        void InsertDeviceToken(DeviceToken _DeviceToken);
        void UpdateDeviceToken(DeviceToken _DeviceToken);
        void DeleteDeviceToken(int id);

    }
}
