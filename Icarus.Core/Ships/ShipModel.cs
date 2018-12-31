using System;
using Icarus.Engine.Models.Ships;

namespace Icarus.Core.Ships
{
    public class ShipModel : IShipModel
    {
        public ShipModel(double baseArmor, double baseHealth, string name)
        {
            BaseArmor = baseArmor;
            BaseHealth = baseHealth;
            Name = name;
            Id = Guid.NewGuid();
        }

        public string Name { get; }
        public double BaseArmor { get; }
        public double BaseHealth { get; }
        public Guid Id { get; }
    }
}