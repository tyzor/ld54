using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private LightManager lightManager;
    
    [SerializeField]
    private Elevator elevator;


    // Start is called before the first frame update
    void Start()
    {
        lightManager.AllLightsOn();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(elevator.floorNumber < 8)
        {
            //lightManager.AllLightsOff();
        }
    }
}
