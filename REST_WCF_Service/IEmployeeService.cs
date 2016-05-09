using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;

namespace REST_WCF_Service
{
    [ServiceContract]
    public interface IEmployeeService
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "/GetAllEmployee/",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        Task<List<EmployeeDataContract>> GetAllEmployee();

        [OperationContract]
        [WebGet(UriTemplate = "/GetEmployeeDetails/{employeeId}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        Task<EmployeeDataContract> GetEmployeeDetails(string employeeId);

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "/AddNewEmployee",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        void AddNewEmployee(EmployeeDataContract emp);

        [OperationContract]
        [WebInvoke(Method = "PUT",
            ResponseFormat = WebMessageFormat.Json)]
        void UpdateEmployee(EmployeeDataContract contact);

        [OperationContract]
        [WebInvoke(Method = "DELETE",
            UriTemplate = "DeleteEmployee/{employeeId}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        void DeleteEmployee(string employeeId);
    }
}
