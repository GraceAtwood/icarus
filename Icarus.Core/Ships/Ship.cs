using System;
using Icarus.Engine.Framework;
using Icarus.Engine.Models.Ships;
using Icarus.Engine.Views.Ships;

namespace Icarus.Engine.Presenters.Ships
{
    public class Ship : IShipPresenter, ISpawnable
    {
        public IShipModel Model { get; set; }
        public IShipView View { get; set; }

        public Ship(IShipModel model, IShipView view)
        {
            Model = model;
            View = view;
            InstanceId = Guid.NewGuid();
            
            View.DamageReceived += OnDamageReceived;
            View.WeaponFired += OnWeaponFired;
            View.DirectionalInputReceived += OnDirectionalInputReceived;
        }

        private void OnDirectionalInputReceived(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnWeaponFired(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnDamageReceived(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public Guid InstanceId { get; }
    }
}