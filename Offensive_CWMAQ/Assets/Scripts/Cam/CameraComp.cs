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
    public Transform Target;
    public Transform Player;
    public GameObject GameMaster;
    
    void Update()
    {

        if (Player.GetComponent<Movement>().IsMenu || GameMaster.GetComponent<GameMaster>().IsPlayerDead || GameMaster.GetComponent<GameMaster>().IsPlayerWin)
            return;

        //Get Joystick Names
        string[] temp = Input.GetJoystickNames();

        //Check whether array contains anything

        //Iterate over every element
        //Check if the string is empty or not

        //Not empty, controller temp[i] is connected
        //Position of the mouse
        Yaxe += Input.GetAxis("JoystickrightX") * Sensitivity;
        Xaxe -= Input.GetAxis("JoystickrightY") * Sensitivity;


        //If it is empty, controller i is disconnected
        //where i indicates the controller number
        Yaxe += Input.GetAxis("Mouse X") * Sensitivity;
        Xaxe -= Input.GetAxis("Mouse Y") * Sensitivity;
        
        

        //Define the target witch will the camera turn around
        Vector3 TargetRotate = new Vector3(Xaxe, Yaxe);
        transform.eulerAngles = TargetRotate;

        //New Position of the camera * 12 for the distance between
        transform.position = Target.position - transform.forward * 12;

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

        Player.transform.rotation = Quaternion.Euler(0, gameObject.GetComponent<CameraComp>().Yaxe, 0);
    }
}
