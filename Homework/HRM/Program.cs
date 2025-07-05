using System;

namespace HRM
{
    class Program
    {
        static void Main(string[] args)
        {
            var manager = new EmployeeManager("employees.xml");
            while (true)
            {
                try
                {
                    Console.WriteLine("\nМеню управления сотрудниками:");
                    Console.WriteLine("1. Добавить сотрудника");
                    Console.WriteLine("2. Обновить данные сотрудника");
                    Console.WriteLine("3. Получить информацию о сотруднике");
                    Console.WriteLine("4. Показать всех сотрудников");
                    Console.WriteLine("5. Удалить сотрудника");
                    Console.WriteLine("6. Выход");
                    Console.Write("Выберите действие: ");

                    if (!int.TryParse(Console.ReadLine(), out var choice))
                        throw new InvalidInputException("Неверный ввод. Введите число от 1 до 6");

                    switch (choice)
                    {
                        case 1:
                            Console.Write("Введите имя: ");
                            var name = Console.ReadLine();
                            Console.Write("Введите базовую зарплату: ");
                            if (!double.TryParse(Console.ReadLine(), out var salary))
                                throw new InvalidInputException("Неверный формат зарплаты");
                            
                            manager.AddEmployee(name, salary);
                            break;

                        case 2:
                            Console.Write("Введите ID сотрудника: ");
                            if (!int.TryParse(Console.ReadLine(), out var id))
                                throw new InvalidInputException("Неверный формат ID");
                            
                            Console.Write("Введите новое имя (или нажмите Enter, чтобы пропустить): ");
                            var newName = Console.ReadLine();
                            Console.Write("Введите новую базовую зарплату (или 0, чтобы пропустить): ");
                            if (!double.TryParse(Console.ReadLine(), out var newSalary))
                                throw new InvalidInputException("Неверный формат зарплаты");
                            
                            manager.UpdateEmployee(id, newName, newSalary);
                            break;

                        case 3:
                            Console.Write("Введите ID сотрудника: ");
                            if (!int.TryParse(Console.ReadLine(), out var infoId))
                                throw new InvalidInputException("Неверный формат ID");
                            
                            manager.GetEmployeeInfo(infoId);
                            break;

                        case 4:
                            manager.ListAllEmployees();
                            break;

                        case 5:
                            Console.Write("Введите ID сотрудника для удаления: ");
                            if (!int.TryParse(Console.ReadLine(), out var deleteId))
                                throw new InvalidInputException("Неверный формат ID");
                            
                            manager.DeleteEmployee(deleteId);
                            break;

                        case 6:
                            return;

                        default:
                            throw new InvalidInputException("Неверный выбор. Введите число от 1 до 6");
                    }
                }
                catch (InvalidInputException ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Непредвиденная ошибка: {ex.Message}");
                }
            }
        }
    }
}