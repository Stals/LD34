using UnityEngine;

namespace Rendering
{
    [RequireComponent(typeof(UnityStandardAssets.ImageEffects.BloomOptimized))]
    class BloomModifier : MonoBehaviour
    {
        UnityStandardAssets.ImageEffects.BloomOptimized bloomer;

        [SerializeField]
        AnimationCurve curve;

        [SerializeField]
        float period = 1.0f;

        [SerializeField]
        float multiplier = 1.0f;

        void Start()
        {
            bloomer = GetComponent<UnityStandardAssets.ImageEffects.BloomOptimized>();
        }

        void Update()
        {
            float timePassed = GetTimeInPeriod();
            float intesity = multiplier * curve.Evaluate(timePassed / period);
            bloomer.intensity = intesity;

        }

        private float GetTimeInPeriod()
        {
            return Time.timeSinceLevelLoad - period * Mathf.Floor(Time.timeSinceLevelLoad / period);
        }
    }
}
