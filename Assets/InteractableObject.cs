using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool isBeingHeld { get; private set; }

    public void PickUp(Transform newParent)
    {
        isBeingHeld = true;
        transform.SetParent(newParent);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    public void Drop()
    {
        isBeingHeld = false;
        transform.SetParent(null);
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
