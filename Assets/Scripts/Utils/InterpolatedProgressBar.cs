using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UIProgressBar))]
public class InterpolatedProgressBar : MonoBehaviour {

    float targetValue = 1f;
    UIProgressBar progressBar;

    [SerializeField]
    float increaseSpeed = 5f;

    [SerializeField]
    float decreaseSpeed = 5f;

    public float getValue()
    {
        return progressBar.value;
    }

    public void setValue(float value) {     
        targetValue = value;

        float speed = 0f;
        if (progressBar.value < targetValue)
        {
            speed = increaseSpeed;
        }
        else {
            speed = decreaseSpeed;
        }

        progressBar.value = Mathf.Lerp(progressBar.value, targetValue, Time.deltaTime * speed);
    }

    void Awake()
    {
        progressBar = GetComponent<UIProgressBar>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
