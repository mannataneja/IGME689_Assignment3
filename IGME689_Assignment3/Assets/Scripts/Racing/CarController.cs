using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    [SerializeField] float acceleration = 10f;
    [SerializeField] float turnSpeed = 50f;
    [SerializeField] float maxSpeed = 20f;
    [SerializeField] float brakeStrength = 20f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate()
    {
        float move = Input.GetAxis("Vertical");  
        float turn = Input.GetAxis("Horizontal"); 

        //drive forward/backward
        rb.AddForce(transform.forward * move * acceleration, ForceMode.Acceleration);

        //clamp speed
        Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (flatVelocity.magnitude > maxSpeed)
        {
            flatVelocity = flatVelocity.normalized * maxSpeed;
            rb.linearVelocity = new Vector3(flatVelocity.x, rb.linearVelocity.y, flatVelocity.z);
        }

        //turn only when moving
        if (Mathf.Abs(move) > 0.1f)
        {
            transform.Rotate(Vector3.up * turn * turnSpeed * Time.fixedDeltaTime);
        }

        //brake with spacebar
        if (Input.GetKey(KeyCode.Space))
        {
            Vector3 slowed = Vector3.Lerp(rb.linearVelocity, Vector3.zero, brakeStrength * Time.fixedDeltaTime);
            rb.linearVelocity = slowed;
        }
    }
}
