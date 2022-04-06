using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraComp : MonoBehaviour
{
    private float _coef = 0;
    public bool Aiming = false;
    private float _limitRotation = 20.0f;
    public float Xaxe;
    public float Yaxe;
    public float Sensitivity = 8.0f;
    public Transform Player;
    
    void Update()
    {
        //Position of the mouse
        Xaxe -= Input.GetAxis("Mouse Y") * Sensitivity;
        Yaxe += Input.GetAxis("Mouse X") * Sensitivity;

        //Define the target witch will the camera turn around
        Vector3 TargetRotate = new Vector3(Xaxe, Yaxe);
        transform.eulerAngles = TargetRotate;

        //New Position of the camera * 12 for the distance between
        transform.position = Player.position - transform.forward * 12;

        //Limitate the rotation of the x axe
        Xaxe = Mathf.Clamp(Xaxe, -_limitRotation, _limitRotation*2);

        //If im aiming I'm zooming my fov
        if (Aiming == false)
        {
            _coef -= 0.1f;
            if(_coef>1)
            {
                _coef = 1;
            }
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(60, 40, _coef);
        }
        else
        {
            _coef += 0.1f;
            if (_coef < 0)
            {
                _coef = 0;
            }
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(60, 40, _coef);
        }
    }
}
