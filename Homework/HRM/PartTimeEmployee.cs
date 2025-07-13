namespace HRM;

public class PartTimeEmployee : Employee
{
  /// <summary>
  /// Hourly rate for the part-time employee
  /// </summary>
  public decimal HourlyRate { get; set; }

  /// <summary>
  /// Number of hours worked by the part-time employee
  /// </summary>
  public int HoursWorked { get; set; }

  /// <summary>
  /// Hide unused field
  /// </summary>
  private decimal Salary { get; set; }

  /// <summary>
  /// Calculate the salary for the part-time employee based on hourly rate and hours worked
  /// </summary>
  /// <returns>The calculated salary</returns>
  /// <exception cref="ArgumentException">Throw when hourly rate or hours worked is negative</exception>
  public override decimal CalculateSalary()
  {
    if (HourlyRate < 0 || HoursWorked < 0)
      throw new ArgumentException("Ставка в час и количество часов должны быть больше 0");
    return HourlyRate * HoursWorked;
  }
}