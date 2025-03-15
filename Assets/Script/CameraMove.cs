using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private bool canMove = true;
    public float speed = 5.0f;

    private void OnEnable()
    {
        if (PlayerEventManager.Instance != null)
        {
            PlayerEventManager.Instance.OnInteruptionChanged.AddListener(UpdateCanMoveState);
        }
    }

    private void OnDisable()
    {
        if (PlayerEventManager.Instance != null)
        {
            PlayerEventManager.Instance.OnInteruptionChanged.RemoveListener(UpdateCanMoveState);
        }
    }

    private void Update()
    {
        if (canMove)
        {
            transform.position += transform.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime;
            transform.position += transform.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        }
    }

    private void UpdateCanMoveState()
    {
        if (PlayerEventManager.Instance != null)
        {
            canMove = PlayerEventManager.Instance.CheckCanMove();
        }
    }
}
