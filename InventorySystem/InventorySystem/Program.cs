using System;
using System.Data.SQLite;

class Inventory
{
    private static SQLiteConnection conn;
static void Main()
{
    bool isJenkins = Environment.GetEnvironmentVariable("JENKINS_BUILD") == "true";

    InitializeDatabase();

    if (isJenkins)
    {
        Console.WriteLine("Jenkins build detected. Running automated tests...");

        // Run automated tests instead of interactive input
        AddItem("Test Item", 5);
        ViewInventory();
        
        conn.Close();
        return;
    }

    // Run interactive mode for local execution
    RunInteractiveMenu();
    conn.Close();
}


    static void InitializeDatabase()
    {
        conn = new SQLiteConnection("Data Source=inventory.db;Version=3;");
        conn.Open();

        string createTableQuery = "CREATE TABLE IF NOT EXISTS Items (Id INTEGER PRIMARY KEY, Name TEXT, Quantity INTEGER)";
        SQLiteCommand command = new SQLiteCommand(createTableQuery, conn);
        command.ExecuteNonQuery();
    }

  static void RunInteractiveMenu()
{
    bool running = true;

    while (running)
    {
        Console.WriteLine("\n1. Add Item\n2. View Inventory\n3. Exit");
        Console.Write("Select an option: ");
        string choice = Console.ReadLine();

        // Debugging: Print input received
        Console.WriteLine($"DEBUG: Received input: '{choice}'");

        if (string.IsNullOrWhiteSpace(choice)) // Handle null/empty input
        {
            Console.WriteLine("Invalid choice. Try again.");
            continue;
        }

        choice = choice.Trim(); // Remove extra spaces

        switch (choice)
        {
            case "1":
                Console.Write("Enter item name: ");
                string name = Console.ReadLine()?.Trim();
                Console.Write("Enter quantity: ");
                int quantity;

                while (!int.TryParse(Console.ReadLine(), out quantity))
                {
                    Console.Write("Invalid input. Enter a valid quantity: ");
                }

                AddItem(name, quantity);
                break;
            case "2":
                ViewInventory();
                break;
            case "3":
                running = false;
                break;
            default:
                Console.WriteLine("Invalid choice. Try again.");
                break;
        }
    }
}



    static void AddItem(string name, int quantity)
    {
        string query = "INSERT INTO Items (Name, Quantity) VALUES (@name, @quantity)";
        SQLiteCommand command = new SQLiteCommand(query, conn);
        command.Parameters.AddWithValue("@name", name);
        command.Parameters.AddWithValue("@quantity", quantity);
        command.ExecuteNonQuery();

        Console.WriteLine("Item added successfully!");
    }

    static void ViewInventory()
    {
        string query = "SELECT * FROM Items";
        SQLiteCommand command = new SQLiteCommand(query, conn);
        SQLiteDataReader reader = command.ExecuteReader();

        Console.WriteLine("\nInventory:");
        while (reader.Read())
        {
            Console.WriteLine($"ID: {reader["Id"]}, Name: {reader["Name"]}, Quantity: {reader["Quantity"]}");
        }
    }
}
