using Microsoft.EntityFrameworkCore;

namespace HHS_CSharp_React_Tests.Helpers
{
    public static class ContextHelper
    {
        public static DataContext CreateContext(string dbName)
        {
            return new(CreateOptions(dbName));
        }

        private static DbContextOptions<DataContext> CreateOptions(string dbName)
        {
            return new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
        }
    }
}
