using System;

namespace HRM
{
  /// <summary>
  /// Основной класс программы для управления сотрудниками через консольное приложение
  /// </summary>
  public class Program
  {
    #region Main Method

    /// <summary>
    /// Точка входа в программу
    /// </summary>
    public static void Main()
    {
      EmployeeManager manager = new EmployeeManager();

      while (true)
      {
        try
        {
          DisplayMenu();
          if (!int.TryParse(Console.ReadLine(), out int choice))
            throw new InvalidInputException("Invalid input. Please enter a number between 1 and 5");

          switch (choice)
          {
            case 1:
              HandleAddEmployee(manager);
              break;
            case 2:
              HandleUpdateEmployee(manager);
              break;
            case 3:
              HandleGetEmployee(manager);
              break;
            case 4:
              HandleDeleteEmployee(manager);
              break;
            case 5:
              return;
            default:
              throw new InvalidInputException("Invalid input. Please enter a number between 1 and 5");
          }
        }
        catch (EmployeeAlreadyExistsException ex)
        {
          Console.WriteLine($"Error: {ex.Message}");
        }
        catch (EmployeeNotFoundException ex)
        {
          Console.WriteLine($"Error: {ex.Message}");
        }
        catch (InvalidInputException ex)
        {
          Console.WriteLine($"Error: {ex.Message}");
        }
        catch (Exception ex)
        {
          Console.WriteLine($"Unexpected error: {ex.Message}");
        }
      }
    }

    #endregion

    #region Helper methods

    /// <summary>
    /// Выводит меню управления сотрудниками на консоль
    /// </summary>
    private static void DisplayMenu()
    {
      Console.WriteLine("\nМеню управления сотрудниками:");
      Console.WriteLine("1. Добавить сотрудника");
      Console.WriteLine("2. Обновить данные сотрудника");
      Console.WriteLine("3. Получить информацию о сотруднике");
      Console.WriteLine("4. Удалить сотрудника");
      Console.WriteLine("5. Выход");
      Console.Write("Выберите действие: ");
    }

    /// <summary>
    /// Управляет процессом добавления нового сотрудника
    /// </summary>
    /// <param name="manager">Экземпляр менеджера сотрудников</param>
    /// <exception cref="InvalidInputException">Вызывается, если входные данные некорректны</exception>
    private static void HandleAddEmployee(EmployeeManager manager)
    {
      Console.Write("Введите имя: ");
      string name = Console.ReadLine();

      Console.WriteLine("Выберите тип сотрудника:");
      Console.WriteLine("1. Полный рабочий день");
      Console.WriteLine("2. Неполный рабочий день");
      Console.Write("Выберите тип (1 или 2): ");
      if (!int.TryParse(Console.ReadLine(), out int typeChoice) || (typeChoice != 1 && typeChoice != 2))
        throw new InvalidInputException("Invalid type selection. Please enter 1 or 2");

      Employee employee;
      if (typeChoice == 1)
      {
        Console.Write("Введите базовую зарплату: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal baseSalary) || baseSalary < 0)
          throw new InvalidInputException("Incorrect salary format");
        
        employee = new FullTimeEmployee { Name = name, Salary = baseSalary };
      }
      else
      {
        Console.Write("Введите ставку за час: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal hourlyRate) || hourlyRate < 0)
          throw new InvalidInputException("The hourly rate must not be negative.");
        
        Console.Write("Введите количество часов: ");
        if (!int.TryParse(Console.ReadLine(), out int hoursWorked) || hoursWorked < 0)
          throw new InvalidInputException("The hours worked must not be negative");
        
        employee = new PartTimeEmployee { Name = name, HourlyRate = hourlyRate, HoursWorked = hoursWorked };
      }

      manager.Add(employee);
    }

    /// <summary>
    /// Управляет процессом обновления информации о существующем сотруднике
    /// </summary>
    /// <param name="manager">Экземпляр менеджера сотрудников</param>
    /// <exception cref="InvalidInputException">Вызывается, когда входные данные некорректны</exception>
    /// <exception cref="EmployeeNotFoundException">Вызывается, когда сотрудник не найден по его Id</exception>
    private static void HandleUpdateEmployee(EmployeeManager manager)
    {
      Console.Write("Введите Id сотрудника: ");
      if (!int.TryParse(Console.ReadLine(), out int id))
        throw new InvalidInputException("Invalid Id format");

      Employee existing = manager.Get(id);
      if (existing is not null)
      {
        Console.Write("Введите новое имя (или нажмите Enter, чтобы пропустить): ");
        string newName = Console.ReadLine();
        if (!string.IsNullOrEmpty(newName))
          existing.Name = newName;

        Console.Write("Введите новую базовую зарплату (или 0, чтобы пропустить): ");
        if (decimal.TryParse(Console.ReadLine(), out decimal updatedSalary) && updatedSalary > 0)
          existing.Salary = updatedSalary;

        if (existing is PartTimeEmployee partTime)
        {
          Console.Write("Введите новую ставку за час (или 0, чтобы пропустить): ");
          if (decimal.TryParse(Console.ReadLine(), out decimal newHourlyRate) && newHourlyRate >= 0)
            partTime.HourlyRate = newHourlyRate;
          
          Console.Write("Введите новое количество часов (или 0, чтобы пропустить): ");
          if (int.TryParse(Console.ReadLine(), out int newHoursWorked) && newHoursWorked >= 0)
            partTime.HoursWorked = newHoursWorked;
        }

        manager.Update(existing);
        Console.WriteLine("Данные обновлены");
      }
      else
      {
        throw new EmployeeNotFoundException("Employee with specified Id not found");
      }
    }

    /// <summary>
    /// Управляет процессом возврата и выводит в консоль информацию о сотруднике
    /// </summary>
    /// <param name="manager">Экземпляр менеджера сотрудников</param>
    /// <exception cref="InvalidInputException">Вызывается, когда входные данные некорректны</exception>
    /// <exception cref="EmployeeNotFoundException">Вызывается, когда сотрудник с указанным Id не найден</exception>
    private static void HandleGetEmployee(EmployeeManager manager)
    {
      Console.Write("Введите Id сотрудника: ");
      if (!int.TryParse(Console.ReadLine(), out int infoId))
        throw new InvalidInputException("Invalid Id format");

      Employee employee = manager.Get(infoId);
      if (employee is not null)
      {
        decimal salary = employee.CalculateSalary();
        Console.WriteLine($"Id: {employee.Id}, Name: {employee.Name}, Salary: {salary:F2}");
      }
      else
      {
        throw new EmployeeNotFoundException("Employee with specified Id not found");
      }
    }

    /// <summary>
    /// Управляет процессом удаления сотрудника
    /// </summary>
    /// <param name="manager">Экземпляр менеджера сотрудников</param>
    /// <exception cref="InvalidInputException">Вызывается, когда входные данные некорректны</exception>
    /// <exception cref="EmployeeNotFoundException">Вызывается, когда сотрудник с указанным Id не найден</exception>
    private static void HandleDeleteEmployee(EmployeeManager manager)
    {
      Console.Write("Введите Id сотрудника для удаления: ");
      if (!int.TryParse(Console.ReadLine(), out int deleteId))
        throw new InvalidInputException("Invalid Id format");

      Employee employee = manager.Get(deleteId);
      if (employee is not null)
      {
        manager.Delete(deleteId);
        Console.WriteLine("Сотрудник удален");
      }
      else
      {
        throw new EmployeeNotFoundException("Employee with specified Id not found");
      }
    }

    #endregion
  }
}