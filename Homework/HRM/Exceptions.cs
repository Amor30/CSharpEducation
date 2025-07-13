namespace HRM;

public class InvalidInputException : Exception
{
  /// <summary>
  /// Инициализация нового экземпляра InvalidInputException
  /// </summary>
  /// <param name="message">Сообщение, которое описывает ошибку</param>
  public InvalidInputException(string message) : base(message) { }
}

public class EmployeeAlreadyExistsException : Exception
{
  /// <summary>
  /// Инициализация нового экземпляра EmployeeAlreadyExistsException
  /// </summary>
  /// <param name="message">Сообщение, которое описывает ошибку</param>
  public EmployeeAlreadyExistsException(string message) : base(message) { }
}

public class EmployeeNotFoundException : Exception
{
  /// <summary>
  /// Инициализация нового экземпляра EmployeeNotFoundException
  /// </summary>
  /// <param name="message">Сообщение, которое описывает ошибку</param>
  public EmployeeNotFoundException(string message) : base(message) { }
}