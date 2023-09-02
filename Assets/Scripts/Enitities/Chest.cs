using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class Chest : Entity, IStorage
{
    public Inventory Inventory { get; set; }

}
