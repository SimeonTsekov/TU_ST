namespace webAPI.Interfaces
{
    public interface IBaseService<in TP, TR>
    {
        TR Create(TP newModel);
        TR Update(int id, TP updatedModel);
        TR GetById(int id);
        List<TR> GetAll();
        void Delete(int id);
    }
}
