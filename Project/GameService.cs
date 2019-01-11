using System;
using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;
using CastleGrimtol.Project.Models;

namespace CastleGrimtol.Project
{
  public class GameService : IGameService
  {
    public IRoom CurrentRoom { get; set; }
    public Player CurrentPlayer { get; set; }
    private Dictionary<string, IRoom> Rooms = new Dictionary<string, IRoom>();

    public void GetUserInput()
    {
      Console.WriteLine("What would you like to do");
      String Input = Console.ReadLine().ToLower();
      String[] Command = Input.Split(" ");
      if (Command.Length > 2)
      {
        Console.WriteLine("Invalid Command!");
      }
      else
      {
        switch (Command[0].ToLower())
        {
          case "go":
            if (Command.Length == 2)
            {
              Go(Command[1]);
            }
            break;
          // case "help":

          //   break;
          // case "inv":

          //   break;
          // case "look":

          //   break;
          case "quit":
            Quit();
            break;
          // case "reset":

          //   break;
          // case "takeitem":

          //   break;
          // case "useitem":
          //   UseItem(Item.)
          //   break;
          default:

            break;
        }
      }

    }

    public void Go(string direction)
    {
      // determine if direction is valid
      if (!Command[0] == null)




      else
      {
        if (CurrentRoom.Go(input[1]) == null)
        {
          Console.Clear();
          Console.WriteLine("You peer into the blackness and see only a wall ahead, your path is blocked");
        }
        else
        {
          CurrentRoom = CurrentRoom.Go(input[1]);
          Console.Clear();
        }
        Room nextroom = (Room)CurrentRoom.Exits[direction];
        CurrentRoom = Rooms[CurrentRoom.Exits[direction].Name];
      }


    }

    public void Help()
    {
      Console.Clear();
      Console.WriteLine("Commands: ");

    }

    public void Inventory()
    {

    }

    public void Look()
    {

    }

    public void Quit()
    {
      Console.WriteLine("Come back when you get some courage!");
      System.Threading.Thread.Sleep(5000);
      System.Environment.Exit(1);
    }

    public void Reset()
    {

    }

    public void Setup()
    {
      Room Center = new Room("Center", "This is the Center room");
      Room Top = new Room("Top", "This is the Top Room");
      Room Left = new Room("Left", "This is the Left room");
      Room Right = new Room("Right", "This is the Right room");
      Room Bottom = new Room("Bottom", "This is the Bottom room");

      Center.Exits.Add("north", Top);
      Center.Exits.Add("west", Left);
      Center.Exits.Add("east", Right);
      Center.Exits.Add("south", Bottom);
      Top.Exits.Add("south", Center);
      Left.Exits.Add("east", Center);
      Right.Exits.Add("west", Center);
      Bottom.Exits.Add("north", Center);

      Item EightBall = new Item("Magic Eightball", "Could help you but could also kill you depending on the answer");
      Item Sword = new Item("Indistructable Sword", "Can be used to smash and destroy things");
      Item Key = new Item("Door key", "Can be used to open certain doors");

      Rooms.Clear();
      Rooms.Add(Center.Name, Center);
      Rooms.Add(Top.Name, Top);
      Rooms.Add(Left.Name, Left);
      Rooms.Add(Right.Name, Right);
      Rooms.Add(Bottom.Name, Bottom);

      CurrentRoom = Rooms["Center"];



    }

    public void StartGame()
    {
      Console.WriteLine("Welcome to the Dungeon");
      Console.WriteLine("Story goes here");
      Console.WriteLine("Enter your name to start this quest");

      // verify user entered some characters other than spaces or nothing. 
      string PlayerName = "";
      while (PlayerName == "")
      {
        PlayerName = Console.ReadLine().Trim();
        if (PlayerName == "")
        {
          Console.WriteLine("Enter a real name you bucket of Puke!");
        }
      }
      CurrentPlayer = new Player(PlayerName);
      Setup();
      GetUserInput();
    }

    public void TakeItem(string itemName)
    {

    }

    public void UseItem(string itemName)
    {

    }
    public GameService()
    {

    }
  }
}