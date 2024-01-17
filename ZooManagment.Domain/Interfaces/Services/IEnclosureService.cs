using ZooManagment.Domain.Dtos.Enclosure;
using ZooManagment.Domain.Models;

namespace ZooManagment.Domain.Interfaces.Services;

public interface IEnclosureService
{
    /// <summary>
    /// Creates an enclosure and its related location objects
    /// </summary>
    /// <param name="enclosureCreateParsedDto"></param>
    /// <returns></returns>
    Task<Enclosure> CreateAsync(EnclosureCreateParsedDto enclosureCreateParsedDto);
}