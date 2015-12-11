using UnityEngine;
using System.Collections;

//     

public class ParticleController : MonoBehaviour
{

    [SerializeField]
    string sortingLayerName = "Foreground";


    private ParticleSystem _ps;
    private void Awake()
    {
        _ps = GetComponent<ParticleSystem>();
        _ps.GetComponent<Renderer>().sortingLayerName = sortingLayerName;
    }

    private void OnEnable()
    {
        _ps.Simulate(Time.unscaledDeltaTime, false, true); //Need to call this one OnEnable with true for the restart. If not, if you disable and reenable your gameObject. The simulation won't work...maybe because I'm disabling the emission during the simulation and reenabling it at some point.
    }

    private void Update()
    {
        _ps.Simulate(Time.unscaledDeltaTime, false, false);
    }
}