namespace HRM
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }
        public DateTime HireDate { get; set; }

        public Employee() { }

        public Employee(int id, string name, double salary)
        {
            Id = id;
            Name = name;
            Salary = salary;
            HireDate = DateTime.Now;
        }

        public double CalculateSalary()
        {
            var experience = DateTime.Now.Year - HireDate.Year;
            var bonus = experience > 0 ? (Salary * 0.2 * experience) : 0;
            return Salary + bonus;
        }
    }
}