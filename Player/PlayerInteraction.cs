using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public int _level;
    public Transform _camHolder;
    public Vector3[] _checkPoint;
    public Vector3[] _camCheckPoint;

    public static PlayerInteraction instance;
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

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.transform.tag)
        {
            case "Enemy":
                StaticMethods.Reset(transform, _camHolder, _checkPoint[_level], _camCheckPoint[_level]);
                break;

            case "Platform":
                ++_level;
                transform.position = _checkPoint[_level];
                _camHolder.position = _camCheckPoint[_level];
                StartCoroutine(WaitSeconds(collision, 2f));
            break;

        }
    }
    IEnumerator WaitSeconds(Collision collision, float secs)
    {
        yield return new WaitForSeconds(secs);
        collision.transform.GetComponent<Renderer>().enabled = true;
        collision.transform.tag = "Enemy";
    }
}
