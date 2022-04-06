using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        //If I get the collision with someone who have the component "AmmoCom" (who anybody have)
        if(other.gameObject.GetComponent<AmmoComponent>())
        {
            //They can press E to pick-up it
            if(Input.GetKeyDown(KeyCode.E))
            {
                if(other.gameObject.GetComponent<AmmoComponent>().Ammo<=200)
                {
                    Destroy(other.gameObject);
                }
            }
        }
    }
}
