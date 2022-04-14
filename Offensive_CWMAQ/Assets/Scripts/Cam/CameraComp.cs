using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraComp : MonoBehaviour
{
    private float coef = 0;
    private float limitRotation = 20.0f;
    public bool Aiming = false;
    public float Xaxe;
    public float Yaxe;
    public float Sensitivity = 8.0f;
    public Transform Target;
    public Transform Player;
    public GameObject GameMaster;
    
    void Update()
    {

        if (Player.GetComponent<PauseComponent>().IsMenu || GameMaster.GetComponent<GameMaster>().IsPlayerDead || GameMaster.GetComponent<GameMaster>().IsPlayerWin)
            return;

        //Position of the mouse
        Yaxe += Input.GetAxis("JoystickrightX") * Sensitivity;
        Xaxe -= Input.GetAxis("JoystickrightY") * Sensitivity;

        //Position of the JoyStick
        Yaxe += Input.GetAxis("Mouse X") * Sensitivity;
        Xaxe -= Input.GetAxis("Mouse Y") * Sensitivity;      

        //Define the target witch will the camera turn around
        Vector3 TargetRotate = new Vector3(Xaxe, Yaxe);
        transform.eulerAngles = TargetRotate;

        //New Position of the camera * 12 for the distance between
        transform.position = Target.position - transform.forward * 12;

        //Limitate the rotation of the x axe
        Xaxe = Mathf.Clamp(Xaxe, -limitRotation, limitRotation*2);

        //If im aiming I'm zooming my fov
        if (Aiming == false)
        {
            coef -= 0.1f;
            if(coef>1)
            {
                coef = 1;
            }
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(60, 40, coef);
        }
        else
        {
            coef += 0.1f;
            if (coef < 0)
            {
                coef = 0;
            }
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(60, 40, coef);
        }

        Player.transform.rotation = Quaternion.Euler(0, gameObject.GetComponent<CameraComp>().Yaxe, 0);
    }
}
