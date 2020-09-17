using Repository;

namespace Backend.Tests.Repository
{
    public static class TestRepository
    {
        public static LivrariaContext CreateContext()
        {
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new LivrariaContext(connection);
            return context;
        }

        public static LivrariaRepository CreateRepository()
        {
            var context = CreateContext();
            var repository = new LivrariaRepository(context);
            return repository;
        }
    }
}
