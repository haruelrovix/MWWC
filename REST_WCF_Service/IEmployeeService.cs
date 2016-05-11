using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;

namespace REST_WCF_Service
{
    [ServiceContract]
    public interface IEmployeeService
    {
        /// <summary>
        /// Gets all employee.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "/GetAllEmployee/",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        Task<List<EmployeeDataContract>> GetAllEmployee();

        /// <summary>
        /// Gets the employee details.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "/GetEmployeeDetails/{employeeId}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        Task<EmployeeDataContract> GetEmployeeDetails(string employeeId);

        /// <summary>
        /// Adds the new employee.
        /// </summary>
        /// <param name="employee">The employee.</param>
        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "/AddNewEmployee",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        void AddNewEmployee(EmployeeDataContract employee);

        /// <summary>
        /// Updates the employee.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "PUT",
            UriTemplate = "/UpdateEmployee",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        Task<bool> UpdateEmployee(EmployeeDataContract employee);

        /// <summary>
        /// Deletes the employee.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "DELETE",
            UriTemplate = "/DeleteEmployee/{employeeId}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        Task<bool> DeleteEmployee(string employeeId);
    }
}
