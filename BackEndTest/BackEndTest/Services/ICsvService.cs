using BackEndTest.Models.Responses;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndTest.Services
{
    public interface ICsvService
    {
        UploadResponse ExtractData(IFormFile file);
    }
}
