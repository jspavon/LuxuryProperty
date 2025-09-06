using MongoDB.Driver;

namespace luxuryProperty.app.infraestructure.Context.Interfaces
{
    public interface IMongoDataContext : IDisposable
    {
        // Métodos para MongoDB
        IMongoCollection<TEntity> GetCollection<TEntity>(string name = null) where TEntity : class;
        
        // Métodos de transacción
        Task<IClientSessionHandle> StartSessionAsync();
        Task CommitTransactionAsync(IClientSessionHandle session);
        Task AbortTransactionAsync(IClientSessionHandle session);
    
    }
}