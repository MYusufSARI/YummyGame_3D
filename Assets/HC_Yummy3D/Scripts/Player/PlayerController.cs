using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header(" Settings ")]
    [SerializeField] private float screenPositionFollowThershold;
    [SerializeField] private float moveSpeed;
    private Vector3 clickedScreenPosition;
    private bool canMove;



    private void Start()
    {
        PlayerTimer.onTimerOver += DisableMovement;

        GameManager.onGameStateChanged += GameStateChangedCallback;
    }


    private void OnDestroy()
    {
        PlayerTimer.onTimerOver -= DisableMovement;

        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }


    private void Update()
    {
        if(canMove)
        ManageControl();
    }


    private void ManageControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickedScreenPosition = Input.mousePosition;
        }

        else if (Input.GetMouseButton(0))
        {
            // Calculate the difference in Screen Position
            Vector3 difference = Input.mousePosition - clickedScreenPosition;

            Vector3 direction = difference.normalized;

            float maxScreenDistance = screenPositionFollowThershold * Screen.height;

            if (difference.magnitude > maxScreenDistance)
            {
                clickedScreenPosition = Input.mousePosition - direction * maxScreenDistance;
                difference = Input.mousePosition - clickedScreenPosition;
            }

            difference /= Screen.width;

            difference.z = difference.y;
            difference.y = 0;

            Vector3 targetPosition = transform.position + difference * moveSpeed * Time.deltaTime;

            transform.position = targetPosition;
        }
    }


    private void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.GAME:
                EnableMovement();
                break;
        }
    }


    private void EnableMovement()
    {
        canMove = true;
    }


    private void DisableMovement()
    {
        canMove = false;
    }
}
