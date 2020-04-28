using UnityEngine;

public class Rotura : MonoBehaviour
{
    public Rigidbody rigibody;
    public ControladorEnemigo enemigo;
    public Renderer renderercuerpo;
    public GameObject prefab;
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

            //GameObject partes = new GameObject();
            //if (prefab != null)
            //{
            //    partes = Instantiate(prefab, transform.position, transform.rotation);
            //}

            //Rigidbody[] partesrigibody = partes.GetComponentsInChildren<Rigidbody>();

            //foreach (Rigidbody partesdelbody in partesrigibody)
            //{

            //    partesdelbody.interpolation = RigidbodyInterpolation.Interpolate;
            //    partesdelbody.AddExplosionForce(15, transform.position, 5);

            //}


            rigibody.AddExplosionForce(15, transform.position, 5);

            this.enabled = false;
            muerto = true;
            enemigo.Muerte();
        }
    }
}