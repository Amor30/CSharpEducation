namespace HRM;

/// <summary>
/// Defines methods for managing employees
/// </summary>
/// <typeparam name="T">Type of employee to manage</typeparam>
public interface IEmployeeManager<T>
{
  /// <summary>
  /// Adds a new employee
  /// </summary>
  /// <param name="employee">The employee to add</param>
  void Add(T employee);

  /// <summary>
  /// Return an employee by their Id
  /// </summary>
  /// <param name="id">The Id of the employee to return</param>
  /// <returns></returns>
  T Get(int id);

  /// <summary>
  /// Updates an existing employee's info
  /// </summary>
  /// <param name="employee">The employee with updated info</param>
  void Update(T employee);

  /// <summary>
  /// Deletes and employee by their Id
  /// </summary>
  /// <param name="id">The Id of the employee to delete</param>
  void Delete(int id);
}