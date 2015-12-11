using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteSmoothMovement : MonoBehaviour {

    public float speed = 0.5f;
    float realSpeed;
    
    float sqrRealSpeed;
    Vector2 nowOffset;
    Vector2 destination;

    Vector2 startPos;

    UI2DSprite sprite;

    public void SetNewOffset(Vector2 newOffset)
    {
        destination = 1.0f / transform.lossyScale.magnitude * newOffset;
    }

	// Use this for initialization
	void Start () {
        startPos = transform.localPosition;
        realSpeed = 1.0f / transform.lossyScale.magnitude * speed;
        sqrRealSpeed = realSpeed * realSpeed;
	}
	
	// Update is called once per frame
	void Update () {

        Vector2 diff = destination - nowOffset;
        if (diff.sqrMagnitude > sqrRealSpeed)
        {
            nowOffset += speed * diff.normalized;
        }
        else
        {
            nowOffset += speed * diff;
        }

        transform.localPosition = startPos + nowOffset;
	
	}
}
