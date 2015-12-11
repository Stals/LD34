using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace InputHelp
{
    class MouseAdapter : ITouch
    {
        private const int TouchButtonNumber = 0;

        public bool StoppedThisFrame
        {
            get
            {
                return Input.GetMouseButtonUp(TouchButtonNumber);
            }
        }

        public Vector2 Position
        {
            get
            {
                return Input.mousePosition;
            }
        }

        public bool Active
        {
            get
            {
                return Input.GetMouseButton(TouchButtonNumber);
            }
        }

        public bool StartedThisFrame
        {
            get
            {
                return Input.GetMouseButtonDown(TouchButtonNumber);
            }
        }

        public bool Equals(ITouch other)
        {
            return (other is MouseAdapter);
        }
    }
}
