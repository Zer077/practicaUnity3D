using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activar3 : MonoBehaviour
{
    public ControladorPersonaje controller;
    bool one = false;


    private void OnTriggerEnter(Collider other)
    {
        if (!one && other.tag == "Player")
        {
            controller.velocidadConstante = 10;
            one = true;
        }





    }
}
