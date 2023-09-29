using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float lookSpeed = 700f;

    void Update()
    {
        transform.position = player.position + offset;

        float turnInput = Input.GetAxis("Horizontal") * lookSpeed * Time.deltaTime;
        player.Rotate(Vector3.up * turnInput);
    }
}
