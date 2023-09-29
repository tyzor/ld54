using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 700f;

    void Update()
    {
        float moveInput = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float turnInput = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;

        transform.Translate(Vector3.forward * moveInput);
        transform.Rotate(Vector3.up * turnInput);
    }
}
