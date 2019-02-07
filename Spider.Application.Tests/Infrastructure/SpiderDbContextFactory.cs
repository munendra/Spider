using Microsoft.EntityFrameworkCore;
using Spider.Persistence;
using System;

namespace Spider.Application.Tests.Infrastructure
{
    public static class SpiderDbContextFactory
    {
        public static SpiderDbContext Create()
        {
            var options = new DbContextOptionsBuilder<SpiderDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new SpiderDbContext(options);
            context.Database.EnsureCreated();
            return context;
        }

        public static void Destroy(SpiderDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}