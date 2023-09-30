using UnityEngine;
using System.Collections;

public class PuzzleManager : MonoBehaviour
{
    public Camera playerCamera;
    public Transform puzzleTransform;  // Assign the puzzle object's transform here
    public Vector3 cameraFocusOffset;  // Offset from the puzzle's position to focus the camera
    public float cameraTransitionDuration = 1.0f;
    private Vector3 originalCameraPosition;
    private Quaternion originalCameraRotation;
    private bool isPuzzleActive = false;
    private float interactionTime = 0f;
    private const float requiredInteractionTime = 2f;

    void Update()
    {
        if (isPuzzleActive)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                DeactivatePuzzle();
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.E))
            {
                interactionTime += Time.deltaTime;
                if (interactionTime >= requiredInteractionTime)
                {
                    ActivatePuzzle();
                }
            }
            else
            {
                interactionTime = 0f;
            }
        }
    }

    void ActivatePuzzle()
    {
        isPuzzleActive = true;
        originalCameraPosition = playerCamera.transform.position;
        originalCameraRotation = playerCamera.transform.rotation;
        
        Vector3 targetPosition = puzzleTransform.position + cameraFocusOffset;
        StartCoroutine(CameraTransition(targetPosition, puzzleTransform.rotation));

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    IEnumerator CameraTransition(Vector3 targetPosition, Quaternion targetRotation)
    {
        float elapsedTime = 0f;
        while (elapsedTime < cameraTransitionDuration)
        {
            float t = elapsedTime / cameraTransitionDuration;
            playerCamera.transform.position = Vector3.Lerp(originalCameraPosition, targetPosition, t);
            playerCamera.transform.rotation = Quaternion.Slerp(originalCameraRotation, targetRotation, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        playerCamera.transform.position = targetPosition;
        playerCamera.transform.rotation = targetRotation;
    }

    void DeactivatePuzzle()
    {
        isPuzzleActive = false;
        StartCoroutine(CameraTransition(originalCameraPosition, originalCameraRotation));
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
