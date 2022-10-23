using System;
using System.Data;
using System.Collections.Generic;
using Dapper;
using System.Linq;

namespace ORM_Dapper
{
    public class DapperDepartmentRepository : IDepartmentRepositorys
    {
        private readonly IDbConnection _connection;

        public DapperDepartmentRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            return _connection.Query<Department>("SELECT * FROM Departments;").ToList();
        }


        public void InsertDepartment(string newDepartmentName)
        {
            _connection.Execute("INSERT INTO DEPARTMENTS (Name) VALUES (@departmentName);",
new { departmentName = newDepartmentName });
        }


        public void DeleteDepartment(string departmentName)
        {
            _connection.Execute("DELETE FROM DEPARTMENTS WHERE Name = @departmentName;",
                new { departmentName = departmentName });
        }
    }
}
