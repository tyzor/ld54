using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Camera playerCamera;
    public float interactDistance = 2f;
    public LayerMask interactableLayer;

    private InteractableObject heldObject;  // Reference to the currently held object

    void Update()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetKeyDown(KeyCode.E) && heldObject != null)
        {
            Debug.Log("Dropping object");
            heldObject.Drop();
            heldObject = null;
        }
        else if (Physics.Raycast(ray, out hit, interactDistance, interactableLayer))
        {
            Debug.Log("Raycast hit: " + hit.collider.name);
            InteractableObject interactableObject = hit.collider.GetComponent<InteractableObject>();
            if (interactableObject != null && Input.GetKeyDown(KeyCode.E) && heldObject == null)
            {
                Debug.Log("Picking up object");
                interactableObject.PickUp(playerCamera);
                heldObject = interactableObject;
            }
        }

        // Debug line to visualize the raycast
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * interactDistance, Color.red);
    }
}
