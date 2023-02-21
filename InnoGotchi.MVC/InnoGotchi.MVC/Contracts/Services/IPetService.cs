using InnoGotchi.MVC.Models.Pet;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace InnoGotchi.MVC.Contracts.Services
{
    public interface IPetService
    {
        Task FeedAsync(string jwt, string userId, Guid farmId, Guid petId);
        Task GiveADrinkAsync(string jwt, string userId, Guid farmId, Guid petId);
        Task<PetDetailsDto> GetPetDetailsAsync(string jwt, string userId, Guid farmId, Guid petId);
        Task UpdatePetAsync(string jwt, string userId, Guid farmId, Guid petId, PetForUpdateDto petDto);
        Task<PetPagingDto> GetPetsAsync(string jwt, int pageNumber, string orderBy, int pageSize);
        Task CreatePetAsync(string jwt, string userId, Guid farmId, PetForCreationDto petDto);
    }
}
