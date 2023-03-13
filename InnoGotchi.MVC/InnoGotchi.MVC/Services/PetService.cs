using InnoGotchi.MVC.Contracts.Clients;
using InnoGotchi.MVC.Contracts.Services;
using InnoGotchi.MVC.Models.Pet;
using InnoGotchi.MVC.RequestFeatures;
using Newtonsoft.Json;

namespace InnoGotchi.MVC.Services
{
    public class PetService : IPetService
    {
        private readonly IPetClient _petClient;

        public PetService(IPetClient petClient)
        {
            _petClient = petClient;
        }

        public async Task FeedAsync(string jwt, string userId, Guid farmId, Guid petId)
        {
            await _petClient.FeedAsync(userId, farmId.ToString(), petId.ToString(), jwt);
        }

        public async Task<PetDetailsDto> GetPetDetailsAsync(string jwt, string userId, Guid farmId, Guid petId)
        {
            return await _petClient.GetPetDetailsAsync(userId, farmId.ToString(), petId.ToString(), jwt);
        }

        public async Task GiveADrinkAsync(string jwt, string userId, Guid farmId, Guid petId)
        {
            await _petClient.GiveADrinkAsync(userId, farmId.ToString(), petId.ToString(), jwt);
        }

        public async Task UpdatePetAsync(string jwt, string userId, Guid farmId, Guid petId, PetForUpdateDto petDto)
        {
            await _petClient.UpdatePetAsync(userId, farmId.ToString(), petId.ToString(), jwt, petDto);
        }

        public async Task<PetPagingDto> GetPetsAsync(string jwt, int pageNumber, string orderBy, int pageSize)
        {
            var (pets, paginationHeaderValue) = await _petClient.GetPetsAsync(jwt, pageNumber, orderBy, pageSize);

            var petPaging = new PetPagingDto
            {
                PetDetails = pets,
                MetaData = JsonConvert.DeserializeObject<MetaData>(paginationHeaderValue)
            };

            return petPaging;
        }

        public async Task CreatePetAsync(string jwt, string userId, Guid farmId, PetForCreationDto petDto)
        {
            await _petClient.CreatePetAsync(userId, farmId.ToString(), jwt, petDto);
        }
    }
}
