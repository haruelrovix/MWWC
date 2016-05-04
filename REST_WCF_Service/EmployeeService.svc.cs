using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace REST_WCF_Service
{
    public class EmployeeService : IEmployeeService
    {
        public async void AddNewEmployee(EmployeeDataContract employee)
        {
            MongoClient client = new MongoClient();
            IMongoDatabase database = client.GetDatabase("EmployeeData");
            IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("Employees");

            BsonDocument document = new BsonDocument
            {
                { "EmployeeID", employee.EmployeeID },
                { "Name", employee.Name },
                { "JoiningDate", employee.JoiningDate },
                { "CompanyName", employee.CompanyName },
                { "Address", employee.Address }
            };

            await collection.InsertOneAsync(document);
        }

        public void DeleteEmployee(string EmpId)
        {
        }

        public async Task<List<EmployeeDataContract>> GetAllEmployee()
        {
            List<EmployeeDataContract> empList = new List<EmployeeDataContract>();

            MongoClient client = new MongoClient();
            IMongoDatabase database = client.GetDatabase("EmployeeData");
            IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("Employees");
            var filter = Builders<BsonDocument>.Filter.Empty;
            List<BsonDocument> employees = await collection.Find(filter).ToListAsync();

            foreach (BsonDocument employee in employees)
            {
                empList.Add(new EmployeeDataContract
                {
                    EmployeeID = employee["EmployeeID"].AsString,
                    Name = employee["Name"].AsString,
                    JoiningDate = employee["JoiningDate"].AsString,
                    CompanyName = employee["CompanyName"].AsString,
                    Address = employee["Address"].AsString
                });
            }

            return empList;
        }

        public async Task<EmployeeDataContract> GetEmployeeDetails(string employeeId)
        {
            EmployeeDataContract emp = new EmployeeDataContract();

            MongoClient client = new MongoClient();
            IMongoDatabase database = client.GetDatabase("EmployeeData");
            IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("Employees");

            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("EmployeeID", employeeId);

            BsonDocument employee = await collection.Find(filter).FirstOrDefaultAsync();

            if (employee != null)
            {
                emp.EmployeeID = employee["EmployeeID"].AsString;
                emp.Name = employee["Name"].AsString;
                emp.JoiningDate = employee["JoiningDate"].AsString;
                emp.CompanyName = employee["CompanyName"].AsString;
                emp.Address = employee["Address"].AsString;
            }

            return emp;
        }

        public void UpdateEmployee(EmployeeDataContract employee)
        {
        }
    }
}
