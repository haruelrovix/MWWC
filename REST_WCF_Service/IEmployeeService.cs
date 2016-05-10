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
        void AddNewEmployee(EmployeeDataContract employee);

        [OperationContract]
        [WebInvoke(Method = "PUT",
            UriTemplate = "/UpdateEmployee",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        Task<bool> UpdateEmployee(EmployeeDataContract employee);

        [OperationContract]
        [WebInvoke(Method = "DELETE",
            UriTemplate = "/DeleteEmployee/{employeeId}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        Task<bool> DeleteEmployee(string employeeId);
    }
}
