namespace HRM;

/// <summary>
/// Определяет методы управления сотрудниками
/// </summary>
/// <typeparam name="T">Тип сотрудника для управления</typeparam>
public interface IEmployeeManager<T>
{
  /// <summary>
  /// Добавляет нового сотрудника
  /// </summary>
  /// <param name="employee">Сотрудник для добавления</param>
  void Add(T employee);

  /// <summary>
  /// Возвращает сотрудника по его Id
  /// </summary>
  /// <param name="id">Id сотрудника, которого нужно вернуть</param>
  /// <returns></returns>
  T Get(int id);

  /// <summary>
  /// Обновляет информацию у существующего сотрудника
  /// </summary>
  /// <param name="employee">Сотрудник с обновленной информацией</param>
  void Update(T employee);

  /// <summary>
  /// Удаление сотрудника по его Id
  /// </summary>
  /// <param name="id">Id сотрудника, которого нужно удалить</param>
  void Delete(int id);
}