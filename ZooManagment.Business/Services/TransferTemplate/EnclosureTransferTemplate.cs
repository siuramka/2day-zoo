using ZooManagment.DataAccess.Repositories;
using ZooManagment.Domain.Interfaces.Repositories;
using ZooManagment.Domain.Models;

namespace ZooManagment.Business.Services.TransferTemplate;

public abstract class EnclosureTransferTemplate
{
    private IEnclosureRepository _enclosureRepository;

    protected EnclosureTransferTemplate(IEnclosureRepository enclosureRepository)
    {
        _enclosureRepository = enclosureRepository;
    }

    public async Task<Enclosure?> GetEnclosureAsync(Animal animal)
    {
        var sameSpecieEnclosure = await _enclosureRepository.GetSameSpecie(animal.Specie.Name);
        if (sameSpecieEnclosure != null)
            return sameSpecieEnclosure;

        var enclosureByRules = await GetEnclosureByRules(animal);
        if (enclosureByRules != null)
            return enclosureByRules;

        var firstEmptyEnclosure = await _enclosureRepository.GetFirstEmpty();
        return firstEmptyEnclosure;
    }

    protected abstract Task<Enclosure?> GetEnclosureByRules(Animal animal);
}