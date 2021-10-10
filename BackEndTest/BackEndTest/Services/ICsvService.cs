using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndTest.Services
{
    public interface ICsvService
    {
        bool ExtractData(IFormFile file, out string errorMessage);
    }
}
