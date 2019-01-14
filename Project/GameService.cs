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
    // private Dictionary<string, IRoom> Rooms = new Dictionary<string, IRoom>();



    //Initializes the game, creates rooms, their exits, and add items to rooms
    public void Setup()
    {
      Room Center = new Room("Center", "This is the Center room");
      Room Top = new Room("Top", "This is the Top Room");
      Room Left = new Room("Left", "This is the Left room");
      Room Right = new Room("Right", "This is the Right room");
      Room Bottom = new Room("Bottom", "This is the Bottom room");



      Item EightBall = new Item("Magic-Eightball", "Could help you but could also kill you depending on the answer");
      Item Sword = new Item("Sword", "Can be used to smash and destroy things");
      Item Depends = new Item("Depends", "Can be worn but they smell awful. Possible it could be used for protection");
      Item EvilWizard = new Item("Wizard", "The only way to escape is for you to Kill the Evil Wizard");
      Item Armor = new Item("Armor", "Can be worn and might help to protect you");

      Center.Exits.Add("north", Top);
      Center.Exits.Add("west", Left);
      Center.Exits.Add("east", Right);
      Center.Exits.Add("south", Bottom);
      Top.Exits.Add("south", Center);
      Left.Exits.Add("east", Center);
      Right.Exits.Add("west", Center);
      Bottom.Exits.Add("north", Center);

      // Left room has Eightball 
      // Right has Armor and depends
      // Top room is has Wizard, 
      // Bottom has sword
      Left.Items.Add(EightBall);
      Top.Items.Add(EvilWizard);
      Bottom.Items.Add(Sword);
      Right.Items.Add(Depends);
      Right.Items.Add(Armor);



      // Rooms.Clear();
      // Rooms.Add(Center.Name, Center);
      // Rooms.Add(Top.Name, Top);
      // Rooms.Add(Left.Name, Left);
      // Rooms.Add(Right.Name, Right);
      // Rooms.Add(Bottom.Name, Bottom);

      CurrentRoom = Center;
    }

    //Restarts the game 
    public void Reset()
    {
      Console.Clear();
      if (!CurrentPlayer.Alive)
      {
        Console.WriteLine("If you are here you have given a poor performance");
        Console.WriteLine("You have gone and got yourself killed and forgot to use your brain you bucket of Puke!");
        Console.WriteLine("You have the option to go down in history as a coward or");
        Console.WriteLine("return and kill the Evil Wizard and return with honor to your Kingdom");
        Console.WriteLine("So whats it gonna be");
        Console.WriteLine("Are you going to be a coward to live with never ending shame");
        Console.WriteLine("or play again and return with honor?");
        Console.WriteLine("'play' or 'p' to play again, anything else to quit");
        string restart = Console.ReadLine().Trim();
        if (!(restart.ToLower() == "play" || restart.ToLower() == "p"))
        {
          Quit();
        }
      }
      StartGame();
    }

    //Setup and Starts the Game loop
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
          Console.WriteLine("Enter a real name you bucket of Puke! Uhh beggin yer pardon Sir. I'm just a lowly Sergeant");
        }
        if (PlayerName.ToLower() == "quit")
        {
          Quit();
        }
      }
      CurrentPlayer = new Player(PlayerName);
      Console.WriteLine("-------------------------------------------- ");
      Console.WriteLine("Type 'help' or 'h' to see a list of commands");
      Console.WriteLine("-------------------------------------------- ");
      Setup();
      GetUserInput();
    }

    //Gets the user input and calls the appropriate command
    public void GetUserInput()
    {
      while (CurrentPlayer.Alive)
      {
        Console.WriteLine($"What would you like to do {CurrentPlayer.PlayerName}!");
        String Input = Console.ReadLine().ToLower();
        String[] Command = Input.Split(" ");
        if (Command.Length <= 2)
        {
          switch (Command[0].ToLower())
          {
            case "go":
            case "g":
              if (Command.Length == 2)
              {
                Go(Command[1]);
                break;
              }
              else
                Console.WriteLine("Enter a valid command you bucket of Puke! Uhh beggin yer pardon Sir. I'm just a lowly Sergeant! Type 'help' to get a list of commands");
              break;
            case "help":
            case "h":
              Help();
              break;
            case "inv":
              Inventory();
              break;
            case "look":
            case "l":
              Look();
              break;
            case "quit":
            case "q":
              Quit();
              break;
            case "restart":
            case "r":
              Reset();
              break;
            case "take":
            case "t":
              if (Command.Length == 2)
              {
                TakeItem(Command[1]);
              }
              else
                Console.WriteLine("Enter a valid command you bucket of Puke! Uhh beggin yer pardon Sir. I'm just a lowly Sergeant! Type 'help' to get a list of commands");
              break;
            case "use":
            case "u":
              if (Command.Length == 2)
              {
                UseItem(Command[1]);
              }
              else
                Console.WriteLine("Enter a valid command you bucket of Puke! Uhh beggin yer pardon Sir. I'm just a lowly Sergeant! Type 'help' to get a list of commands");
              break;
            case "":
              break;
            default:
              Console.Clear();
              Console.WriteLine("You must give a valid command you bucket of Puke!");
              Console.WriteLine("Uhh beggin yer pardon Sir. I forgot who I was talkin to. I'm just a lowly Sergeant. Please forgive me.");
              Console.WriteLine("You can 'go' some where");
              Console.WriteLine("You can 'take' something ");
              Console.WriteLine("You can 'use' something like your brain for example! Uhh beggin yer pardon Sir");
              Console.WriteLine("You can type 'help' to get a list of commands.");
              Console.WriteLine("Or you can 'quit' like a coward! Uhh pardon again Sir");
              Console.WriteLine("Please don't send me to the Dungon, I'm just a lowly Sergeant");
              break;
          }
        }
        else
        {
          Console.WriteLine("Enter a valid command you bucket of Puke! Uhh beggin yer pardon Sir. I'm just a lowly Sergeant! Type 'help' to get a list of commands");
        }
      }
    }

    #region Console Commands

    //Stops the application
    public void Quit()
    {
      Console.Clear();
      Console.WriteLine("Come back when you get some courage!");
      System.Threading.Thread.Sleep(3000);
      System.Environment.Exit(1);
    }

    //Should display a list of commands to the console
    public void Help()
    {
      Console.Clear();
      Console.WriteLine("You can use the following commands");
      Console.WriteLine("'quit' or 'q' allows you to quit the game");
      Console.WriteLine("'help' or 'h' will show avaiable commands");
      Console.WriteLine("'go' or 'g' with a direction 'north', 'south','east', or 'west'");
      Console.WriteLine(@"'take'or 't' and an 'item name' will get an item");
      Console.WriteLine(@"   and put it into your inventory");
      Console.WriteLine(@"'use' or 'u' and an 'item name' will use an item");
      Console.WriteLine(@"   if its in your inventory or current room");
      Console.WriteLine("'inv' or 'i' will show you what is in your inventory");
      Console.WriteLine("'look' or 'l' will tell you about the current room you are in ");
    }

    //Validate CurrentRoom.Exits contains the desired direction
    //if it does change the CurrentRoom
    public void Go(string direction)
    {
      // determine if direction is valid
      if (CurrentRoom.Exits.ContainsKey(direction))
      {
        Console.Clear();
        CurrentRoom = CurrentRoom.Exits[direction];
        Console.WriteLine(CurrentRoom.Description);
      }
      else
      {
        Console.Clear();
        Console.WriteLine($"You peer into the blackness and see only a wall. {CurrentRoom.Description}");
      }
    }

    //When taking an item be sure the item is in the current room 
    //before adding it to the player inventory, Also don't forget to 
    //remove the item from the room it was picked up in
    public void TakeItem(string itemName)
    {

      Item choice = CurrentRoom.Items.Find(item => item.Name.ToLower() == itemName.ToLower());
      if (choice != null)
      {
        CurrentPlayer.Inventory.Add(choice);
        CurrentRoom.Items.Remove(choice);
        Console.WriteLine($"You have added '{choice.Name}' to you Inventory");
      }
      else
      {
        Console.WriteLine($"There is no such '{itemName}' in this room");
      }
    }

    //No need to Pass a room since Items can only be used in the CurrentRoom
    //Make sure you validate the item is in the room or player inventory before
    //being able to use the item
    public void UseItem(string itemName)
    {
      Item choice = CurrentPlayer.Inventory.Find(item => item.Name.ToLower() == itemName.ToLower());

      if (choice == null)
      {
        choice = CurrentRoom.Items.Find(item => item.Name.ToLower() == itemName.ToLower());
        switch (itemName)
        {
          case "magic-eightball":
            {
              System.Random rnd = new Random();
              int meb = rnd.Next(1, 4);
              if (meb == 1)
              {
                CurrentPlayer.Alive = false;
                Console.WriteLine("The was a chance for you to live");
                Console.WriteLine("This time the Eightball did not give");
                Console.WriteLine("Try agin and you will see that I can help to set you free");
                Console.WriteLine("YOU ARE DEAD!");
                Reset();
              }
              if (meb == 2)
              {
                Console.WriteLine("Fanshionable Armor you may wear");
                Console.WriteLine("Only do it if you dare");
                Console.WriteLine("It may not be what you think");
                Console.WriteLine("If you can stand the nasty stink");
              }
              if (meb == 3)
              {
                Console.WriteLine("Keep me with you if you must ");
                Console.WriteLine("but you might turn into dust");
                Console.WriteLine("Only with me you will see");
                Console.WriteLine("that I will help you to be free");
              }
              break;
            }
          case "sword":
            {
              if (CurrentRoom.Name != "Top")
              {
                Console.WriteLine("There is nothing to Kill here");
              }
              break;
            }
          case "armor":
            {
              CurrentPlayer.Alive = false;
              Console.WriteLine("As you try on the Armor it fits very well......wait");
              Console.WriteLine("What is that smell? It is your skin burning off of your bones!");
              Console.WriteLine("There must have been poison on the inside of the Armor!");
              Console.WriteLine("DAMN YOU WIZARD!");
              Console.WriteLine("You hear yourself screeming and then...YOU DIE!");
              Reset();
            }
            break;
          case "depends":
            {
              Console.WriteLine("As you put on the nasty smelling depends underware");
              Console.WriteLine("you are overwhelmed by the smell and everything goes dark");
              System.Threading.Thread.Sleep(3000);
              Console.WriteLine("Hours later you wake up though you can't smell anthing");
              Console.WriteLine("the depends fit quite nicely!");
            }
            break;
        }

      }

      if (choice != null)
      {
        // switch here
        switch (itemName)
        {
          case "magic-eightball":
            {
              System.Random rnd = new Random();
              int meb = rnd.Next(1, 4);
              if (meb == 1)
              {
                CurrentPlayer.Alive = false;
                Console.WriteLine("The was a chance for you to live");
                Console.WriteLine("This time the Eightball did not give");
                Console.WriteLine("Try agin and you will see that I can help to set you free");
                Reset();
              }
              if (meb == 2)
              {
                Console.WriteLine("Fanshionable Armor you may wear");
                Console.WriteLine("Only do it if you dare");
                Console.WriteLine("It may not be what you think");
                Console.WriteLine("If you can stand the nasty stink");
              }
              if (meb == 3)
              {
                Console.WriteLine("Keep me with you if you must ");
                Console.WriteLine("but you might turn into dust");
                Console.WriteLine("Only with me you will see");
                Console.WriteLine("that I will help you to be free");
              }
              break;
            }
          case "sword":
            {
              if (CurrentRoom.Name != "Top")
              {
                Console.WriteLine("There is nothing to Kill here");
              }

              else
              {
                Item depends = CurrentPlayer.Inventory.Find(i => i.Name.ToLower() == "depends");
                Item magic = CurrentPlayer.Inventory.Find(i => i.Name.ToLower() == "magic-eightball");
                if (depends != null && magic != null)
                {
                  Console.WriteLine("You Swing your Sword and Kill the Evil Wizard!");
                  Console.WriteLine("As the Wizard is dieing, he curses you and asks");
                  Console.WriteLine("HOW DID YOU KNOW? ");
                  Console.WriteLine("MY SPELL SHOULD HAVE WORKED! CURSE YOU!");
                  Console.WriteLine("Yu look down and the dirty depends ");
                  Console.WriteLine("transforms into an indistuctable set of armor");
                  Console.WriteLine("You return to your kindom victorious");
                  Console.WriteLine("and with you honor in tacked you bucket of puke!");
                  Console.WriteLine("Uhhh sorry Sir. I'm just a lowley Sergeant!");
                  Console.WriteLine(" ");
                  Console.WriteLine(" ");
                  Console.WriteLine("'play' or 'p' to play again, anything else to quit");
                  string restart = Console.ReadLine().Trim();
                  if (!(restart.ToLower() == "play" || restart.ToLower() == "p"))
                  {
                    Quit();
                  }
                  StartGame();
                }
                else
                {
                  Console.WriteLine("You Swing your sword at the Evil Wizard ");
                  Console.WriteLine("He just stands there and laughs at you");
                  Console.WriteLine("as your sword hits him and says ");
                  Console.WriteLine("Now you are going to DIE!");
                  Console.WriteLine("He casts a spell and your skin starts to burn");
                  Console.WriteLine("the last thing you hear is the Evil Wizard laughing!");
                  Console.WriteLine("YOU ARD DEAD ");
                  Reset();
                }
              }
              break;
            }

          case "armor":
            {
              CurrentPlayer.Alive = false;
              Console.WriteLine("As you try on the Armor it fits very well......wait");
              Console.WriteLine("What is that smell? It is your skin burning off of your bones!");
              Console.WriteLine("There must have been poison on the inside of the Armor!");
              Console.WriteLine("DAMN YOU WIZARD!");
              Console.WriteLine("You hear yourself screeming and then...YOU DIE!");
              Reset();
            }
            break;
          case "depends":
            {
              Console.WriteLine("As you put on the nasty smelling depends underware");
              Console.WriteLine("you are overwhelmed by the smell and everything goes dark");
              System.Threading.Thread.Sleep(3000);
              Console.WriteLine("Hours later you wake up though you can't smell anthing");
              Console.WriteLine("the depends fit quite nicely!");
            }
            break;
        }
      }
      else
      {
        Console.WriteLine($"There is no such '{itemName}' in this room or your inventory");
      }
    }


    //Print the list of items in the players inventory to the console
    public void Inventory()
    {
      Console.WriteLine("You currently have:");
      if (CurrentPlayer.Inventory.Count == 0)
      {
        Console.WriteLine("no items");
      }
      foreach (Item item in CurrentPlayer.Inventory)
      {
        Console.WriteLine(item.Name);
      }
    }

    //Display the CurrentRoom Description, Exits, and Items
    public void Look()
    {

      Console.Clear();
      Console.WriteLine($"You look around the room and see {CurrentRoom.Description}");
      if (CurrentRoom.Items.Count != 0)
      {
        Console.WriteLine("You also see the following: ");
      }
      foreach (Item item in CurrentRoom.Items)
      {
        Console.WriteLine($"'{item.Name}'   {item.Description}");
      }
    }

    #endregion
    public GameService()
    {

    }

  }
}