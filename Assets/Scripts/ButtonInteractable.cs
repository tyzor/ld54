using System;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public enum ELEVATOR_BUTTON
{   
    Doors = 0,
    Lights = 1
}

public class ButtonInteractable : MonoBehaviour, IInteractable
{
    [SerializeField]
    private DoorManager doorsManager;

    [SerializeField]
    private ELEVATOR_BUTTON type;

    public SelectionHighlight highlight;

    public void Interact()
    {
        if(type == ELEVATOR_BUTTON.Doors)
        {
            doorsManager.CloseDoor();
        }
    }
}