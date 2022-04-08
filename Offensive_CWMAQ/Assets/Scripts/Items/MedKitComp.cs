using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKitComp : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<HpManager>())
        {
            //They can press E to pick-up it
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (other.gameObject.GetComponent<HpManager>().Hp < 100)
                {
                    other.GetComponent<HpManager>().Hp = 100;
                    Destroy(gameObject);
                }
            }
        }
    }
}
