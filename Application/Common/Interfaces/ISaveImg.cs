using Microsoft.AspNetCore.Http;

namespace Application.Common.Interfaces
{
    public interface ISaveImg
    {
        string SaveImage(IFormFile newFile);
    }
}
