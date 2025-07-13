namespace HRM;

public class PartTimeEmployee : Employee
{
  /// <summary>
  /// Ставка в час сотрудника, который работает неполный рабочий день
  /// </summary>
  public decimal HourlyRate { get; set; }

  /// <summary>
  /// Количество часов, которые работает сотрудник
  /// </summary>
  public int HoursWorked { get; set; }

  /// <summary>
  /// Скрытие неиспользуемого поля Salary
  /// </summary>
  private decimal Salary { get; set; }

  /// <summary>
  /// Расчет зарплата для сотрудника, который работает неполный рабочий день, на основе почасовой ставки и отработанных часов
  /// </summary>
  /// <returns>Зарплату сотрудника</returns>
  /// <exception cref="ArgumentException">Вызывается когда почасовая ставка или отработанные часы меньше нуля</exception>
  public override decimal CalculateSalary()
  {
    if (HourlyRate < 0 || HoursWorked < 0)
      throw new ArgumentException("Hourly rate and hours worked must be greater than 0");
    
    return HourlyRate * HoursWorked;
  }
}