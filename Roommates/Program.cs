﻿using Roommates.Models;
using Roommates.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Roommates
{
    class Program
    {
        //  This is the address of the database.
        //  We define it here as a constant since it will never change.
        private const string CONNECTION_STRING = @"server=localhost\SQLExpress;database=Roommates;integrated security=true";

        static void Main(string[] args)
        {
            RoomRepository roomRepo = new RoomRepository(CONNECTION_STRING);
            ChoreRepository choreRepo = new ChoreRepository(CONNECTION_STRING);
            RoommateRepository roommateRepo = new RoommateRepository(CONNECTION_STRING);
            bool runProgram = true;
            while (runProgram)
            {
                string selection = GetMenuSelection();

                switch (selection)
                {
                    case ("Show all rooms"):
                        List<Room> rooms = roomRepo.GetAll();
                        foreach (Room r in rooms)
                        {
                            Console.WriteLine($"{r.Name} has an Id of {r.Id} and a max occupancy of {r.MaxOccupancy}");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Search for room"):
                        Console.Write("Room Id: ");
                        int id = int.Parse(Console.ReadLine());

                        Room room = roomRepo.GetById(id);

                        Console.WriteLine($"{room.Id} - {room.Name} Max Occupancy({room.MaxOccupancy})");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Add a room"):
                        Console.Write("Room name: ");
                        string name = Console.ReadLine();

                        Console.Write("Max occupancy: ");
                        int max = int.Parse(Console.ReadLine());

                        Room roomToAdd = new Room()
                        {
                            Name = name,
                            MaxOccupancy = max
                        };

                        roomRepo.Insert(roomToAdd);

                        Console.WriteLine($"{roomToAdd.Name} has been added and assigned an Id of {roomToAdd.Id}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Update a room"):
                        List<Room> roomOptions = roomRepo.GetAll();
                        foreach (Room r in roomOptions)
                        {
                            Console.WriteLine($"{r.Id} - {r.Name} Max Occupancy({r.MaxOccupancy})");
                        }

                        Console.Write("Which room would you like to update? ");
                        int selectedRoomId = int.Parse(Console.ReadLine());
                        Room selectedRoom = roomOptions.FirstOrDefault(r => r.Id == selectedRoomId);

                        Console.Write("New Name: ");
                        selectedRoom.Name = Console.ReadLine();

                        Console.Write("New Max Occupancy: ");
                        selectedRoom.MaxOccupancy = int.Parse(Console.ReadLine());

                        roomRepo.Update(selectedRoom);

                        Console.WriteLine("Room has been successfully updated.");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Delete a room"):
                        List<Room> deleteOptions = roomRepo.GetAll();
                        foreach (Room r in deleteOptions)
                        {
                            Console.WriteLine($"{r.Id} - {r.Name} Max Occupancy({r.MaxOccupancy})");
                        }

                        Console.Write("Which room would you like to delete? ");
                        int selRoomId = int.Parse(Console.ReadLine());
                        Room selRoom = deleteOptions.FirstOrDefault(r => r.Id == selRoomId);

                        try
                        {
                            roomRepo.Delete(selRoomId);
                        }
                        catch
                        {
                            Console.WriteLine("You can't delete this room, someone or something is living in it.");
                            Console.Write("Press any key to continue");
                            Console.ReadKey();
                            break;
                        }

                        Console.WriteLine("Room has been successfullly deleted.");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Show all chores"):
                        List<Chore> chores = choreRepo.GetAll();
                        foreach (Chore c in chores)
                        {
                            Console.WriteLine($"{c.Name} has an Id of {c.Id}.");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Show unassigned chores"):
                        List<Chore> unChores = choreRepo.GetUnassignedChores();
                        foreach (Chore c in unChores)
                        {
                            Console.WriteLine($"{c.Name} has an Id of {c.Id}.");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Search for chore"):
                        Console.Write("Chore Id: ");
                        int choreId = int.Parse(Console.ReadLine());

                        Chore chore = choreRepo.GetById(choreId);

                        Console.WriteLine($"{chore.Id} - {chore.Name}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Add a chore"):
                        Console.Write("Chore name: ");
                        string choreName = Console.ReadLine();

                        Chore choreToAdd = new Chore()
                        {
                            Name = choreName,
                        };

                        choreRepo.Insert(choreToAdd);

                        Console.WriteLine($"{choreToAdd.Name} has been added and assigned an Id of {choreToAdd.Id}.");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Update a chore"):
                        List<Chore> choreOptions = choreRepo.GetAll();
                        foreach (Chore c in choreOptions)
                        {
                            Console.WriteLine($"{c.Id} - {c.Name}");
                        }

                        Console.Write("Which chore would you like to update? ");
                        int selectedChoreId = int.Parse(Console.ReadLine());
                        Chore selectedChore = choreOptions.FirstOrDefault(c => c.Id == selectedChoreId);

                        Console.Write("New Name: ");
                        selectedChore.Name = Console.ReadLine();

                        choreRepo.Update(selectedChore);

                        Console.WriteLine("Chore has been successfully updated.");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Delete a chore"):
                        List<Chore> deleteChoreOptions = choreRepo.GetAll();
                        foreach (Chore c in deleteChoreOptions)
                        {
                            Console.WriteLine($"{c.Id} - {c.Name}");
                        }

                        Console.Write("Which chore would you like to delete? ");
                        int selChoreId = int.Parse(Console.ReadLine());
                        Chore selChore = deleteChoreOptions.FirstOrDefault(c => c.Id == selChoreId);

                        choreRepo.Delete(selChoreId);

                        Console.WriteLine("Chore has been successfully deleted.");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Assign chores"):
                        List<Roommate> roommates = roommateRepo.GetAll();
                        foreach (Roommate r in roommates)
                        {
                            Console.WriteLine($"{r.Id} - {r.FirstName}");
                        }
                        Console.Write("Choose Roommate Id: ");
                        int roommateSelection = int.Parse(Console.ReadLine());

                        List<Chore> choreList = choreRepo.GetAll();
                        foreach (Chore c in choreList)
                        {
                            Console.WriteLine($"{c.Name} has an id of {c.Id}.");
                        }
                        Console.Write("Choose Chore Id: ");
                        int choreSelection = int.Parse(Console.ReadLine());

                        choreRepo.AssignChore(roommateSelection, choreSelection);

                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Search for roommate"):
                        Console.Write("Roommate Id: ");
                        int roommateId = int.Parse(Console.ReadLine());

                        Roommate roommate = roommateRepo.GetById(roommateId);


                        Console.WriteLine($"{roommate.FirstName} Rent Portion({roommate.RentPortion}) RoomName({roommate.Room.Name})");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Exit"):
                        runProgram = false;
                        break;
                }
            }


        }

        static string GetMenuSelection()
        {
            Console.Clear();

            List<string> options = new List<string>()
            {
                "Show all rooms",
                "Search for room",
                "Add a room",
                "Update a room",
                "Delete a room",
                "Show all chores",
                "Show unassigned chores",
                "Search for chore",
                "Add a chore",
                "Update a chore",
                "Delete a chore",
                "Assign chores",
                "Search for roommate",
                "Exit"
            };

            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }

            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("Select an option > ");

                    string input = Console.ReadLine();
                    int index = int.Parse(input) - 1;
                    return options[index];
                }
                catch (Exception)
                {

                    continue;
                }
            }
        }
    }
}
