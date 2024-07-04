namespace ShoppingSystemData
{
    public class UnitOfWork : IUnitOfWork
    {
        private IUserRepository? _userRepository;
        private IProductRepository? _productRepository;
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private bool _disposed;

        public UnitOfWork()
        {
        }

        public UnitOfWork(string? connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public bool Commit()
        {
            bool isSuccess = false;
            try
            {
                _transaction.Commit();
                isSuccess = true;
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                GC.Collect();
            }

            return isSuccess;
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
            GC.Collect();
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            dispose(false);
        }

        #region Repositories

        public IUserRepository Users => _userRepository ??= new UserRepository(_transaction);
        public IProductRepository Products => _productRepository ??= new ProductRepository(_transaction);

        #endregion
    }
}
