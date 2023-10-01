using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    enum PlayerState {
        Move,
        Interacting
    }
    private PlayerState state;

    [SerializeField]
    private float PuzzleRange = 1f;

    private PlayerMovement _playerMovement;

    [SerializeField]
    private CinemachineVirtualCamera PuzzleCamera;
    [SerializeField]
    private CinemachineVirtualCamera PlayerCamera;

    // Start is called before the first frame update
    void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(state == PlayerState.Move)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2f,Screen.height/2f,0));
            RaycastHit hit;

            int layerMask = 1 << 6;
            Debug.DrawRay(Camera.main.transform.position,ray.direction * PuzzleRange, Color.yellow);
            if(Physics.Raycast(ray, out hit, PuzzleRange, layerMask))
            {
                Puzzle hitPuzzle = hit.transform.gameObject.GetComponent<Puzzle>();
                if(Input.GetKeyDown(KeyCode.E))
                    EngagePuzzle(hitPuzzle);                
            }
        }

        if(state == PlayerState.Interacting)
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                DisengagePuzzle();
            }
        }
    }


    void EngagePuzzle(Puzzle puzzle)
    {
        state = PlayerState.Interacting;

        _playerMovement.enabled = false;

        PlayerCamera.gameObject.SetActive(false);
        PuzzleCamera.gameObject.SetActive(true);

        PuzzleCamera.Follow = puzzle.CameraLockPoint;
        PuzzleCamera.LookAt = puzzle.transform;

        Cursor.lockState = CursorLockMode.Confined;
                
    }

    void DisengagePuzzle()
    {
        state = PlayerState.Move;

        _playerMovement.enabled = true;

        PlayerCamera.gameObject.SetActive(true);
        PuzzleCamera.gameObject.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
    }


}
