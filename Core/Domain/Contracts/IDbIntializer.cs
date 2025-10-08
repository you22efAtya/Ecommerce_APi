namespace Domain.Contracts
{
    public interface IDbIntializer
    {
        Task InitializeAsync();
        Task InitializeIdentityAsync();
    }
}
