using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AbonentLibrary;

public class Phonebook
{
    private static Phonebook _instance;
    private readonly Dictionary<string, Abonent> _abonents;

    private Phonebook()
    {
        _abonents = new Dictionary<string, Abonent>();
        LoadFromFile();
    }

    public static Phonebook Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Phonebook();
            }
            return _instance;
        }
    }

    public string IsValidPhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
        {
            return "Номер телефона не может быть пустым";
        }
        if (!Regex.IsMatch(phone, @"^\d{10,}$"))
        {
            return "Номер телефона должен содержать только цифры и минимум 10 цифр";
        }
        return null; // No error
    }

    public string IsValidName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return "Имя не может быть пустым";
        }
        return null; // No error
    }

    public string AddAbonent(Abonent abonent)
    {
        if (abonent == null)
        {
            return "Абонент не может быть null";
        }

        string phoneError = IsValidPhone(abonent.Phone);
        if (phoneError != null)
        {
            return phoneError;
        }

        string nameError = IsValidName(abonent.Name);
        if (nameError != null)
        {
            return nameError;
        }

        if (_abonents.ContainsKey(abonent.Phone))
        {
            return $"Номер телефона {abonent.Phone} уже существует";
        }

        _abonents[abonent.Phone] = abonent;
        return $"Абонент {abonent.Name} с номером {abonent.Phone} успешно добавлен";
    }

    public string RemoveAbonent(string phone)
    {
        string phoneError = IsValidPhone(phone);
        if (phoneError != null)
        {
            return phoneError;
        }

        if (_abonents.Remove(phone))
        {
            return $"Абонент с номером {phone} успешно удален";
        }
        return $"Абонент с номером {phone} не найден";
    }

    public (Abonent, string) FindByPhone(string phone)
    {
        string phoneError = IsValidPhone(phone);
        if (phoneError != null)
        {
            return (null, phoneError);
        }

        if (_abonents.TryGetValue(phone, out var abonent))
        {
            return (abonent, null);
        }
        return (null, $"Абонент с номером {phone} не найден");
    }

    public (List<Abonent>, string) FindByName(string name)
    {
        string nameError = IsValidName(name);
        if (nameError != null)
        {
            return (new List<Abonent>(), nameError);
        }

        var results = _abonents.Values.Where(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).ToList();
        if (results.Count == 0)
        {
            return (results, $"Нет абонентов с именем {name}");
        }
        return (results, null);
    }

    public string SaveToFile()
    {
        try
        {
            using (StreamWriter writer = new StreamWriter("phonebook.txt"))
            {
                foreach (var abonent in _abonents.Values)
                {
                    writer.WriteLine($"{abonent.Phone},{abonent.Name}");
                }
            }
            return "Данные успешно сохранены в файл";
        }
        catch (Exception ex)
        {
            return $"Ошибка сохранения в файл: {ex.Message}";
        }
    }

    private string LoadFromFile()
    {
        try
        {
            if (File.Exists("phonebook.txt"))
            {
                string[] lines = File.ReadAllLines("phonebook.txt");
                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    string[] parts = line.Split(',');
                    if (parts.Length == 2 && !string.IsNullOrWhiteSpace(parts[0]) && !string.IsNullOrWhiteSpace(parts[1]))
                    {
                        _abonents[parts[0]] = new Abonent { Phone = parts[0], Name = parts[1] };
                    }
                    else
                    {
                        return $"Пропущена недопустимая строка в файле: {line}";
                    }
                }
                return $"Загружено {_abonents.Count} абонентов из файла";
            }
            return "Файл phonebook.txt не найден. Создается новый файл";
        }
        catch (Exception ex)
        {
            return $"Ошибка загрузки файла phonebook.txt: {ex.Message}";
        }
    }
}