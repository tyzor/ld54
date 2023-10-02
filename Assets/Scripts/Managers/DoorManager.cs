using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{

    private bool m_DoorOpen = false;
    private bool isToggling = false;

    [SerializeField]
    private GameObject _Doors;

    private Animator m_DoorAnimator;

    public bool GetDoorStatus() { return m_DoorOpen; }

    // Start is called before the first frame update
    void Start()
    {
        m_DoorAnimator = _Doors.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO -- remove, used for testing
        /*
        if(Input.GetKey(KeyCode.F) && !isToggling)
        {
            StartCoroutine(ToggleDoors());
        }
        */
        
    }

    public void OpenDoor()
    {
        if(m_DoorOpen) return;
        m_DoorAnimator.Play("DoorsOpen");
        m_DoorOpen = true;
    }
    public void CloseDoor()
    {
        if(!m_DoorOpen) return;
        m_DoorAnimator.Play("DoorsClose");
        m_DoorOpen = false;
    }

    private IEnumerator ToggleDoors()
    {
        isToggling = true;

        m_DoorOpen = !m_DoorOpen;
        var animName = m_DoorOpen ? "DoorsOpen" : "DoorsClose";
        m_DoorAnimator.Play(animName);

        yield return new WaitForSeconds(1);
        isToggling = false;
    }
}