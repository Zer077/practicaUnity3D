using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activar : MonoBehaviour
{

    public GameObject nino;
    public GameObject adolescente;
    bool one = false;
    public CharacterController character;


    private void OnTriggerEnter(Collider other)
    {

        if (!one && other.tag == "Player")
        {
            character.height = 1.2f;
            character.center = new Vector3(0, 0.5f, 0);
            adolescente.active = true;
            nino.active = false;
            one = true;

        }





    }
}
