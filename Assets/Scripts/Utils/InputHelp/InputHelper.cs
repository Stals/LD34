using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace InputHelp
{

    public class InputHelper
    {

        public static Vector3 getTouchPosition()
        {
            return Input.mousePosition;
        }

        public static bool isTouchMoving()
        {
            if (Input.GetMouseButton(0))
            {
                return true;
            }

            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool isTouchBegin()
        {
            if (Input.GetMouseButtonDown(0))
            {
                return true;
            }

            if (Input.touchCount > 0)
            {
                if ((Input.GetTouch(0).phase == TouchPhase.Began))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool isTouchEnded()
        {

            if (Input.GetMouseButtonUp(0))
            {
                return true;
            }

            if (Input.touchCount > 0)
            {
                if ((Input.GetTouch(0).phase == TouchPhase.Ended) ||
                    (Input.GetTouch(0).phase == TouchPhase.Canceled))
                {

                    return true;
                }
            }
            return false;
        }

        public static List<ITouch> JustActivatedITouches
        {
            get
            {
                List<ITouch> result = new List<ITouch>();
                foreach (var t in Input.touches)
                {
                    ITouch touchAdapter = new TouchAdapter(t);
                    if (touchAdapter.StartedThisFrame)
                    {
                        result.Add(touchAdapter);
                    }
                }
                if ((Application.platform != RuntimePlatform.Android) &&
                    (Application.platform != RuntimePlatform.IPhonePlayer))
                {
                    var mouseAdapter = new MouseAdapter();
                    if (mouseAdapter.StartedThisFrame)
                        result.Add(mouseAdapter);
                }
                return result;
            }
        }
    }

}
