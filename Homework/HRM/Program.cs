using System;

namespace HRM
{
  /// <summary>
  /// Main program class for managing employees through a console interface
  /// </summary>
  public class Program
  {
    #region Main Method

    /// <summary>
    /// Entry point of the program
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
            throw new InvalidInputException("Неверный ввод. Введите число от 1 до 5");

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
              throw new InvalidInputException("Неверный ввод. Введите число от 1 до 5");
          }
        }
        catch (EmployeeAlreadyExistsException ex)
        {
          Console.WriteLine($"Ошибка: {ex.Message}");
        }
        catch (EmployeeNotFoundException ex)
        {
          Console.WriteLine($"Ошибка: {ex.Message}");
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

    #endregion

    #region Helper methods

    /// <summary>
    /// Displays the employee management menu to the console
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
    /// Handles the process of adding a new employee
    /// </summary>
    /// <param name="manager">The employee manager instance</param>
    /// <exception cref="InvalidInputException">Thrown when input is invalid</exception>
    private static void HandleAddEmployee(EmployeeManager manager)
    {
      Console.Write("Введите имя: ");
      string name = Console.ReadLine();

      Console.WriteLine("Выберите тип сотрудника:");
      Console.WriteLine("1. Полный рабочий день");
      Console.WriteLine("2. Неполный рабочий день");
      Console.Write("Выберите тип (1 или 2): ");
      if (!int.TryParse(Console.ReadLine(), out int typeChoice) || (typeChoice != 1 && typeChoice != 2))
        throw new InvalidInputException("Неверный выбор типа. Введите 1 или 2");

      Employee employee;
      if (typeChoice == 1)
      {
        Console.Write("Введите базовую зарплату: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal baseSalary) || baseSalary < 0)
          throw new InvalidInputException("Неверный формат зарплаты");
        employee = new FullTimeEmployee { Name = name, Salary = baseSalary };
      }
      else
      {
        Console.Write("Введите ставку за час: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal hourlyRate) || hourlyRate < 0)
          throw new InvalidInputException("Ставка за час должна быть не отрицательной");
        Console.Write("Введите количество часов: ");
        if (!int.TryParse(Console.ReadLine(), out int hoursWorked) || hoursWorked < 0)
          throw new InvalidInputException("Количество часов должно быть не отрицательным");
        employee = new PartTimeEmployee { Name = name, HourlyRate = hourlyRate, HoursWorked = hoursWorked };
      }

      manager.Add(employee);
    }

    /// <summary>
    /// Handles the process of updating an existing employee's info
    /// </summary>
    /// <param name="manager">The employee manager instance</param>
    /// <exception cref="InvalidInputException">Thrown when input is invalid</exception>
    /// <exception cref="EmployeeNotFoundException">Thrown when the employee is not found</exception>
    private static void HandleUpdateEmployee(EmployeeManager manager)
    {
      Console.Write("Введите Id сотрудника: ");
      if (!int.TryParse(Console.ReadLine(), out int id))
        throw new InvalidInputException("Неверный формат Id");

      Employee existing = manager.Get(id);
      if (existing != null)
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
        throw new EmployeeNotFoundException("Сотрудник с указанным Id не найден");
      }
    }

    /// <summary>
    /// Handles the process of return and print an employee's info
    /// </summary>
    /// <param name="manager">The employee manager instance</param>
    /// <exception cref="InvalidInputException">Thrown when input is invalid</exception>
    /// <exception cref="EmployeeNotFoundException">Thrown when the employee is not found</exception>
    private static void HandleGetEmployee(EmployeeManager manager)
    {
      Console.Write("Введите Id сотрудника: ");
      if (!int.TryParse(Console.ReadLine(), out int infoId))
        throw new InvalidInputException("Неверный формат Id");

      Employee employee = manager.Get(infoId);
      if (employee != null)
      {
        decimal salary = employee.CalculateSalary();
        Console.WriteLine($"Id: {employee.Id}, Name: {employee.Name}, Salary: {salary:F2}");
      }
      else
      {
        throw new EmployeeNotFoundException("Сотрудник с указанным Id не найден");
      }
    }

    /// <summary>
    /// Handles the process of deleting an employee
    /// </summary>
    /// <param name="manager">The employee manager instance</param>
    /// <exception cref="InvalidInputException">Thrown when input is invalid</exception>
    /// <exception cref="EmployeeNotFoundException">Thrown when the employee is not found</exception>
    private static void HandleDeleteEmployee(EmployeeManager manager)
    {
      Console.Write("Введите Id сотрудника для удаления: ");
      if (!int.TryParse(Console.ReadLine(), out int deleteId))
        throw new InvalidInputException("Неверный формат Id");

      Employee employee = manager.Get(deleteId);
      if (employee != null)
      {
        manager.Delete(deleteId);
        Console.WriteLine("Сотрудник удален");
      }
      else
      {
        throw new EmployeeNotFoundException("Сотрудник с указанным Id не найден");
      }
    }

    #endregion
  }
}