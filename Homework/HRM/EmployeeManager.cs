namespace HRM;

public class EmployeeManager : IEmployeeManager<Employee>
{
  #region Fields
  
  /// <summary>
  /// Список сотрудников
  /// </summary>
  private List<Employee> employees;
  
  /// <summary>
  /// Следующий свободный Id для сотрудника
  /// </summary>
  private int nextId;

  #endregion
  
  #region Constructors
  
  /// <summary>
  /// Инициализация списка и следующего Id
  /// </summary>
  public EmployeeManager()
  {
    this.employees = new List<Employee>();
    this.nextId = 1;
  }

  #endregion
  
  #region Methods
  
  /// <summary>
  /// Добавление нового сотрудника
  /// </summary>
  /// <param name="employee">Сотрудник которого нужно добавить</param>
  /// <exception cref="ArgumentNullException">Вызывается, когда сотрудник равен null</exception>
  /// <exception cref="EmployeeAlreadyExistsException">Вызывается, когда сотрудник с таким Id уже существует</exception>
  public void Add(Employee employee)
  {
    try
    {
      if (employee is null) throw new ArgumentNullException(nameof(employee));
      if (employees.Any(e => e.Id == employee.Id))
        throw new EmployeeAlreadyExistsException("An employee with this Id already exists.");
      
      employee.Id = nextId++;
      employees.Add(employee);
      Console.WriteLine("Сотрудник добавлен");
    }
    catch (Exception e)
    {
      Console.WriteLine(e);
      throw;
    }
  }

  /// <summary>
  /// Возвращает сотрудника по его Id
  /// </summary>
  /// <param name="id">Id сотрудника, которого нужно вернуть</param>
  /// <returns>Вызывается при возникновении непредвиденный ошибки</returns>
  public Employee Get(int id)
  {
    try
    {
      return employees.FirstOrDefault(e => e.Id == id);
    }
    catch (Exception e)
    {
      Console.WriteLine(e);
      throw;
    }
  }

  /// <summary>
  /// Обновляет информацию сотрудника
  /// </summary>
  /// <param name="employee">Сотрудник с обновленной информацией</param>
  /// <exception cref="ArgumentNullException">Вызывается, когда сотрудник равен null</exception>
  /// <exception cref="EmployeeNotFoundException">Вызывается, когда сотрудник с таким Id не найден</exception>
  public void Update(Employee employee)
  {
    try
    {
      if (employee is null) throw new ArgumentNullException(nameof(employee));
      var existing = employees.FirstOrDefault(e => e.Id == employee.Id);
      if (existing is not null)
      {
        employees.Remove(existing);
        employees.Add(employee);
      }
      else
      {
        throw new EmployeeNotFoundException("\nEmployee with specified Id not found");
      }
    }
    catch (Exception e)
    {
      Console.WriteLine(e);
      throw;
    }
  }

  /// <summary>
  /// Удаляет сотрудника по его Id
  /// </summary>
  /// <param name="id">Id сотрудника для удаления</param>
  /// <exception cref="EmployeeNotFoundException">Вызывается, когда сотрудник с таким Id не найден</exception>
  public void Delete(int id)
  {
    try
    {
      var employee = employees.FirstOrDefault(e => e.Id == id);
      if (employee is not null)
      {
        employees.Remove(employee);
      }
      else
      {
        throw new EmployeeNotFoundException("Employee with specified Id not found");
      }
    }
    catch (Exception e)
    {
      Console.WriteLine(e);
      throw;
    }
  }
  
  #endregion
}