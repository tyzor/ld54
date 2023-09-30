using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Camera playerCamera;
    public float interactDistance = 2f;
    public LayerMask interactableLayer;
    public PuzzleManager puzzleManager;  // Reference to the PuzzleManager script

    private InteractableObject heldObject;  // Reference to the currently held object
    private float interactionTime = 0f;  // Variable to track interaction time

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

        if (Input.GetKey(KeyCode.E))
        {
            interactionTime += Time.deltaTime;
            if (interactionTime >= 2f)  // Assume 2 seconds is the required time to interact
            {
                DetectPuzzle(ray);
                interactionTime = 0f;  // Reset interaction time
            }
        }
        else
        {
            interactionTime = 0f;  // Reset interaction time
        }

        // Debug line to visualize the raycast
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * interactDistance, Color.red);
    }

    void DetectPuzzle(Ray ray)
    {
        // Call the DetectPuzzle method in the PuzzleManager script,
        // passing the ray as a parameter
        puzzleManager.DetectPuzzle(ray);
    }
}
