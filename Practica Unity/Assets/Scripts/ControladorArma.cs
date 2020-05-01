using DG.Tweening;
using System.Collections;
using UnityEngine;


public class ControladorArma : MonoBehaviour
{
    public bool suelto = true;

    public bool recargando = false;

    private Rigidbody rigibody;
    private Collider coll;

    public float tiempoderecarga = .3f;
    public AudioSource song;
    public AudioClip disparo;
    private void Start()
    {
        song = this.transform.GetComponentInParent<AudioSource>();

        rigibody = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();

        CambiarEstado();
    }


    //Cambia el estado del arma segun la situación
    private void CambiarEstado()
    {
        if (transform.parent == null)
        {
            rigibody.isKinematic = (GameController.gamecontroller.arma == this) ? true : false;
            rigibody.interpolation = (GameController.gamecontroller.arma == this) ? RigidbodyInterpolation.None : RigidbodyInterpolation.Interpolate;
            coll.isTrigger = (GameController.gamecontroller.arma == this);
        }
    }

    /// <summary>
    /// Disparar bala, dispara una bala en la direccion en la que se mira
    /// </summary>
    /// <param name="posicion"></param>
    /// <param name="rotacion"></param>
    /// <param name="enemigo"></param>
    public void Disparar(Vector3 posicion, Quaternion rotacion, bool enemigo, string nombre)
    {
        if (enemigo)
        {

            GameObject bala = Instantiate(GameController.gamecontroller.balaprefab, posicion, rotacion);
            bala.GetComponent<MovimientoBala>().enemigo = nombre;

            song.PlayOneShot(disparo);

            Debug.Log(bala.transform.position);
            if (GetComponentInChildren<ParticleSystem>() != null)
            {
                GetComponentInChildren<ParticleSystem>().Play();
            }



        }
        else if (!recargando)
        {
            song.PlayOneShot(disparo);
            GameObject bala = Instantiate(GameController.gamecontroller.balaprefab, posicion, rotacion);
            bala.GetComponent<MovimientoBala>().enemigo = null;

            Debug.Log(bala.transform.position);

            if (GetComponentInChildren<ParticleSystem>() != null)
            {
                GetComponentInChildren<ParticleSystem>().Play();
            }

            if (GameController.gamecontroller.arma == this)
            {
                StartCoroutine(Recargar());

                Camera.main.transform.DOComplete();
                Camera.main.transform.DOShakePosition(.2f, .01f, 10, 90, false, true).SetUpdate(true);

                transform.DOLocalMoveZ(-.1f, .05f).OnComplete(() => transform.DOLocalMoveZ(0, .2f));
            }
        }

    }

    /// <summary>
    /// Lanza el arma hacia delante
    /// </summary>
    public void Lanzar()
    {
        Sequence secuence = DOTween.Sequence();
        secuence.Append(transform.DOMove(transform.position - transform.forward, .01f)).SetUpdate(true);
        secuence.AppendCallback(() => transform.parent = null);
        secuence.AppendCallback(() => transform.position = Camera.main.transform.position + (Camera.main.transform.right * .10f));
        secuence.AppendCallback(() => CambiarEstado());
        secuence.AppendCallback(() => rigibody.AddForce(Camera.main.transform.forward * 10f, ForceMode.Impulse));
        secuence.AppendCallback(() => rigibody.AddTorque(transform.transform.right + transform.transform.up * 20f, ForceMode.Impulse));
    }

    /// <summary>
    /// Recoge el arma
    /// </summary>
    public void Recoger()
    {
        if (suelto)
        {
            GameController.gamecontroller.arma = this;
            CambiarEstado();

            transform.parent = GameController.gamecontroller.posiciondelarma;

            transform.DOLocalMove(Vector3.zero, .25f).SetEase(Ease.OutBack).SetUpdate(true);
            transform.DOLocalRotate(Vector3.zero, .25f).SetUpdate(true);
        }
    }
    /// <summary>
    /// Cuando el enemigo muere suelta el arma
    /// </summary>
    public void Soltar()
    {
        suelto = true;
        transform.parent = null;
        rigibody.isKinematic = false;
        rigibody.interpolation = RigidbodyInterpolation.Interpolate;
        coll.isTrigger = false;

        rigibody.AddForce((Camera.main.transform.position - transform.position) * 2, ForceMode.Impulse);
        rigibody.AddForce(Vector3.up * 1, ForceMode.Impulse);
    }
    /// <summary>
    /// Controla que el arma se caargue un tiempo
    /// </summary>
    /// <returns></returns>
    private IEnumerator Recargar()
    {
        if (GameController.gamecontroller.arma == this)
        {
            GameController.gamecontroller.RecargarIndicador(tiempoderecarga);
            recargando = true;
            yield return new WaitForSeconds(tiempoderecarga);
            recargando = false;
        }
    }

    /// <summary>
    /// Si choca con el enemigo lo mata
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && collision.relativeVelocity.magnitude < 15)
        {
            Rotura rotura = collision.gameObject.GetComponent<Rotura>();

            if (!rotura.enemigo.muerto)
            {
                Instantiate(GameController.gamecontroller.particulas, transform.position, transform.rotation);
            }

            rotura.Romper();
        }
    }
}