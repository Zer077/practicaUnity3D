using UnityEngine;

#if UNITY_EDITOR
#endif

public class ControladorPersonaje : MonoBehaviour
{
    public static ControladorPersonaje Instance { get; protected set; }

    public Camera MainCamera;

    public Transform posicionDeCamara;
    public Transform posicionDeArma;

    public float sensibilidadRaton = 5;
    public float velocidadConstante = 5.0f;

    private float velocidadVertical = 0.0f;
    private float anguloVertical;
    private float anguloHorizontal;
    public float velocidadActual = 0.0f;

    private CharacterController controlador;

    private void Start()
    {
        Instance = this;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        MainCamera.transform.SetParent(posicionDeCamara, false);
        MainCamera.transform.localPosition = Vector3.zero;
        MainCamera.transform.localRotation = Quaternion.identity;
        controlador = GetComponent<CharacterController>();

        anguloVertical = 0.0f;
        anguloHorizontal = transform.localEulerAngles.y;
    }

    /// <summary>
    /// Permite el movimiento del personaje
    /// </summary>
    private void Update()
    {
        velocidadActual = 0;
        Vector3 movimiento = Vector3.zero;

        movimiento = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (movimiento.sqrMagnitude > 1.0f)
        {
            movimiento.Normalize();
        }

        movimiento *= (velocidadConstante * Time.deltaTime);

        movimiento = transform.TransformDirection(movimiento);
        controlador.Move(movimiento);

        anguloHorizontal += Input.GetAxis("Mouse X") * sensibilidadRaton;

        anguloHorizontal += anguloHorizontal > 360 ? -360f : 360f;

        Vector3 anguloActual = transform.localEulerAngles;
        anguloActual.y = anguloHorizontal;
        transform.localEulerAngles = anguloActual;

        float giro = -Input.GetAxis("Mouse Y");
        giro *= sensibilidadRaton;
        anguloVertical = Mathf.Clamp(giro + anguloVertical, -89.0f, 89.0f);
        anguloActual = posicionDeCamara.transform.localEulerAngles;
        anguloActual.x = anguloVertical;
        posicionDeCamara.transform.localEulerAngles = anguloActual;

        velocidadActual = movimiento.magnitude / (velocidadConstante * Time.deltaTime);

        gravedad();
    }

    //Establece una gravedad continua hacia abajo
    private void gravedad()
    {
        velocidadVertical = velocidadVertical - 10.0f * Time.deltaTime;
        if (velocidadVertical < -10.0f)
        {
            velocidadVertical = -10.0f;
        }

        var verticalMove = new Vector3(0, velocidadVertical * Time.deltaTime, 0);
        var flag = controlador.Move(verticalMove);
        if ((flag & CollisionFlags.Below) != 0)
        {
            velocidadVertical = 0;
        }
    }
}