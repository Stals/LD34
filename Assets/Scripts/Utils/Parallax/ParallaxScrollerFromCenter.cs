using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InputHelp;

[RequireComponent(typeof(SpriteSmoothMovement))]
public class ParallaxScrollerFromCenter : MonoBehaviour
{

    public float scrollCoef = 1.0f;

    float accelMultiplier = 5.0f;
    Queue<Vector2> accelQueue = new Queue<Vector2>();

    SpriteSmoothMovement parallaxer;

	// Use this for initialization
	void Start () {
        parallaxer = GetComponent<SpriteSmoothMovement>();
	}

    Vector2 calculateOffsetFromMouse()
    {
        var camSizeHeight = Camera.main.pixelHeight;
        var camSizeWidth = Camera.main.pixelWidth;
        Vector2 mousePosFromCenter = (Vector2)InputHelper.getTouchPosition() - 0.5f * new Vector2(camSizeWidth, camSizeHeight);
        Vector2 offset = new Vector2(mousePosFromCenter.x / camSizeWidth, mousePosFromCenter.y / camSizeHeight);
        return offset;
    }

    void AddToAccelQuee(Vector2 vec)
    {
        accelQueue.Enqueue(vec);
        if (accelQueue.Count > 20)
        {
            accelQueue.Dequeue();
        }
    }

    Vector2 AccelOffset
    {
        get
        {
            Vector2 res = new Vector2();
            foreach (var v in accelQueue)
            {
                res += v;
            }
            return (1.0f / (float)accelQueue.Count) * res;
        }
    }

    Vector2 calculateOffsetFromAcceleration()
    {
        Vector2 offset = new Vector2(Input.acceleration.x, Input.acceleration.y);
        AddToAccelQuee(offset);
        return AccelOffset;
    }

	// Update is called once per frame
    void Update()
    {
        Vector2 offset;
#if UNITY_IOS||UNITY_ANDROID
        offset = accelMultiplier * calculateOffsetFromAcceleration();
#else
        offset = calculateOffsetFromMouse();
#endif
        offset.x = Mathf.Clamp(offset.x, -1.0f, 1.0f);
        offset.y = Mathf.Clamp(offset.y, -1.0f, 1.0f);

        parallaxer.SetNewOffset(2f * scrollCoef * offset);
    }
}
