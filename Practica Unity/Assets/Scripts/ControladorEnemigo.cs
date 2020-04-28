using UnityEngine;

public class ControladorEnemigo : MonoBehaviour
{
    private Animator animacion;
    public bool muerto;
    public Transform posicionarma;
    public float vision;

    private void Start()
    {
        animacion = GetComponent<Animator>();

        if (posicionarma.GetComponentInChildren<ControladorArma>() != null)
        {
            posicionarma.GetComponentInChildren<ControladorArma>().suelto = false;
        }
    }

    private void Update()
    {
        //Si no está muerto continuamente te mira

        if (!muerto)
        {
            transform.LookAt(new Vector3(GameObject.FindGameObjectWithTag("MainCamera").transform.position.x, GameObject.FindGameObjectWithTag("MainCamera").transform.position.y - 1, GameObject.FindGameObjectWithTag("MainCamera").transform.position.z));
        }
    }

    /// <summary>
    /// Se cae al suelo como si estuviese muerto
    /// </summary>
    public void Muerte()
    {
        animacion.enabled = false;
        Rotura[] partes = GetComponentsInChildren<Rotura>();
        foreach (Rotura rotura in partes)
        {
            rotura.rigibody.isKinematic = false;
            rotura.rigibody.interpolation = RigidbodyInterpolation.Interpolate;
        }
        muerto = true;

        if (posicionarma.GetComponentInChildren<ControladorArma>() != null)
        {
            ControladorArma arma = posicionarma.GetComponentInChildren<ControladorArma>();
            arma.Soltar();
        }
    }

    /// <summary>
    /// Dispara siempre que no esté muerto
    /// </summary>
    public void Dispara()
    {
        float distancia = Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position);

        if (!muerto && posicionarma.GetComponentInChildren<ControladorArma>() != null && distancia < vision)
        {
            posicionarma.GetComponentInChildren<ControladorArma>().Disparar(GetComponentInChildren<ParticleSystem>().transform.position, transform.rotation, true, this.name);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, vision);
    }


}