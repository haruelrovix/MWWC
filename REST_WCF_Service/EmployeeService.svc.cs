using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace REST_WCF_Service
{
    public class EmployeeService : IEmployeeService
    {
        #region Constants

        /// <summary>
        /// The database name
        /// </summary>
        private static readonly string DatabaseName = "EmployeeData";

        /// <summary>
        /// The collection name
        /// </summary>
        private static readonly string CollectionName = "Employees";

        /// <summary>
        /// The Document keys
        /// </summary>
        private static readonly string EmployeeID = "EmployeeID";
        private static readonly string EmployeeName = "Name";
        private static readonly string JoiningDate = "JoiningDate";
        private static readonly string CompanyName = "CompanyName";
        private static readonly string EmployeeAddress = "Address";

        #endregion

        #region Private Method(s)

        /// <summary>
        /// Gets the collection.
        /// </summary>
        /// <returns></returns>
        private IMongoCollection<BsonDocument> GetCollection()
        {
            MongoClient client = new MongoClient();
            IMongoDatabase database = client.GetDatabase(DatabaseName);

            return database.GetCollection<BsonDocument>(CollectionName);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the new employee.
        /// </summary>
        /// <param name="employee">The employee.</param>
        public async void AddNewEmployee(EmployeeDataContract employee)
        {
            IMongoCollection<BsonDocument> collection = GetCollection();

            BsonDocument document = new BsonDocument
            {
                { EmployeeID, employee.EmployeeID },
                { EmployeeName, employee.Name },
                { JoiningDate, employee.JoiningDate },
                { CompanyName, employee.CompanyName },
                { EmployeeAddress, employee.Address }
            };

            await collection.InsertOneAsync(document);
        }

        /// <summary>
        /// Deletes the employee.
        /// </summary>
        /// <param name=EmployeeID>The employee identifier.</param>
        /// <returns></returns>
        public async Task<bool> DeleteEmployee(string employeeId)
        {
            IMongoCollection<BsonDocument> collection = GetCollection();
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq(EmployeeID, employeeId);

            DeleteResult result = await collection.DeleteOneAsync(filter);

            return result.DeletedCount > 0;
        }

        /// <summary>
        /// Gets all employee.
        /// </summary>
        /// <returns></returns>
        public async Task<List<EmployeeDataContract>> GetAllEmployee()
        {
            IMongoCollection<BsonDocument> collection = GetCollection();
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Empty;
            List<BsonDocument> employees = await collection.Find(filter).ToListAsync();

            List<EmployeeDataContract> empList = new List<EmployeeDataContract>();
            foreach (BsonDocument employee in employees)
            {
                empList.Add(new EmployeeDataContract
                {
                    EmployeeID = employee[EmployeeID].AsString,
                    Name = employee[EmployeeName].AsString,
                    JoiningDate = employee[JoiningDate].AsString,
                    CompanyName = employee[CompanyName].AsString,
                    Address = employee[EmployeeAddress].AsString
                });
            }

            return empList;
        }

        /// <summary>
        /// Gets the employee details.
        /// </summary>
        /// <param name=EmployeeID>The employee identifier.</param>
        /// <returns></returns>
        public async Task<EmployeeDataContract> GetEmployeeDetails(string employeeId)
        {
            IMongoCollection<BsonDocument> collection = GetCollection();

            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq(EmployeeID, employeeId);

            BsonDocument employee = await collection.Find(filter).FirstOrDefaultAsync();

            EmployeeDataContract emp = null;
            if (employee != null)
            {
                emp = new EmployeeDataContract();

                emp.EmployeeID = employee[EmployeeID].AsString;
                emp.Name = employee[EmployeeName].AsString;
                emp.JoiningDate = employee[JoiningDate].AsString;
                emp.CompanyName = employee[CompanyName].AsString;
                emp.Address = employee[EmployeeAddress].AsString;
            }

            return emp;
        }

        /// <summary>
        /// Updates the employee.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <returns></returns>
        public async Task<bool> UpdateEmployee(EmployeeDataContract employee)
        {
            IMongoCollection<BsonDocument> collection = GetCollection();

            BsonDocument document = new BsonDocument
            {
                { EmployeeID, employee.EmployeeID },
                { EmployeeName, employee.Name },
                { JoiningDate, employee.JoiningDate },
                { CompanyName, employee.CompanyName },
                { EmployeeAddress, employee.Address }
            };

            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq(EmployeeID, employee.EmployeeID);

            ReplaceOneResult result = await collection.ReplaceOneAsync(filter, document);

            return result.ModifiedCount > 0;
        }

        #endregion
    }
}
