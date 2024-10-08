﻿
using Jobeer.Exceptions;
using Jobeer.Interfaces.Services;

namespace Jobeer.Services
{
    public class HHruEmployeeData : IEmployeeData
    {
        private readonly string _letter;
        public HHruEmployeeData(IConfiguration configuration)
        {
            _letter = configuration["HHLetter"];
        }

        public string GetLetter()
        {
            return _letter;
        }

    }

}
