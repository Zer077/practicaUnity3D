using UnityEngine;
using UnityEngine.UI;

public class MovimientoBala : MonoBehaviour
{
    public float velocidad = 5f;
    public string enemigo;


    private void Update()
    {
        transform.position += transform.forward * velocidad * Time.deltaTime;

        if (GameController.imagen2.active == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {

                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

            }


        }
        else
        {
            Destroy(gameObject, 4f);

        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rotura rotura = other.gameObject.GetComponent<Rotura>();

            Instantiate(GameController.gamecontroller.particulas, transform.position, transform.rotation);

            rotura.Romper();
        }
        else if (other.gameObject.CompareTag("Player") && enemigo != null)
        {


            switch (enemigo)
            {

                case "red":
                    GameController.muertescript.ModificarTexto("Te han asesinado");
                    break;

                case "yellow":
                    GameController.muertescript.ModificarTexto("Has sufrido un accidente");

                    break;
                case "blue":
                    GameController.muertescript.ModificarTexto("Moriste triste y solo");

                    break;
                case "green":
                    GameController.muertescript.ModificarTexto("Tus deudas se pagan caras");

                    break;
                case "white":
                    GameController.muertescript.ModificarTexto("Muerto por sobredosis");

                    break;
                case "black":
                    GameController.muertescript.ModificarTexto("No soportaste la muerte");

                    break;
                case "grey":
                    GameController.muertescript.ModificarTexto("TUna enfermedad ha podido contigo");

                    break;



            }
            GameController.imagen2.active = true;





        }

        if (GameController.imagen2.active == false)
        {

            Destroy(gameObject);

        }
    }

}