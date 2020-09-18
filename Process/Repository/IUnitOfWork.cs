using System.Threading.Tasks;

namespace Process.Repository
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
