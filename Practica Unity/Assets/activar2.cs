using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activar2 : MonoBehaviour
{

    public GameObject adulto;
    public GameObject adolescente;
    bool one = false;
    public CharacterController character;


    private void OnTriggerEnter(Collider other)
    {
        if (!one && other.tag == "Player")
        {
            character.height = 1.5f;
            character.center = new Vector3(0, 0, 0);

            adulto.active = true;
            adolescente.active = false;

            one = true;
        }





    }
}
