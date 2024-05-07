using System.Threading.Tasks;

namespace Game.Infrastructure.Factories
{
    public interface IAsyncFactory
    {
        public Task Prepare();
        public void Clear();
    }
}