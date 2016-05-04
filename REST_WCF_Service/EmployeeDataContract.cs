using System.Runtime.Serialization;

namespace REST_WCF_Service
{
    [DataContract]
    public class EmployeeDataContract
    {
        [DataMember]
        public string EmployeeID
        {
            get;
            set;
        }

        [DataMember]
        public string Name
        {
            get;
            set;
        }

        [DataMember]
        public string JoiningDate
        {
            get;
            set;
        }

        [DataMember]
        public string CompanyName
        {
            get;
            set;
        }

        [DataMember]
        public string Address
        {
            get;
            set;
        }
    }
}