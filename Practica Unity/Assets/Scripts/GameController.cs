using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController gamecontroller;
    public static Muerte muertescript;
    public GameObject canvas;


    public bool puedeDisparar = true;
    public bool accion;
    public GameObject bala;
    public Transform balaspawn;


    public ControladorArma arma;

    public Transform posiciondelarma;
    public LayerMask capa;

    public Image indicador;

    public GameObject particulas;

    public GameObject balaprefab;

    public GameObject imagen;
    public static GameObject imagen2;



    private void Awake()
    {
        imagen2 = imagen;
        muertescript = canvas.GetComponent<Muerte>();
        gamecontroller = this;
        if (posiciondelarma.GetComponentInChildren<ControladorArma>() != null)
        {
            arma = posiciondelarma.GetComponentInChildren<ControladorArma>();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        //Si pulsas el botón de disparar, disparas una bala
        if (puedeDisparar && Input.GetMouseButtonDown(0))
        {
            StopCoroutine(TiempoEspera(.03f));
            StartCoroutine(TiempoEspera(.03f));
            if (arma != null)
            {

                arma.Disparar(Camera.main.transform.position + (Camera.main.transform.forward * .5f) + (Camera.main.transform.up * -.02f), Camera.main.transform.rotation, false, null);
            }
        }
        //Si pulsas el otro botón tiras el arma
        if (Input.GetMouseButtonDown(1) && (!arma.recargando || arma == null))
        {
            StopCoroutine(TiempoEspera(.4f));
            StartCoroutine(TiempoEspera(.4f));

            if (arma != null)
            {
                arma.Lanzar();
                arma = null;
            }
        }
        //Si no tienes arma, pero tu vista está sobre un arma, y pulsas el botón de disparar, el alma volará hacia ti
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 10, capa) && Input.GetMouseButtonDown(0) && arma == null)
        {
            hit.transform.GetComponent<ControladorArma>().Recoger();
        }

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        float tiempo = (x != 0 || y != 0) ? 1f : .03f;
        float lerpTime = (x != 0 || y != 0) ? .05f : .5f;

        tiempo = accion ? 1 : tiempo;
        lerpTime = accion ? .1f : lerpTime;

        //Escala el tiempo segun tu movimiento, si te mueves  el tiempo se vuelve normal, si no, mas lento
        Time.timeScale = Mathf.Lerp(Time.timeScale, tiempo, lerpTime);
    }

    private IEnumerator TiempoEspera(float time)
    {
        accion = true;
        yield return new WaitForSecondsRealtime(.06f);
        accion = false;
    }

    //Animacion en el HUD de como se recarga el arma segun el indicador
    public void RecargarIndicador(float time)
    {
        indicador.transform.DORotate(new Vector3(0, 0, 90), time, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).OnComplete(() => indicador.transform.DOPunchScale(Vector3.one / 3, .2f, 10, 1).SetUpdate(true));
    }
}