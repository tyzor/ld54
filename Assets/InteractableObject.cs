using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool isBeingHeld { get; private set; }
    public float heldDistance = 0f;  // Distance from the player camera
    public Vector3 rotationOffset;  // Rotation offset to show the desired side
    public Vector3 positionOffset = Vector3.zero;  // Position offset to show the desired position

    private Transform originalParent;  // Store the original parent of the object

    public void PickUp(Camera playerCamera)
    {
        isBeingHeld = true;
        originalParent = transform.parent;  // Store the original parent
        transform.SetParent(playerCamera.transform);  // Set the camera as the new parent
        SetHeldPosition();
        SetHeldRotation();
        Rigidbody rb = GetComponent<Rigidbody>();
        if (!rb)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.isKinematic = true;
    }

    public void Drop()
    {
        isBeingHeld = false;

        // Temporarily store the current position and rotation
        Vector3 currentPosition = transform.position;
        Quaternion currentRotation = transform.rotation;

        // Unparent the object
        transform.SetParent(originalParent, false);

        // Restore the position and rotation
        transform.position = currentPosition;
        transform.rotation = currentRotation;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
    }

    void Update()
    {
        if (isBeingHeld)
        {
            SetHeldPosition();
            SetHeldRotation();
        }
    }

    void SetHeldPosition()
    {
        Vector3 targetPosition = transform.parent.position + transform.parent.forward * heldDistance + positionOffset;
        transform.position = targetPosition;
    }

    void SetHeldRotation()
    {
        Quaternion targetRotation = transform.parent.rotation * Quaternion.Euler(rotationOffset);
        transform.rotation = targetRotation;
    }
}
