using System;
using Icarus.Engine.Framework;
using UnityEngine;

namespace Icarus.Engine.Views.Ships
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

        private void Initialize([FromTemplate] Mesh mesh)
        {
            Mesh = mesh;
        }
    }
}