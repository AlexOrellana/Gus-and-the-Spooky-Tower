    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    private Rigidbody rb;

    public LayerMask groundLayers;

    public float jumpForce = 7;

    public CapsuleCollider col;

    [SerializeField]
    float moveSpeed = 4f;
    Vector3 forward, right;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }

    void Update()
    {

        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;

        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (direction.magnitude > 0.1f)
        {
            Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxisRaw("Horizontal");
            Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxisRaw("Vertical");

            Vector3 heading = Vector3.Normalize(rightMovement + upMovement);
            transform.forward = Vector3.Lerp(transform.forward, heading, 0.1f);
            transform.position += heading * moveSpeed * Time.deltaTime;
        }

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up*jumpForce, ForceMode.Impulse);
        }

    }

    private bool IsGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * .9f, groundLayers);
    }
}
