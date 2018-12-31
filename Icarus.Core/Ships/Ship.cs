using System;
using Icarus.Engine.Framework;
using Icarus.Engine.Models.Ships;
using Icarus.Engine.Presenters.Ships;
using Icarus.Engine.Views.Ships;

namespace Icarus.Core.Ships
{
    public class Ship : IShipPresenter, ITemplateSpawnable
    {
        public IShipModel Model { get; set; }
        public IShipView View { get; set; }
        public Guid InstanceId { get; }

        public Ship(ShipModel model, ShipView view)
        {
            Model = model;
            View = view;
            InstanceId = Guid.NewGuid();
            
            View.DamageReceived += OnDamageReceived;
            View.WeaponFired += OnWeaponFired;
            View.DirectionalInputReceived += OnDirectionalInputReceived;
        }

        public virtual void OnDirectionalInputReceived(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public virtual void OnWeaponFired(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public virtual void OnDamageReceived(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}