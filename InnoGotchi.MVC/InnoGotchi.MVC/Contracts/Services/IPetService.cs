using InnoGotchi.MVC.Models.Pet;

namespace InnoGotchi.MVC.Contracts.Services
{
    public interface IPetService
    {
        Task FeedAsync(string jwt, string userId, string farmId, string petId);
        Task GiveADrinkAsync(string jwt, string userId, string farmId, string petId);
        Task<PetDetailsDto> GetPetDetailsAsync(string jwt, string userId, string farmId, string petId);
        Task UpdatePetAsync(string jwt, string userId, string farmId, string petId, PetForUpdateDto petDto);
    }
}
