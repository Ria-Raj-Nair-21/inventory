using System;
using System.Collections.Generic;

class Program
{
    static List<string> inventory = new List<string>();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nInventory System");
            Console.WriteLine("1. Add Item");
            Console.WriteLine("2. Remove Item");
            Console.WriteLine("3. View Inventory");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddItem();
                    break;
                case "2":
                    RemoveItem();
                    break;
                case "3":
                    ViewInventory();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void AddItem()
    {
        Console.Write("Enter item name: ");
        string item = Console.ReadLine();
        inventory.Add(item);
        Console.WriteLine($"{item} added to inventory.");
    }

    static void RemoveItem()
    {
        Console.Write("Enter item name to remove: ");
        string item = Console.ReadLine();
        if (inventory.Remove(item))
        {
            Console.WriteLine($"{item} removed from inventory.");
        }
        else
        {
            Console.WriteLine("Item not found.");
        }
    }

    static void ViewInventory()
    {
        Console.WriteLine("\nCurrent Inventory:");
        if (inventory.Count == 0)
        {
            Console.WriteLine("Inventory is empty.");
        }
        else
        {
            foreach (var item in inventory)
            {
                Console.WriteLine("- " + item);
            }
        }
    }
}
