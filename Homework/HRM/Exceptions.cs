namespace HRM;

public class InvalidInputException : Exception
{
  /// <summary>
  /// Initializes a new instance of the InvalidInputException
  /// </summary>
  /// <param name="message">The message that describes the error</param>
  public InvalidInputException(string message) : base(message) { }
}

public class EmployeeAlreadyExistsException : Exception
{
  /// <summary>
  /// Initializes a new instance of the EmployeeAlreadyExistsException
  /// </summary>
  /// <param name="message">The message that describes the error</param>
  public EmployeeAlreadyExistsException(string message) : base(message) { }
}

public class EmployeeNotFoundException : Exception
{
  /// <summary>
  /// Initializes a new instance of the EmployeeNotFoundException
  /// </summary>
  /// <param name="message">The message that describes the error</param>
  public EmployeeNotFoundException(string message) : base(message) { }
}