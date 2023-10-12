using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    [SerializeField] Rigidbody _playerRigidBody;
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
    private void Update() => transform.position = Vector3.Lerp(transform.position, transform.position + _playerRigidBody.velocity * 0.08f, Time.deltaTime * 10f);


}
