using UnityEngine;
using VR = UnityEngine.VR;
using System.Collections;

/// <summary>
/// Esta clase esta basada en el OVRDebugHeadController para emular las entradas del dispositivo HMD, pero
/// diseñado para teclado y mouse. Se puede usar de dos maneras, directamente en el objeto CameraRig o en un 
/// nodo padre. En el últmimo caso, funcionaría como una plataforma en que la cámara está conectada: 
/// cuando la plataforma se mueve o gira, la cámara se mueve o gira relativo al padre, pero aun así la cámara 
/// puede moverse de forma independiente
/// </summary>
/// 
public class OVRDebugHeadController2 : MonoBehaviour
{

    [SerializeField]
    public bool AllowPitchLook = false;
    [SerializeField]
    public bool AllowYawLook = true;
    [SerializeField]
    public bool InvertPitch = false;
    [SerializeField]
    public float PitchDegreesPerSec = 90.0f;
    [SerializeField]
    public float YawDegreesPerSec = 90.0f;
    [SerializeField]
    public bool AllowMovement = false;
    [SerializeField]
    public float ForwardSpeed = 2.0f;
    [SerializeField]
    public float StrafeSpeed = 2.0f;

    protected OVRCameraRig CameraRig = null;

    void Awake()
    {
        // locate the camera rig so we can use it to get the current camera transform each frame
        OVRCameraRig[] CameraRigs = gameObject.GetComponentsInChildren<OVRCameraRig>();

        if (CameraRigs.Length == 0)
            Debug.LogWarning("OVRCamParent: No OVRCameraRig attached.");
        else if (CameraRigs.Length > 1)
            Debug.LogWarning("OVRCamParent: More then 1 OVRCameraRig attached.");
        else
            CameraRig = CameraRigs[0];
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!VR.VRDevice.isPresent)
        {
			
            if (AllowMovement)
            {
                float fwdAxis = Input.GetAxis("Vertical");
                float strafeAxis = Input.GetAxis("Horizontal");

                Vector3 fwdMove = (CameraRig.centerEyeAnchor.rotation * Vector3.forward) * fwdAxis * Time.deltaTime * ForwardSpeed;
                Vector3 strafeMove = (CameraRig.centerEyeAnchor.rotation * Vector3.right) * strafeAxis * Time.deltaTime * StrafeSpeed;
                transform.position += fwdMove + strafeMove;
            }

            if (AllowYawLook || AllowPitchLook)
            {
                Quaternion r = transform.rotation;
                if (AllowYawLook)
                {
                    float yaw = Input.GetAxis("Mouse X");
                    float yawAmount = yaw * Time.deltaTime * YawDegreesPerSec;
                    Quaternion yawRot = Quaternion.AngleAxis(yawAmount, Vector3.up);
                    r = yawRot * r;
                }
                if (AllowPitchLook)
                {
                    float pitch = Input.GetAxis("Mouse Y");
                    if (Mathf.Abs(pitch) > 0.0001f)
                    {
                        if (InvertPitch)
                        {
                            pitch *= -1.0f;
                        }
                        float pitchAmount = pitch * Time.deltaTime * PitchDegreesPerSec;
                        Quaternion pitchRot = Quaternion.AngleAxis(pitchAmount, Vector3.left);
                        r = r * pitchRot;
                    }
                }

                transform.rotation = r;
            }
        }
    }
}
