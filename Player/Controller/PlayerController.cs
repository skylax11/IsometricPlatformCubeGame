using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public State PlayerState;
    public GroundState groundState;

    [SerializeField] Transform GroundChecker;
    private RaycastHit _hit;
    public enum State
    {
        Standing,
        Walking,
        Jumping,
    }
    public enum GroundState
    {
        OnAir,
        OnGround
    }
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
    void Update()
    {
        GroundCheck();

        if (PlayerState == State.Standing)
        {
            StaticMethods.TorqueForce = 5f;
            return;
        }
        else if (PlayerState == State.Walking && groundState == GroundState.OnGround)
        {
            Vector2 direction = PlayerInputManager.instance.Direction;
            StaticMethods.MakeMove(gameObject.transform, direction);
        }
        else if ((PlayerState == State.Jumping) && groundState == GroundState.OnGround)
        {
            StaticMethods.Jump(GetComponent<Rigidbody>());
        }
    }
    public void GroundCheck()
    {
        if (Physics.Raycast(GroundChecker.transform.position, new Vector3(0, -1f, 0), out _hit, 1f))
        {
            if (!_hit.transform.CompareTag("Player"))
            {
                groundState = GroundState.OnGround;
            }
        }
        else
        {
            groundState = GroundState.OnAir;
        }
    }
}