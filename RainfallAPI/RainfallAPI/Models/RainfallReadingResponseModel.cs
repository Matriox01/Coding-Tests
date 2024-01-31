// In Models/RainfallModels.cs
using RainfallAPI.Models;
using System;
using System.Collections.Generic;

namespace RainfallApi.Models
{
    public class RainfallReadingResponseModel
    {
        public List<RainfallReadingModel> Readings { get; set; }
    }
}
