using InnoGotchi.MVC.Contracts.Helpers;

namespace InnoGotchi.MVC.Helpers
{
    public class ConvertHelper : IConvertHelper
    {
        public string ConvertFromImageToBase64(IFormFile file)
        {
            using var stream = new MemoryStream();
            file.CopyTo(stream);
            var bytes = stream.ToArray();

            string base64String = Convert.ToBase64String(bytes);

            return base64String;
        }
    }
}
