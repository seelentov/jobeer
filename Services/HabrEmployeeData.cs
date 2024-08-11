
using Jobeer.Exceptions;
using Jobeer.Interfaces.Services;

namespace Jobeer.Services
{
    public class HabrEmployeeData : IEmployeeData
    {
        private readonly string _letter;
        public HabrEmployeeData(IConfiguration configuration)
        {
            _letter = configuration["HabrLetter"];
        }

        public string GetLetter()
        {
            return _letter;
        }

    }

}
