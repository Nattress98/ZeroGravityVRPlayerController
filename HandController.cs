public class HandController : MonoBehaviour
{
    public XRNode m_ControllerNode;

    public InputDevice inputDevice;

    public PlayerController player;

    public Collider currentCollider;

    public Transform handMesh;

    bool grabbed = false;
    public bool wallGrabbed = false;
    public Vector3 deltaPosition;
    Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
        inputDevice = InputDevices.GetDeviceAtXRNode(m_ControllerNode);
    }

    void Update()
    {
        inputDevice.TryGetFeatureValue(CommonUsages.gripButton, out grabbed);

        if ((grabbed && (currentCollider || wallGrabbed)) != wallGrabbed)
        {
            wallGrabbed = grabbed && currentCollider;
            Debug.Log("Wall Grabbed: " + wallGrabbed);
            if (wallGrabbed)
                GrabWall();
            else
                ReleaseWall();
        }
    }
    protected void GrabWall()
    {
        player.SetLeadHand(this);
        handMesh.parent = null;

        //position on closest point of collider;
    }
    protected void ReleaseWall()
    {
        player.RemoveLeadHand();
        handMesh.parent = transform;
        handMesh.localPosition = Vector3.zero;  //consider handmesh class containting offset amounts, could also include animator etc.;
        handMesh.localEulerAngles = Vector3.zero;
    }
    private void FixedUpdate()
    {
        inputDevice.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 pos);
        deltaPosition = pos - lastPosition;
        lastPosition = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        currentCollider = other;
    }
    private void OnTriggerExit(Collider other)
    {
        if (currentCollider == other)
            currentCollider = null;
    }
}
