using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;

    public HandController leftHand, rightHand;

    HandController leadHand;
    public float maxSpeed = 12.0f;

    void FixedUpdate()
    {
        if (leadHand)
        {
            transform.parent.position -= leadHand.deltaPosition;
        }
    }
    public void SetLeadHand(HandController hand)
    {
        leadHand = hand;
        rb.velocity = Vector3.zero;
    }
    public void RemoveLeadHand()
    {
        Vector3 v = leadHand.deltaPosition / -Time.fixedDeltaTime;
        if (v.magnitude > maxSpeed)
            v = v.normalized * maxSpeed;
        rb.velocity = v;
        leadHand = null;
    }
}
