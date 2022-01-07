using Net6MinimalApi.Models;

namespace Net6MinimalApi.Services
{
    public interface IJerseyService
    {
        List<Jersey> GetAll();
        Jersey Get(int id);
        int Insert(Jersey jersey);
        Jersey Update(int id, string playerName);
        bool Delete(int id);
    }
}
