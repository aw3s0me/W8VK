using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKontakteModel.Entities
{
    public class User:IEntity
    {
        public string Uid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string Sex { get; set; }
        public string BirthDate { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string TimeZone { get; set; }
        public string Photo { get; set; }
        public string PhotoBig { get; set; }
        public string PhotoMedium { get; set; }
        public string Domain { get; set; }
        public string HasMobile { get; set; }
        public string Rate { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string University { get; set; }
        public string UniversityName { get; set; }
        public string Faculty { get; set; }
        public string FacultyName { get; set; }
        public string Graduation { get; set; }
        public string Online { get; set; }
    }
}
