namespace Shop.Infrastructure.Interfaces;
public interface IUnitOfWork
{
    Task<int> SaveCahngesAsync();
}
