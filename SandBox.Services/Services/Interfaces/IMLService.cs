using SandBox.DTOs.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SandBox.Services.Services.Interfaces
{
    public interface IMLService
    {
        Task<IrisPrediction> TestdeML();
    }
}
