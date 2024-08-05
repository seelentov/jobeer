using Jobeer.Models.Base;

namespace Jobeer.Models
{
    public class Notification
    {
        public string Name {  get; set; }
        public string Company {  get; set; }

        public override string ToString()
        {
            return $"{Name}\n{Company}";
        }
    }
}
