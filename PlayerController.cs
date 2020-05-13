using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;

    public HandController leftHand, rightHand;

    HandController leadHand;
    public float maxSpeed = 12.0f;

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
            rb.velocity = Vector3.forward;
        if (leadHand)
        {
            Debug.Log(leadHand.deltaPosition);
            transform.parent.position -= leadHand.deltaPosition;//velocity = v * -1f;
        }
    }
    public void SetLeadHand(HandController hand)
    {
        if(hand) Debug.Log("HandGrabbed: " + hand.name);
        leadHand = hand;
        rb.velocity = Vector3.zero;
    }
    public void RemoveLeadHand()
    {
        Debug.Log("Hand Released");
        Vector3 v = leadHand.deltaPosition / -Time.fixedDeltaTime;
        if (v.magnitude > maxSpeed)
            v = v.normalized * maxSpeed;
        rb.velocity = v;
        leadHand = null;
    }
}
