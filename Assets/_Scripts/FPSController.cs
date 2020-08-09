using System.Threading.Tasks;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    public float MouseSensitivity = 100f;
    public float Gravity = -9.81f;
    public float JumpHeight = 3f;
    public float WalkSpeed = 6f;
    public float SprintSpeed = 12f;

    public Transform GroundCheck;
    public float GroundDistance = 0.4f;
    public LayerMask GroundMask;

    public Joystick joystickMove;
    public FixedTouchField TouchField;
    private Vector2 LookAxis;

    private Transform camera;
    private float xRotation;

    private CharacterController controller;

    private Vector3 velocity;
    private bool isGrounded;

    public GameObject ReticleUI;
    public GameObject MobileUI;
    private bool MobileControll;
	private bool onPlatform = false;


    // For puzzles
    public Vector3 additionalMovementSpeed = Vector3.zero;

    void Start()
    {
        if (GameManager.Instance)
            MobileControll = GameManager.Instance.Mobile;

        if (MobileControll)
        {
            MobileUI.SetActive(true);
            ReticleUI.SetActive(false);
        }
        else
        {
            MobileUI.SetActive(false);
            ReticleUI.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
        }

        camera = GetComponentInChildren<Camera>().transform;
        controller = GetComponent<CharacterController>();

    }

    private void OnEnable()
    {
        xRotation = 0;
    }

    void Update()
    {
        // Gravity

        isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(JumpHeight * -2f * Gravity);
        }

        velocity.y += Gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Rotation

        float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;


        if (MobileControll)
        {
            float rotateFactor = 5;
            LookAxis = TouchField.TouchDist;
            mouseX = LookAxis.x / rotateFactor;
            mouseY = LookAxis.y / rotateFactor;
        }

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        camera.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX);

        // Movement

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (MobileControll)
        {
            x = joystickMove.Horizontal;
            z = joystickMove.Vertical;
        }

        Vector3 move = transform.right * x + transform.forward * z;

        float moveSpeed = Input.GetKey(KeyCode.LeftShift) && z == 1 ? SprintSpeed : WalkSpeed;

        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    public void AssignQuestFormation(Transform refernce)
    {
        transform.position = refernce.position;
        
        transform.rotation = Quaternion.Euler(0, refernce.eulerAngles.y, 0);
        Camera.main.transform.localRotation = Quaternion.Euler(refernce.eulerAngles.x, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Platt") && !onPlatform)
        {
			onPlatform = true;
            transform.parent = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.name.Contains("Platt") && onPlatform)
        {
			onPlatform = false;
            transform.parent = null;           
        }
    }

}
