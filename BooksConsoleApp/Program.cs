using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

class Program
{
    static void Main()
    {
        // Definir o nome do ficheiro JSON
        string fileName = "books.json";
        bool running = true;

        while (running)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n---------------------- Book Library Menu ----------------------\n");
            Console.ResetColor();
            Console.WriteLine("\nPlease choose an option:\n");
            Console.WriteLine("1. View books with 300+ pages sorted by number of pages");
            Console.WriteLine("2. Add new books to the file");
            Console.WriteLine("3. Clear all books from file");
            Console.WriteLine("4. Exit");
            string choice = Console.ReadLine();

            Console.Clear();

            switch (choice)
            {

                /*
                Carregamento dos livros do ficheiro JSON nas seguntes condições:
                    - Exclui livros com 300 páginas ou menos
                    - Livros ordenados de forma decrescente.
                */
                case "1":
                    var loadedBooks = LoadBooksFromJson(fileName);
                    var filteredBooks = GetSortedBooksOver300Pages(loadedBooks);

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("------------------ Books with more than 300 pages in descending order ------------------\n");
                    Console.ResetColor();


                    // Verificação da existência de livros com 300+ páginas
                    if (filteredBooks.Count > 0)
                        PrintBooks(filteredBooks);
                    else
                        Console.WriteLine("There are no books with more than 300 pages.");
                        Console.Write("\nPress any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                    break;

                // Adicionar novos livros ao ficheiro, usando um método onde o user insere os dados dos livros.
                case "2":
                    List<Book> newBooks = GetBooksFromUser();
                    SaveBooksToJson(newBooks, fileName);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nBooks added and saved to file.");
                    Console.ResetColor();
                    Console.Write("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    break;

                // Limpa o JSON.
                case "3":
                    ClearJsonFile(fileName);
                    Console.WriteLine("\nAll books have been cleared from the file.");
                    Console.Write("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    break;

                // Encerra o programa.
                case "4":
                    running = false;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\nExiting program. Until we meet again!\n");
                    Console.ResetColor();
                    break;

                default:
                    // Mensagem de erro para inputs inválidos
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid option. Please choose 1, 2, 3, or 4.");
                    Console.ResetColor();
                    Console.Write("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    break;
            }
        }
    }

    static void ClearJsonFile(string fileName)
    {
        File.WriteAllText(fileName, "[]");
    }

    static List<Book> GetBooksFromUser()
    {
        List<Book> books = new List<Book>();
        Console.Write("\nHow many books would you like to add? ");
        int count;


        // Validação de inputs que não correspondem a números acima de 0
        while (!int.TryParse(Console.ReadLine(), out count) || count <= 0)
        {
            Console.Write("Please enter a valid positive number: ");
        }

        for (int i = 1; i <= count; i++)
        {
            Console.WriteLine($"\nEntering details for book #{i}:");

            Console.Write("Title: ");
            string title = Console.ReadLine();

            Console.Write("Author: ");
            string author = Console.ReadLine();

            int pages;
            Console.Write("Number of pages: ");
            while (!int.TryParse(Console.ReadLine(), out pages) || pages <= 0)
            {
                Console.Write("Please enter a valid number of pages: ");
            }

            books.Add(new Book(title, author, pages));
        }

        return books;
    }

    static List<Book> GetSortedBooksOver300Pages(List<Book> books)
    {
        return books
            .Where(b => b.Pages > 300)
            .OrderByDescending(b => b.Pages)
            .ToList();
    }

    static void PrintBooks(List<Book> books)
    {
        foreach (var book in books)
        {
            Console.WriteLine($"\"{book.Title}\" by {book.Author}, {book.Pages} pages");
        }
    }

    static void SaveBooksToJson(List<Book> newBooks, string fileName)
    {
        List<Book> existingBooks = new List<Book>();

        if (File.Exists(fileName))
        {
            string json = File.ReadAllText(fileName);
            if (!string.IsNullOrWhiteSpace(json))
            {
                try
                {
                    existingBooks = JsonSerializer.Deserialize<List<Book>>(json);
                }
                catch
                {
                    existingBooks = new List<Book>();
                }
            }
        }

        existingBooks.AddRange(newBooks);

        string combinedJson = JsonSerializer.Serialize(existingBooks, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(fileName, combinedJson);
    }

    static List<Book> LoadBooksFromJson(string fileName)
    {
        if (!File.Exists(fileName))
            return new List<Book>();

        string json = File.ReadAllText(fileName);
        if (string.IsNullOrWhiteSpace(json))
            return new List<Book>();

        try
        {
            return JsonSerializer.Deserialize<List<Book>>(json);
        }
        catch
        {
            return new List<Book>();
        }
    }
}