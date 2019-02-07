using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spider.Application.Tests.Infrastructure
{
    public interface ITestData
    {
        Task<T> Read<T>(string name);
    }
}