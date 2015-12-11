using UnityEngine;
using System.Collections;

public class SmoothTimeHandler : MonoBehaviour {
    
    static void SetTimeScale(float newScale)
    {
        //Debug.Log("setting timescale = " + newScale.ToString());
        if (newScale < 0f) newScale = 0f;
        Time.timeScale = newScale;
    }

    class CurveStopper
    {
        public bool active = true;
    }
    CurveStopper currentCurveControl = new CurveStopper();

    public void StartCurvePlaying(AnimationCurve curveToPlay, float playTime)
    {
        currentCurveControl.active = false;
        CurveStopper control = new CurveStopper();
        ParallelProcesses.RunForGivenTime(this, playTime, (float t) =>
        {
            if (control.active)
            {
                float scale = curveToPlay.Evaluate(t / playTime);
                SetTimeScale(scale);
            }
        });
        currentCurveControl = control;
    }

    public void SetInTime(float value, float time)
    {
        //Debug.Log("SetInTime");
        currentCurveControl.active = false;
        CurveStopper control = new CurveStopper();
        float currentValue = Time.timeScale;
        ParallelProcesses.RunForGivenTime(this, time, (float t) =>
        {
            if (control.active)
            {
                float scale = Mathf.Lerp(currentValue, value, t / time);
                SetTimeScale(scale);
            }
        });
        currentCurveControl = control;
    }
	
	public void SetImmidiate(float value)
    {
        //Debug.Log("SetImmidiate");
        currentCurveControl.active = false;
        SetTimeScale(value);
    }

    public void OnDestroy()
    {
        SetTimeScale(1.0f);
    }

}
