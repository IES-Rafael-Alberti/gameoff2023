using UnityEngine;
using UnityEngine.InputSystem;

public class CameraTrackingScript : MonoBehaviour
{
    public static GameObject targetRoom;
    public static GameObject targetPlayer;
    private Vector3 startingOffset;

    private PlatformingControls controls;
    private float cameraZoom = 1;

    // Start is called before the first frame update

    private void Awake()
    {
        controls = new PlatformingControls();
        controls.Player.Peak.performed += ZoomOut;
        controls.Player.Peak.canceled += ZoomIn;
    }
    void Start()
    {
        startingOffset = transform.localPosition;
        transform.parent = null;
    }

    private void ZoomIn(InputAction.CallbackContext context)
    {
        cameraZoom = 1f;
    }

    private void ZoomOut(InputAction.CallbackContext context)
    {
        cameraZoom = 3f;
    }

    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }


    // Update is called once per frame
    void LateUpdate()
    {
        if (targetPlayer != null) {
            transform.position = targetRoom.transform.position + Vector3.back * 35f * cameraZoom + Vector3.up * 15f;
            transform.LookAt(targetPlayer.transform.position);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, 0);
        }
    }
}
