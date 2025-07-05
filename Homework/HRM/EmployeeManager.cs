using System.Xml.Serialization;
using EmployeeManagement;

namespace HRM
{
    public class EmployeeManager : IEmployeeManager
    {
        private List<Employee> employees;
        private int nextId;
        private readonly string xmlFilePath;

        public EmployeeManager(string filePath)
        {
            xmlFilePath = filePath;
            employees = LoadEmployees();
            nextId = this.employees.Any() ? employees.Max(e => e.Id) + 1 : 1;
        }

        private List<Employee> LoadEmployees()
        {
            try
            {
                if (!File.Exists(xmlFilePath))
                    return new List<Employee>();

                using (var reader = new StreamReader(xmlFilePath))
                {
                    var serializer = new XmlSerializer(typeof(List<Employee>));
                    return (List<Employee>)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                throw new FileOperationException($"Ошибка при загрузке данных из XML: {ex.Message}");
            }
        }

        private void SaveEmployees()
        {
            try
            {
                using (var writer = new StreamWriter(xmlFilePath))
                {
                    var serializer = new XmlSerializer(typeof(List<Employee>));
                    serializer.Serialize(writer, employees);
                }
            }
            catch (Exception ex)
            {
                throw new FileOperationException($"Ошибка при сохранении данных в XML: {ex.Message}");
            }
        }

        public void AddEmployee(string name, double salary)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    throw new InvalidInputException("Имя сотрудника не может быть пустым");
                if (salary <= 0)
                    throw new InvalidInputException("Зарплата должна быть больше 0");

                var employee = new Employee(nextId++, name, salary);
                employees.Add(employee);
                SaveEmployees();
                Console.WriteLine($"Сотрудник {name} успешно добавлен с ID: {employee.Id}");
            }
            catch (InvalidInputException)
            {
                throw;
            }
            catch (FileOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new EmployeeException($"Ошибка при добавлении сотрудника: {ex.Message}");
            }
        }

        public void UpdateEmployee(int id, string name, double salary)
        {
            try
            {
                var employee = employees.FirstOrDefault(e => e.Id == id);
                if (employee == null)
                    throw new EmployeeException($"Сотрудник с ID {id} не найден");

                if (!string.IsNullOrWhiteSpace(name))
                    employee.Name = name;
                if (salary > 0)
                    employee.Salary = salary;

                SaveEmployees();
                Console.WriteLine($"Данные сотрудника с ID {id} успешно обновлены");
            }
            catch (EmployeeException)
            {
                throw;
            }
            catch (FileOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new EmployeeException($"Ошибка при обновлении сотрудника: {ex.Message}");
            }
        }

        public void DeleteEmployee(int id)
        {
            try
            {
                var employee = employees.FirstOrDefault(e => e.Id == id);
                if (employee == null)
                    throw new EmployeeException($"Сотрудник с ID {id} не найден");

                employees.Remove(employee);
                SaveEmployees();
                Console.WriteLine($"Сотрудник с ID {id} успешно удален");
            }
            catch (EmployeeException)
            {
                throw;
            }
            catch (FileOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new EmployeeException($"Ошибка при удалении сотрудника: {ex.Message}");
            }
        }

        public void GetEmployeeInfo(int id)
        {
            try
            {
                var employee = employees.FirstOrDefault(e => e.Id == id);
                if (employee == null)
                    throw new EmployeeException($"Сотрудник с ID {id} не найден");

                Console.WriteLine($"ID: {employee.Id}");
                Console.WriteLine($"Имя: {employee.Name}");
                Console.WriteLine($"Базовая зарплата: {employee.Salary:C}");
                Console.WriteLine($"Дата найма: {employee.HireDate.ToShortDateString()}");
                Console.WriteLine($"Итоговая зарплата: {employee.CalculateSalary():C}");
            }
            catch (EmployeeException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new EmployeeException($"Ошибка при получении информации: {ex.Message}");
            }
        }

        public void ListAllEmployees()
        {
            try
            {
                if (!employees.Any())
                    throw new EmployeeException("Список сотрудников пуст");

                Console.WriteLine("\nСписок всех сотрудников:");
                foreach (var employee in employees)
                {
                    Console.WriteLine($"ID: {employee.Id}, Имя: {employee.Name}, Зарплата: {employee.CalculateSalary():C}");
                }
            }
            catch (EmployeeException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new EmployeeException($"Ошибка при получении списка сотрудников: {ex.Message}");
            }
        }
    }
}