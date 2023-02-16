namespace InnoGotchi.MVC.Contracts.Services
{
    public interface IPetService
    {
        Task Feed(string jwt, Guid petId);
        Task GiveADrink(string jwt, Guid petIt);
    }
}
