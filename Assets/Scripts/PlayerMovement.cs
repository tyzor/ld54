using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController m_CharacterController;
    public float moveSpeed = 1f;

    private Transform cameraTransform;

    private Rigidbody rb;
    private Vector2 _currentInput;

    // Start is called before the first frame update
    void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _currentInput = GetMoveInput();
        
        Vector3 direction = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up);
        transform.forward = direction.normalized;

        //Debug.DrawRay(transform.position, transform.forward * 10f, Color.red);
    }

    // TODO -- replace with new input system
    Vector2 GetMoveInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void FixedUpdate()
    {
        var currentVelocity = rb.velocity;

        // Zero out our velocity if we have no input
        if(_currentInput.x == 0 && _currentInput.y == 0)
        {
            currentVelocity.x = 0f;
            currentVelocity.z = 0f;
            rb.velocity = currentVelocity;
            return;
        }

        Vector3 inputDir = transform.forward * _currentInput.y + transform.right * _currentInput.x;
        var newVelocity = inputDir * moveSpeed;
        rb.velocity = newVelocity;
        
    }
}