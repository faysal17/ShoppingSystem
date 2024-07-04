namespace ShoppingSystemCore.Abstraction
{
    public interface IUnitOfWork
    {
        public IUserRepository Users { get; }
        public IProductRepository Products { get; }
        bool Commit();
    }
}
