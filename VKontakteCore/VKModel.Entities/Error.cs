using System.Runtime.Serialization;
using VKModel.Interfaces;

namespace VKModel
{
    namespace Entities
    {
        [DataContract(Name = "error")]
        public class Error
        {
            [DataMember(Name = "error_code")]
            public string ErrorCode { get; set; }

            [DataMember(Name = "error_msg")]
            public string ErrorMsg { get; set; }
        }

        [DataContract]
        public class ErrorResponse : IServiceResult
        {
            [DataMember(Name = "error")]
            public Error ErrorResult { get; set; }

            public string Error { get; set; }

            public bool ResponseIsSuccessful()
            {
                return ErrorResult != null;
            }
        }
    }
}