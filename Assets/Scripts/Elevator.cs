using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField]
    private DoorManager DoorManager;

    public int floorNumber = 30;
    [SerializeField]
    private TextMeshProUGUI floorIndicatorText;

    // LIGHTS
    [SerializeField]
    private Light mainLight;
    [SerializeField]
    private List<GameObject> ceilingLights;

    // Start is called before the first frame update
    void Start()
    {
        floorIndicatorText.text = floorNumber.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(!testing && floorNumber > 1)
            StartCoroutine(UpdateFloorNumber());
    }

    // TODO -- remove this after done testing
    private bool testing = false;
    IEnumerator UpdateFloorNumber()
    {
        testing = true;
        yield return new WaitForSeconds(1);
        floorNumber -= 1;
        floorIndicatorText.text = floorNumber.ToString();
        testing = false;
    }
}
