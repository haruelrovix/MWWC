using System.Runtime.Serialization;

namespace REST_WCF_Service
{
    [DataContract]
    public class EmployeeDataContract
    {
        /// <summary>
        /// Gets or sets the employee identifier.
        /// </summary>
        /// <value>
        /// The employee identifier.
        /// </value>
        [DataMember]
        public string EmployeeID
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [DataMember]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the joining date.
        /// </summary>
        /// <value>
        /// The joining date.
        /// </value>
        [DataMember]
        public string JoiningDate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        /// <value>
        /// The name of the company.
        /// </value>
        [DataMember]
        public string CompanyName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        [DataMember]
        public string Address
        {
            get;
            set;
        }
    }
}