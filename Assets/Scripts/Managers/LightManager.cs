using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class LightManager : MonoBehaviour
{
    [SerializeField]
    private Light MainLight;
    
    [SerializeField]
    private Light AmbientLight;
    [SerializeField]
    private float AmbientIntensity = .5f;

    [SerializeField]
    private Material CeilingLightsMat;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AllLightsOff()
    {
        MainLight.enabled = false;
        AmbientLight.intensity = 0f;
        CeilingLightsMat.DisableKeyword("_EMISSION");
    }

    public void AllLightsOn()
    {
        MainLight.enabled = true;
        AmbientLight.intensity = AmbientIntensity;
        CeilingLightsMat.EnableKeyword("_EMISSION");
    }
}
