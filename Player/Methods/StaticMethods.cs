using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticMethods
{
    public static float TorqueForce = 5f;

    public static void MakeMove(Transform theObject,Vector2 direction)
    {
        Vector3 vector3 = new Vector3(direction.x,0f,direction.y);
        TorqueForce = Mathf.Lerp(TorqueForce, TorqueForce + Time.deltaTime, Time.deltaTime * 10f);
        TorqueForce = Mathf.Clamp(TorqueForce, 5f, 8f);
        theObject.GetComponent<Rigidbody>().velocity = Vector3.ClampMagnitude(theObject.GetComponent<Rigidbody>().velocity,6f);
        theObject.GetComponent<Rigidbody>().AddTorque(vector3*TorqueForce);
    }
    public static void Jump(Rigidbody theObject)
    {
        theObject.AddForce(new Vector3(0f, 300f, 0f));
        PlayerInputManager.instance.SetStatement(PlayerController.State.Standing);  // resetting State
    }
    public static void Dash(Rigidbody theObject)
    {
        Vector2 direction = PlayerInputManager.instance.Direction;

        theObject.AddForce(new Vector3(-direction.y * 400f, 0f, 0f));
    }
    public static void Reset(Transform playerPos,Transform camPos,Vector3 playerTP,Vector3 camTP)
    {
        playerPos.position = new Vector3(playerTP.x, playerTP.y + 2f, playerTP.z);
        camPos.position = camTP;
    }
}
