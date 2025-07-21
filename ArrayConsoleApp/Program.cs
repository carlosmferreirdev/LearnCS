using System;

class Program
{
    static void Main()
    {
        // Texto inicial com boas-vindas em verde
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("---------------------------- Welcome to the Number checking program! ----------------------------\n");
        Console.ResetColor();

        // Explicação para o user sobre o funcionamento do programa e limpeza do terminal
        Console.WriteLine("This program will allow you to insert numbers into an array and check if they are odd or even.");
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
        Console.Clear();

        // Instruções e solicitação do tamanho do array ao user
        Console.WriteLine("Please chose a number above 0 to start inserting numbers into the array.\n");
        bool validInput = false;

        //Verificação do input do user para o tamanho do array
        int arraySize = 0;
        while (!validInput)
        {
            Console.Write("How many numbers do you want to insert into the array for validation? ");
            string sizeInput = Console.ReadLine();

            if (int.TryParse(sizeInput, out arraySize) && arraySize > 0)
            {
                validInput = true;
            }
            else if (string.IsNullOrWhiteSpace(sizeInput))
            {
                // Texto de erro em vermelho caso o input esteja vazio
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n\nInput cannot be empty. Please enter a valid number.\n");
                Console.ResetColor();
            }
            else
            {
                // Texto de erro em vermelho caso o input não seja válido
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n\nInvalid input. Please enter a valid number.\n");
                Console.ResetColor();
            }
        }

        // Criação do array com o tamanho definido pelo user
        int[] numbers = new int[arraySize];

        // Ciclo para inserir números no array com validação de input
        int count = 0;
        while (count < arraySize)
        {
            Console.Write($"Enter number #{count + 1}: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int number) && number > 0)
            {
                // Adiciona o número ao array e pede o número seguinte ao user
                numbers[count] = number;
                count++;
            }
            else if (string.IsNullOrWhiteSpace(input))
            {
                // Texto de erro em vermelho caso o input esteja vazio
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Input cannot be empty. Please enter a valid number.");
                Console.ResetColor();
            }
            else
            {
                // Texto de erro em vermelho caso o input não seja válido
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Please enter a valid number.");
                Console.ResetColor();
            }
        }

        // Ordenação do array por ordem crescente e limpeza do terminal
        Array.Sort(numbers);
        Console.Clear();

        // Exibição dos resultados
        Console.WriteLine("Here are the results:\n");
        foreach (int num in numbers)
        {
            // Verificação dos números pares e ímpares e exibição dos mesmos em cores diferentes para melhor visualização
            if (num % 2 == 0)
            {
                //Azul para números pares e reset da cor
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"{num} is an even number.");
                Console.ResetColor();
            }
            else
            {
                //Magenta para números ímpares e reset da cor
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"{num} is an odd number.");
                Console.ResetColor();
            }
        }

        // Mensagem final para encerrar o programa
        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}
