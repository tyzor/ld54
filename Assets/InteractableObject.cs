using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool isBeingHeld { get; private set; }
    public float heldDistance = 0f;  // Distance from the player camera
    public Vector3 rotationOffset;  // Rotation offset to show the desired side
    public Vector3 positionOffset = Vector3.zero;  // Position offset to show the desired position

    public Material normalMaterial;
    public Material highlightedMaterial;
    public float highlightDistance = 2f;

    private Renderer objectRenderer;
    private Transform playerTransform;
    private Transform originalParent;  // Store the original parent of the object

    void Start()
    {
        objectRenderer = GetRenderer();
        playerTransform = Camera.main.transform;  // Assumes the player camera is the main camera
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer <= highlightDistance)
        {
            HighlightObject();
        }
        else
        {
            UnhighlightObject();
        }

        if (isBeingHeld)
        {
            SetHeldPosition();
            SetHeldRotation();
        }
    }

    private Renderer GetRenderer()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer == null)
        {
            renderer = GetComponentInChildren<Renderer>();
        }
        return renderer;
    }

    public void PickUp(Camera playerCamera)
    {
        isBeingHeld = true;
        originalParent = transform.parent;
        transform.SetParent(playerCamera.transform);
        transform.localPosition = new Vector3(1.2f, -0.5f, 1.0f);

        Rigidbody rb = GetComponent<Rigidbody>();
        if (!rb)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.isKinematic = true;
        transform.localRotation = Quaternion.Euler(rotationOffset);
    }

    public void Drop()
    {
        isBeingHeld = false;
        Vector3 currentPosition = transform.position;
        Quaternion currentRotation = transform.rotation;
        transform.SetParent(originalParent, false);
        transform.position = currentPosition;
        transform.rotation = currentRotation;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
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

    void HighlightObject()
    {
        objectRenderer.material = highlightedMaterial;
    }

    void UnhighlightObject()
    {
        objectRenderer.material = normalMaterial;
    }
}
