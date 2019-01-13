using System;
using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;

namespace CastleGrimtol.Project.Models
{
  public class Player : IPlayer
  {
    public string PlayerName { get; set; }
    public List<Item> Inventory { get; set; }
    public bool Alive = true;
    public Player(string playername)
    {
      PlayerName = playername;
      Inventory = new List<Item>();
    }
  }
}