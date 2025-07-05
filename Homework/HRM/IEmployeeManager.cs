namespace HRM
{
    public interface IEmployeeManager
    {
        void AddEmployee(string name, double salary);
        void UpdateEmployee(int id, string name, double salary);
        void DeleteEmployee(int id);
        void GetEmployeeInfo(int id);
        void ListAllEmployees();
    }
}