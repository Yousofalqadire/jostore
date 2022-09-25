using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;

namespace Api.Interfaces
{
    public interface ICloudStorage
    {
        Task<ImageUploadResult> addPhotoAsync(IFormFile file);
        Task<DeletionResult> deletePhotoAsync(string publicId);
    
    }
}