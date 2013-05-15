using System;
using System.Text;
using VKModel.Entities;

namespace VKViewModels.ItemsViewModels
{
    public class UserViewModel : BaseViewModel
    {
        private readonly User _user;

        public UserViewModel(User user)
        {
            _user = user;
        }


        public string FullNameAndNickName
        {
            get 
            { 
                var result = new StringBuilder();
                result.Append(FirstName);
                result.Append(" ");
                result.Append(LastName);
                if(!String.IsNullOrEmpty(Nickname))
                {
                    result.Append("(");
                    result.Append(Nickname);
                    result.Append(")");
                }
                return result.ToString();
            }
        }
        //public UserViewModel()
        //{
        //}

        public string Uid { get { return _user.Uid; }}

        public string FirstName { get { return _user.FirstName; } }

        public string LastName { get { return _user.LastName; } }

        public string Nickname { get { return _user.NickName; } }

        public string Sex { get { return _user.Sex == "1" ? "М." : "Ж."; } }

        public string BrithDate { get { return _user.BirthDate; } }

        public string City { get { return _user.City; } }

        public string Country { get { return _user.Country; } }

        public string Timezone { get { return _user.TimeZone; } }

        public string Photo { get { return _user.Photo; } }

        public string PhotoMedium { get { return _user.PhotoMedium; } }

        public string PhotoBig { get { return _user.PhotoBig; } }

        public string Domain { get { return _user.Domain; } }

        public string HasMobile { get { return _user.HasMobile; } }

        public string Rate { get { return _user.Rate; } }

        public string HomePhone { get { return _user.HomePhone; } }

        public string MobilePhone { get { return _user.MobilePhone; } }

        public string University { get { return _user.University; } }

        public string UniversityName { get { return _user.UniversityName; } }

        public string Faculty { get { return _user.Faculty; } }

        public string FacultyName { get { return _user.FacultyName; } }

        public string Graduation { get { return _user.Graduation; } }

        public bool Online { get { return ((_user.Online=="1")); } }

    }
}
