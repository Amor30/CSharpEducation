namespace HRM;

public abstract class Employee
{
  #region Свойства

  /// <summary>
  /// Name of the employee
  /// </summary>
  public string Name { get; set; }

  /// <summary>
  /// Base salary of the employee
  /// </summary>
  public decimal Salary { get; set; }

  /// <summary>
  /// Unique identifier of the employee
  /// </summary>
  public int Id { get; set; }

  #endregion

  #region Методы

  /// <summary>
  /// Calculates the salary for the employee
  /// </summary>
  /// <returns>The calculated salary</returns>
  public abstract decimal CalculateSalary();

  #endregion
}