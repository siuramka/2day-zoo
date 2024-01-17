using ZooManagment.DataAccess.Repositories;
using ZooManagment.Domain.Dtos.Enclosure;
using ZooManagment.Domain.Interfaces.Repositories;
using ZooManagment.Domain.Interfaces.Services;
using ZooManagment.Domain.Models;

namespace ZooManagment.Business.Services
{
    public class EnclosureService : IEnclosureService
    {
        private readonly IEnclosureRepository _enclosureRepository;
        private readonly ILocationObjectRepository _locationObjectRepository;

        public EnclosureService(IEnclosureRepository enclosureRepository,
            ILocationObjectRepository locationObjectRepository)
        {
            _enclosureRepository = enclosureRepository;
            _locationObjectRepository = locationObjectRepository;
        }

        /// <summary>
        /// Creates an enclosure and its related location objects
        /// </summary>
        /// <param name="enclosureCreateParsedDto"></param>
        /// <returns></returns>
        public async Task<Enclosure> CreateAsync(EnclosureCreateParsedDto enclosureCreateParsedDto)
        {
            var enclosure = new Enclosure
            {
                EnclosureSize = enclosureCreateParsedDto.EnclosureSize,
                LocationType = enclosureCreateParsedDto.LocationType, Name = enclosureCreateParsedDto.Name
            };

            foreach (var locationObjectName in enclosureCreateParsedDto.Objects)
            {
                var existingLocationObject = await _locationObjectRepository.GetAsync(locationObjectName);
                var newLocationObject = new LocationObject { Name = locationObjectName };

                if (existingLocationObject == null)
                {
                    await _locationObjectRepository.CreateAsync(newLocationObject);
                }

                var locationObject = existingLocationObject ?? newLocationObject;

                var enclosureLocationObject = new EnclosureLocationObject
                    { LocationObjectId = locationObject.Id, EnclosureId = enclosure.Id };

                enclosure.EnclosureLocationObjects.Add(enclosureLocationObject);
            }

            await _enclosureRepository.CreateAsync(enclosure);

            return enclosure;
        }
    }
}