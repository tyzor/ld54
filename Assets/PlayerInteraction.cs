using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactDistance = 2f;
    public Vector3 heldObjectPosition = new Vector3(1.2f, 1.8f, -0.5f);
    private InteractableObject selectedObject;
    private InteractableObject heldObject;

    void Update()
    {
        // Handle object selection
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactDistance))
        {
            InteractableObject interactableObject = hit.collider.GetComponent<InteractableObject>();
            if (interactableObject != null && interactableObject != heldObject)
            {
                if (selectedObject != null)
                {
                    selectedObject.GetComponent<ObjectHighlight>().RemoveHighlight();
                }
                selectedObject = interactableObject;
                selectedObject.GetComponent<ObjectHighlight>().Highlight();
            }
        }
        else if (selectedObject != null)
        {
            selectedObject.GetComponent<ObjectHighlight>().RemoveHighlight();
            selectedObject = null;
        }

        // Handle object interaction
        if (Input.GetKeyDown(KeyCode.E) && selectedObject != null)
        {
            if (heldObject != null)
            {
                heldObject.Drop();
                heldObject = null;
            }
            else
            {
                selectedObject.PickUp(transform, heldObjectPosition);
                heldObject = selectedObject;
                selectedObject.GetComponent<ObjectHighlight>().RemoveHighlight();
                selectedObject = null;
            }
        }
    }
}
