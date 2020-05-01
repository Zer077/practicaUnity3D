using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {



        GameController.muertescript.ModificarTexto("Moriste en el hospital al lado de tu mujer, con una sonrisa y mucho amor");


        GameController.imagen2.SetActive(true);



    }
}
