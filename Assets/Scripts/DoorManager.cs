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

    // Start is called before the first frame update
    void Start()
    {
        m_DoorAnimator = _Doors.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO -- remove, used for testing
        if(Input.GetKey(KeyCode.E) && !isToggling)
        {
            StartCoroutine(ToggleDoors());
        }
    }

    private void OpenDoor()
    {
        m_DoorAnimator.Play("DoorsOpen");
        m_DoorOpen = true;
    }
    private void CloseDoor()
    {
        m_DoorAnimator.Play("DoorsClose");
        m_DoorOpen = false;
    }

    private IEnumerator ToggleDoors()
    {
        isToggling = true;
        Debug.Log("Toggling doors");

        m_DoorOpen = !m_DoorOpen;
        var animName = m_DoorOpen ? "DoorsOpen" : "DoorsClose";
        m_DoorAnimator.Play(animName);

        yield return new WaitForSeconds(1);
        isToggling = false;
    }
}