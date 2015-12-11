using System;
using UnityEngine;

namespace InputHelp
{
    class TouchAdapter : ITouch
    {
        private int id;

        public TouchAdapter(Touch touch)
        {
            id = touch.fingerId;
        }

        public bool StoppedThisFrame
        {
            get
            {
                return (Touch.phase == TouchPhase.Canceled)||(Touch.phase == TouchPhase.Ended);
            }
        }

        public Vector2 Position
        {
            get
            {
                return Touch.position;
            }
        }

        public bool Active
        {
            get
            {
                return (Touch.phase == TouchPhase.Moved)||
                    (Touch.phase == TouchPhase.Stationary);
            }
        }

        public bool StartedThisFrame
        {
            get
            {
                return (Touch.phase == TouchPhase.Began);
            }
        }

        public Touch Touch
        {
            get
            {
                foreach (var t in Input.touches)
                {
                    if (t.fingerId == id) return t;
                }
                return new Touch();
            }
        }

        public bool Equals(ITouch other)
        {
            if (!(other is TouchAdapter))
                return false;
            var otherTouch = other as TouchAdapter;
            return (otherTouch.Touch.fingerId == Touch.fingerId);
        }
    }
}
