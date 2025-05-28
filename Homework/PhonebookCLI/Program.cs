using System;
using PhonebookManagement;
using AbonentLibrary;

namespace PhonebookCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var phoneBook = Phonebook.Instance;

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
                        phoneBook.AddAbonent(new Abonent { Phone = phone, Name = name });
                        break;
                    case "2":
                        Console.Write("Удалить по номеру телефона (минимум 10 цифр): ");
                        phoneBook.RemoveAbonent(Console.ReadLine());
                        break;
                    case "3":
                        Console.Write("Поиск по телефону (минимум 10 цифр): ");
                        phoneBook.FindByPhone(Console.ReadLine());
                        break;
                    case "4":
                        Console.Write("Поиск по имени (буквы и пробелы): ");
                        phoneBook.FindByName(Console.ReadLine());
                        break;
                    case "5":
                        phoneBook.SaveToFile();
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