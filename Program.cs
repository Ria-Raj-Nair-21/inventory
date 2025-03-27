using System;
using System.Collections.Generic;
using System.Data.SQLite;

class Inventory
{
    private static SQLiteConnection conn;
    
    static void Main()
    {
        InitializeDatabase();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n1. Add Item\n2. View Inventory\n3. Exit");
            Console.Write("Select an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddItem();
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

    static void AddItem()
    {
        Console.Write("Enter item name: ");
        string name = Console.ReadLine();
        Console.Write("Enter quantity: ");
        int quantity = int.Parse(Console.ReadLine());

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
