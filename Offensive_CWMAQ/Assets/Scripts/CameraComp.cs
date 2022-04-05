using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraComp : MonoBehaviour
{
    public float Xaxe;
    public float Yaxe;
    public float Sensitivity = 8.0f;
    public Transform Player;
    void Update()
    {
        Xaxe -= Input.GetAxis("Mouse Y") * Sensitivity;
        Yaxe += Input.GetAxis("Mouse X") * Sensitivity;

        Vector3 TargetRotate = new Vector3(Xaxe, Yaxe);
        transform.eulerAngles = TargetRotate;

        transform.position = Player.position - transform.forward * 6;
    }
}
