using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobChat.Infra.StorageService.Interfaces.Services
{
    public interface IBlobService
    {
        Task<string> UploadMediaFileAsync(string container, string fileName, System.IO.Stream fileStream, string contentType);
    }
}
