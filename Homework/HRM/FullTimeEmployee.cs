namespace HRM;

public class FullTimeEmployee : Employee
{
  /// <summary>
  /// Calculates the salary for a full-time employee
  /// </summary>
  /// <returns>The base salary of the employee</returns>
  public override decimal CalculateSalary()
  {
    return this.Salary;
  }
}