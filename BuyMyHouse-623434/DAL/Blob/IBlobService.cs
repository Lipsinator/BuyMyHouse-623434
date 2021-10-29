using System.Threading.Tasks;
using Domain;

namespace DAL.Helpers
{
    public interface IBlobService
    {
        Task<string> CreateFile(string file, string fileName = "");
        Task<bool> DeleteBlobFromServer(string fileName);
        Task<string> GetBlobFromServer(string fileName);
    }
}