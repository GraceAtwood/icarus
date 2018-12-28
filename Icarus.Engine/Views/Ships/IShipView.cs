using System;
using UnityEngine;

namespace Icarus.Engine.Views.Ships
{
    public interface IShipView : IView
    {
        event EventHandler DamageReceived;
        event EventHandler WeaponFired;
        event EventHandler DirectionalInputReceived;
        
        void MoveTo(Vector3 to);
        void MoveFromTo(Vector3 from, Vector3 to);
        void SetPosition(Vector3 pos);
        void ChangeAttitude();

        Mesh Mesh { get; }
    }
}