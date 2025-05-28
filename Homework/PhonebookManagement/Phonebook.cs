using AbonentLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace PhonebookManagement
{
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
        
        private bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                Console.WriteLine("Ошибка: Номер телефона не может быть пустым");
                return false;
            }
            if (!Regex.IsMatch(phone, @"^\d{10,}$"))
            {
                Console.WriteLine("Ошибка: Номер телефона должен содержать только цифры и должно быть минимум 10 цифр");
                return false;
            }
            return true;
        }
        
        private bool IsValidName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Ошибка: Имя не может быть пустым");
                return false;
            }
            if (!Regex.IsMatch(name, @"^[a-zA-Z\s]+$"))
            {
                Console.WriteLine("Ошибка: Имя содержит только буквы и пробелы");
                return false;
            }
            return true;
        }

        public void AddAbonent(Abonent abonent)
        {
            if (abonent == null)
            {
                Console.WriteLine("Ошибка: Абонент не может быть null");
                return;
            }

            if (!IsValidPhone(abonent.Phone) || !IsValidName(abonent.Name))
            {
                return;
            }

            if (_abonents.ContainsKey(abonent.Phone))
            {
                Console.WriteLine($"Ошибка: Номер телефона {abonent.Phone} уже создан");
                return;
            }

            _abonents[abonent.Phone] = abonent;
            SaveToFile();
            Console.WriteLine($"Абонент {abonent.Name} с номером {abonent.Phone} уже добавлен");
        }

        public bool RemoveAbonent(string phone)
        {
            if (!IsValidPhone(phone))
            {
                return false;
            }

            if (_abonents.Remove(phone))
            {
                SaveToFile();
                Console.WriteLine($"Абонент с номером {phone} успешно удален");
                return true;
            }
            else
            {
                Console.WriteLine($"Ошибка: Абонент с номером {phone} не найден");
                return false;
            }
        }

        public Abonent FindByPhone(string phone)
        {
            if (!IsValidPhone(phone))
            {
                return null;
            }

            if (_abonents.TryGetValue(phone, out var abonent))
            {
                Console.WriteLine($"Найден: {abonent}");
                return abonent;
            }
            else
            {
                Console.WriteLine($"Абонент с номером {phone} не найден");
                return null;
            }
        }

        public List<Abonent> FindByName(string name)
        {
            if (!IsValidName(name))
            {
                return new List<Abonent>();
            }

            var results = _abonents.Values.Where(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).ToList();
            if (results.Count == 0)
            {
                Console.WriteLine($"Нет абонентов с именем {name}.");
            }
            else
            {
                Console.WriteLine($"Найдено {results.Count} абонент(ов) с именем {name}:");
                foreach (var a in results)
                {
                    Console.WriteLine(a.ToString());
                }
            }
            return results;
        }

        public void SaveToFile()
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
                Console.WriteLine("Сохранение в файле успешно ");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка сохранения в файле: {ex.Message}");
            }
        }

        private void LoadFromFile()
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
                            Console.WriteLine($"Пропуск недопустимой строки в файле: {line}");
                        }
                    }
                    Console.WriteLine($"Загрузка {_abonents.Count} абонентов из файла");
                }
                else
                {
                    Console.WriteLine("Файл phonebook.txt не найден. Создание файла с нуля");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки файла phonebook.txt: {ex.Message}");
            }
        }
    }
}