namespace HRM;

public class FullTimeEmployee : Employee
{
  /// <summary>
  /// Расчет зарплата для сотрудника, который работает полный день
  /// </summary>
  /// <returns>Зарплату сотрудника</returns>
  public override decimal CalculateSalary()
  {
    return this.Salary;
  }
}