using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool isBeingHeld { get; private set; }

    public void PickUp(Transform newParent, Vector3 relativePosition)
{
    isBeingHeld = true;
    transform.SetParent(newParent);
    transform.localPosition = relativePosition;
    transform.localRotation = Quaternion.identity;
    // Ensure there's a Rigidbody, and set it to kinematic
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
        transform.SetParent(null);
        GetComponent<Rigidbody>().isKinematic = false;
    }

    void Update()
    {
        if (isBeingHeld)
        {
            // Ensure the object faces the player while being held
            transform.forward = -transform.parent.forward;
        }
    }
}
