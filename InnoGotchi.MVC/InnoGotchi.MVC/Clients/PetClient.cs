using InnoGotchi.MVC.Contracts.Clients;
using InnoGotchi.MVC.Models.Pet;

namespace InnoGotchi.MVC.Clients
{
    public class PetClient : RestClient<PetDetailsDto>, IPetClient
    {
        public PetClient(IHttpClientFactory clientFactory, IConfiguration configuration) 
            : base(clientFactory, configuration, "users/userId/farm/farmId/pets/")
        {
        }

        private void SetAddressSuffix(string userId, string farmId)
        {
            AddressSuffix.Replace("userId", userId);
            AddressSuffix.Replace("farmId", farmId);
        }

        public async Task<PetDetailsDto> GetPetDetailsAsync(string userId, string farmId, string petId, string jwt)
        {
            SetAddressSuffix(userId, farmId);
            return await GetAsync<PetDetailsDto>(petId, jwt: jwt);
        }
        public async Task FeedAsync(string userId, string farmId, string petId, string jwt)
        {
            SetAddressSuffix(userId, farmId);
            await PutAsync<PetDetailsDto>(petId, null, "/feed", jwt);
        }
        public async Task GiveADrinkAsync(string userId, string farmId, string petId, string jwt)
        {
            SetAddressSuffix(userId, farmId);
            await PutAsync<PetDetailsDto>(petId, null, "/give-a-drink", jwt);
        }

        public async Task UpdatePetAsync(string userId, string farmId, string petId, string jwt, PetForUpdateDto petDto)
        {
            SetAddressSuffix(userId, farmId);
            await PutAsync(petId, petDto, "", jwt);
        }

        public async Task<(IEnumerable<PetDetailsDto>, string)> GetPetsAsync(string jwt, int pageNumber, string orderBy, int pageSize)
        {
            AddressSuffix.Clear();
            AddressSuffix.Append($"pets");

            return await GetAllAsync($"?OrderBy={orderBy}&PageSize={pageSize}&PageNumber={pageNumber}", jwt);
        }

        public async Task CreatePetAsync(string userId, string farmId, string jwt, PetForCreationDto petDto)
        {
            SetAddressSuffix(userId, farmId);
            await PostAsync("", petDto, jwt: jwt);
        }
    }
}
