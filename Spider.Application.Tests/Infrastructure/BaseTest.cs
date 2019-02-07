using System;
using Spider.Persistence;

namespace Spider.Application.Tests.Infrastructure
{
    public class BaseTest : IDisposable
    {
        public SpiderDbContext Context { get; set; }
        public ITestData TestData { get; set; }

        public BaseTest()
        {
            TestData = new TestData();
            Context = SpiderDbContextFactory.Create();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}