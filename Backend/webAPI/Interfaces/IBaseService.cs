using System.Security.Claims;
using webApi.Data.Models;

namespace webAPI.Interfaces
{
    public interface IBaseService<in TP, TR>
    {
        TR Create(TP newModel, UserModel user);
        TR Update(int id, TP updatedModel);
        void Delete(int id);
        List<TR> GetAll();
        TR GetById(int id);
    }
}
