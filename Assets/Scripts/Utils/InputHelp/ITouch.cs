using UnityEngine;

namespace InputHelp
{
    public interface ITouch
    {
        Vector2 Position { get; }
        bool StoppedThisFrame { get; }
        bool Active { get; }
        bool StartedThisFrame { get; }
        bool Equals(ITouch other);
    }
}
