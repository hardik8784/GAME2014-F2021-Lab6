using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    [Header("Movement")]
    public float HorizontalForce;
    public float VerticalForce;
    public bool isGrounded;
    public Transform GroundOrigin;
    public float GroundRadius;
    public LayerMask GroundLayerMask;

    private Rigidbody2D Rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        CheckIfGrounded();
    }

    private void Move()
    {
        if (isGrounded)
        {

            float DeltaTime = Time.deltaTime;

            // Keyboard Input
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            float Jump = Input.GetAxisRaw("Jump");

            // Check for Flip

            if (x != 0)
            {
                x = FlipAnimation(x);
            }

            // Touch Input
            Vector2 worldTouch = new Vector2();
            foreach (var touch in Input.touches)
            {
                worldTouch = Camera.main.ScreenToWorldPoint(touch.position);
            }


            float HorizontalMoveForce = x * HorizontalForce; //* DeltaTime;
            float JumpMoveForce = Jump * VerticalForce;      // * DeltaTime;

            float Mass = Rigidbody.mass * Rigidbody.gravityScale;

            Rigidbody.AddForce(new Vector2(HorizontalMoveForce, JumpMoveForce) * Mass);
            Rigidbody.velocity *= 0.99f;
        }
    }

    private void CheckIfGrounded()
    {
        RaycastHit2D Hit = Physics2D.CircleCast(GroundOrigin.position, GroundRadius, Vector2.down, GroundRadius, GroundLayerMask);

        isGrounded = (Hit) ? true : false;
    }


    private float FlipAnimation(float x)
    {
        x = (x > 0) ? 1 : -1;
        
        transform.localScale = new Vector3(x, 1.0f);
        return x;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(GroundOrigin.position ,GroundRadius);
    }
}


