using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ExpandCollider : MonoBehaviour
{
    public Vector3 colliderSizeMultiplier = new Vector3(1.5f, 1.5f, 1.5f);

    void Start()
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        boxCollider.size = new Vector3(
            boxCollider.size.x * colliderSizeMultiplier.x,
            boxCollider.size.y * colliderSizeMultiplier.y,
            boxCollider.size.z * colliderSizeMultiplier.z
        );
    }
}
