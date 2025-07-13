namespace HRM;

public abstract class Employee
{
  #region Свойства

  /// <summary>
  /// Имя работника
  /// </summary>
  public string Name { get; set; }

  /// <summary>
  /// Зарплата сотрудника
  /// </summary>
  public decimal Salary { get; set; }

  /// <summary>
  /// Уникальный идентификатор сотрудника
  /// </summary>
  public int Id { get; set; }

  #endregion

  #region Методы

  /// <summary>
  /// Расчет зарплаты для сотрудника
  /// </summary>
  /// <returns>Зарплату сотрудника</returns>
  public abstract decimal CalculateSalary();

  #endregion
}