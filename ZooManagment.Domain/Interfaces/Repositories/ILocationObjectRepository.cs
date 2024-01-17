using ZooManagment.Domain.Models;

namespace ZooManagment.Domain.Interfaces.Repositories;

public interface ILocationObjectRepository
{
    Task<LocationObject?> GetAsync(string locationObjectName);
    Task CreateAsync(LocationObject locationObject);
}