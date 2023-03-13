using InnoGotchi.MVC.Models.Pet;

namespace InnoGotchi.MVC.Contracts.Clients
{
    public interface IPetClient
    {
        Task<PetDetailsDto> GetPetDetailsAsync(string userId, string farmId, string petId, string jwt);
        Task FeedAsync(string userId, string farmId, string petId, string jwt);
        Task GiveADrinkAsync(string userId, string farmId, string petId, string jwt);
        Task UpdatePetAsync(string userId, string farmId, string petId, string jwt, PetForUpdateDto petDto);
        Task<(IEnumerable<PetDetailsDto>, string)> GetPetsAsync(string jwt, int pageNumber, string orderBy, int pageSize);
        Task CreatePetAsync(string userId, string farmId, string jwt, PetForCreationDto petDto);
    }
}
