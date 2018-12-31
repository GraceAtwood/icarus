using System;
using Icarus.Engine.Views.Ships;
using UnityEngine;

namespace Icarus.Core.Ships
{
    public class ShipView : MonoBehaviour, IShipView
    {
        public Mesh Mesh { get; private set; }

        public event EventHandler DamageReceived;
        public event EventHandler WeaponFired;
        public event EventHandler DirectionalInputReceived;
        public void MoveTo(Vector3 to)
        {
            throw new NotImplementedException();
        }

        public void MoveFromTo(Vector3 from, Vector3 to)
        {
            throw new NotImplementedException();
        }

        public void SetPosition(Vector3 pos)
        {
            throw new NotImplementedException();
        }

        public void ChangeAttitude()
        {
            throw new NotImplementedException();
        }

        public static ShipView Create(Mesh mesh)
        {
            var shipView = new GameObject().AddComponent<ShipView>();
            shipView.Mesh = mesh;
            
            return shipView;
        }
    }
}