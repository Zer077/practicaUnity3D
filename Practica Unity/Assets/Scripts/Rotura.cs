using UnityEngine;

public class Rotura : MonoBehaviour
{
    public Rigidbody rigibody;
    public ControladorEnemigo enemigo;
    public Renderer renderercuerpo;

    public bool muerto;

    private void Start()
    {
        rigibody = GetComponent<Rigidbody>();
    }

    public void Romper()
    {
        if (!muerto)
        {
            if (renderercuerpo != null)
            {
                renderercuerpo.enabled = false;
            }

            rigibody.AddExplosionForce(15, transform.position, 5);

            this.enabled = false;
            muerto = true;
            enemigo.Muerte();
        }
    }
}