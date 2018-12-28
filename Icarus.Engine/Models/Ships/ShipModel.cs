using System;
using Icarus.Engine.Framework;
using Icarus.Engine.Framework.Modding;
using Icarus.Engine.Views.Ships;

namespace Icarus.Engine.Models.Ships
{
    public class ShipModel : IShipModel
    {
        public ShipModel([FromTemplate] double baseArmor, [FromTemplate] double baseHealth, [FromTemplate] string name)
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