using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactDistance = 2f;
    private InteractableObject heldObject;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObject != null)
            {
                // Drop the currently held object
                heldObject.Drop();
                heldObject = null;
            }
            else
            {
                // Check for interactable object in front of the player
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, interactDistance))
                {
                    InteractableObject interactableObject = hit.collider.GetComponent<InteractableObject>();
                    if (interactableObject != null)
                    {
                        // Pick up the interactable object
                        interactableObject.PickUp(transform);
                        heldObject = interactableObject;
                    }
                }
            }
        }
    }
}
