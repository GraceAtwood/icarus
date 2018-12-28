using Icarus.Engine.Models.Ships;
using Icarus.Engine.Views.Ships;

namespace Icarus.Engine.Presenters.Ships
{
    public interface IShipPresenter : IPresenter
    {
        IShipModel Model { get; set; }
        IShipView View { get; set; }
    }
}