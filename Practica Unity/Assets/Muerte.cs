using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Muerte : MonoBehaviour
{
    public Text texto;

    public void ModificarTexto(string textomuerte)
    {
        this.texto.text = textomuerte;


    }
}
