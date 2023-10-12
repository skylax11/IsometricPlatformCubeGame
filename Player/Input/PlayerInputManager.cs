using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    private PlayerController _controller;
    public Vector2 direction;
    private bool _canDash = true;
    public static PlayerInputManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            return;
        }
    }

    private void Start() => _controller = GetComponent<PlayerController>();
    public void SetStatement(PlayerController.State state) => _controller.PlayerState = state;

    public void OnMove(InputValue val)
    {
        if (!(val.Get<Vector2>() == Vector2.zero))
        {
            SetStatement(PlayerController.State.Walking);
            direction = val.Get<Vector2>();
        }
        else
        {
            SetStatement(PlayerController.State.Standing);
        }
    }
    public void OnJump(InputValue val)
    {
        if (!(val.Get<Vector2>() == Vector2.zero))
        {
            SetStatement(PlayerController.State.Standing);
        }
        else
        {
            SetStatement(PlayerController.State.Jumping);
        }
    }
    public void OnDash()
    {
        if(_canDash) StaticMethods.Dash(GetComponent<Rigidbody>());
        StartCoroutine("DashCooldown",3f);
    }
    IEnumerator DashCooldown(float secs)
    {
        _canDash = false;
        yield return new WaitForSeconds(secs);
        _canDash = true;
    }
}
