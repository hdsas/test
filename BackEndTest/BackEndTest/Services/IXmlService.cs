using BackEndTest.Models.Responses;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndTest.Services
{
    public interface IXmlService
    {
        UploadResponse ExtractData(IFormFile file);
    }
}
