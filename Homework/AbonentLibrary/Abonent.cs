namespace AbonentLibrary;

public class Abonent
{
    public string Phone { get; set; }
    public string Name { get; set; }

    public override string ToString()
    {
        return $"{Name}, {Phone}";
    }
}