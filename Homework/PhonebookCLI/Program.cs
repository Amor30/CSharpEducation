using System;
using AbonentLibrary;

namespace PhonebookCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var phoneBook = Phonebook.Instance;
            Console.WriteLine("Телефонный справочник");
            while (true)
            {
                Console.WriteLine("\n1. Добавить абонента\n2. Удалить абонента\n3. Поиск по телефону\n4. Поиск по имени\n5. Выход");
                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Телефон (минимум 10 цифр, например 89827962236): ");
                        string phone = Console.ReadLine();
                        Console.Write("Имя (только буквы и пробелы, например Богдан): ");
                        string name = Console.ReadLine();
                        string addResult = phoneBook.AddAbonent(new Abonent { Phone = phone, Name = name });
                        Console.WriteLine(addResult);
                        break;

                    case "2":
                        Console.Write("Удалить по номеру телефона (минимум 10 цифр): ");
                        string removeResult = phoneBook.RemoveAbonent(Console.ReadLine());
                        Console.WriteLine(removeResult);
                        break;

                    case "3":
                        Console.Write("Поиск по телефону (минимум 10 цифр): ");
                        var (abonent, findPhoneError) = phoneBook.FindByPhone(Console.ReadLine());
                        if (findPhoneError != null)
                        {
                            Console.WriteLine(findPhoneError);
                        }
                        else
                        {
                            Console.WriteLine($"Найден: {abonent}");
                        }
                        break;

                    case "4":
                        Console.Write("Поиск по имени (буквы и пробелы): ");
                        var (abonents, findNameError) = phoneBook.FindByName(Console.ReadLine());
                        if (findNameError != null)
                        {
                            Console.WriteLine(findNameError);
                        }
                        else
                        {
                            Console.WriteLine($"Найдено {abonents.Count} абонент(ов):");
                            foreach (var a in abonents)
                            {
                                Console.WriteLine(a.ToString());
                            }
                        }
                        break;

                    case "5":
                        string saveResult = phoneBook.SaveToFile();
                        Console.WriteLine(saveResult);
                        Console.WriteLine("Выход из программы");
                        return;

                    default:
                        Console.WriteLine("Неправильное действие, попробуйте еще раз");
                        break;
                }
            }
        }
    }
}