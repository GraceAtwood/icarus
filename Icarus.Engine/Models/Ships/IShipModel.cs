namespace Icarus.Engine.Models.Ships
{
    public interface IShipModel : IModel
    {
        string Name { get; }
        double BaseArmor { get; }
        double BaseHealth { get; }
    }
}