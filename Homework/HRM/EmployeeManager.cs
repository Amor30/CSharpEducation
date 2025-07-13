namespace HRM;

public class EmployeeManager : IEmployeeManager<Employee>
{
  #region Fields
  
  /// <summary>
  /// List of employees managed
  /// </summary>
  private List<Employee> employees;
  
  /// <summary>
  /// Next available Id for a new employee
  /// </summary>
  private int nextId;

  #endregion
  
  #region Constructors
  
  /// <summary>
  /// Initializes a new instance of the EmployeeManager 
  /// </summary>
  public EmployeeManager()
  {
    this.employees = new List<Employee>();
    this.nextId = 1;
  }

  #endregion
  
  #region Methods
  
  /// <summary>
  /// Adds a new employee
  /// </summary>
  /// <param name="employee">The employee to add</param>
  /// <exception cref="ArgumentNullException">Throw when the employee is null</exception>
  /// <exception cref="EmployeeAlreadyExistsException">Throw when an employee with the same id already  exists</exception>
  public void Add(Employee employee)
  {
    try
    {
      if (employee == null) throw new ArgumentNullException(nameof(employee));
      if (employees.Any(e => e.Id == employee.Id))
        throw new EmployeeAlreadyExistsException("Сотрудник с таким ID уже существует");
      
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
  /// Return an employee by their Id
  /// </summary>
  /// <param name="id">The Id of the employee to return</param>
  /// <returns>Throw when an unexpected error occurs</returns>
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
  /// Updates an existing employee's information
  /// </summary>
  /// <param name="employee">The employee with updated information</param>
  /// <exception cref="ArgumentNullException">Throw when the employee is null</exception>
  /// <exception cref="EmployeeNotFoundException">Throw when the employee with specified Id is not found</exception>
  public void Update(Employee employee)
  {
    try
    {
      if (employee == null) throw new ArgumentNullException(nameof(employee));
      var existing = employees.FirstOrDefault(e => e.Id == employee.Id);
      if (existing != null)
      {
        employees.Remove(existing);
        employees.Add(employee);
      }
      else
      {
        throw new EmployeeNotFoundException("Сотрудник с указанным ID не найден");
      }
    }
    catch (Exception e)
    {
      Console.WriteLine(e);
      throw;
    }
  }

  /// <summary>
  /// Deletes an employee by their Id
  /// </summary>
  /// <param name="id">The Id of the employee to delete</param>
  /// <exception cref="EmployeeNotFoundException">Theow when the employee with the specified Id is not found</exception>
  public void Delete(int id)
  {
    try
    {
      var employee = employees.FirstOrDefault(e => e.Id == id);
      if (employee != null)
      {
        employees.Remove(employee);
      }
      else
      {
        throw new EmployeeNotFoundException("Сотрудник с указанным ID не найден");
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