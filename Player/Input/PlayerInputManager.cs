using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    private PlayerController _controller;
    private PlayerInteraction _interaction;

    public Vector2 Direction;
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

    private void Start()
    {
       _controller = GetComponent<PlayerController>();
       _interaction = GetComponent<PlayerInteraction>();
    }
    public void SetStatement(PlayerController.State state) => _controller.PlayerState = state;

    public void OnMove(InputValue val)
    {
        if (!(val.Get<Vector2>() == Vector2.zero))
        {
            SetStatement(PlayerController.State.Walking);
            Direction = val.Get<Vector2>();
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
    public void OnReset()
    {

        StaticMethods.Reset(transform, _interaction._camHolder, _interaction._checkPoint[_interaction._level], _interaction._camCheckPoint[_interaction._level]);
    }
    IEnumerator DashCooldown(float secs)
    {
        _canDash = false;
        yield return new WaitForSeconds(secs);
        _canDash = true;
    }
}
