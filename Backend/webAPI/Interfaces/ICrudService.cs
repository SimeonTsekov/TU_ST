namespace webAPI.Interfaces
{
    public interface ICrudService<in TP, TR>
    {
        TR Create(TP newModel);
        TR Update(int id, TP updatedModel);
        TR GetById(int id);
        List<TR> Get(string order, int count);
        void Delete(int id);
    }
}
