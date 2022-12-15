using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float slideForce;
    [SerializeField]
    private float slideCd;
    [SerializeField]
    private float shootSpeed;

    public bool CanSlide { get; private set; }

    private Vector2 Direction
    {
        get
        {
            if (this == null || !this.gameObject.activeSelf)
            {
                return Vector2.zero;
            }
            Vector2 vec2pos = this.transform.position;
            return (Utils.GetMousePosition() - vec2pos).normalized;
        }
    }
    private Vector2 lastDirectionWalked;
    [SerializeField]
    private float minSlidingSpeed = 2f;

    private Rigidbody2D rb;
    private Animator anim;
    private GunHolder Gun;

    void Awake()
    {
        CanSlide = true;
        lastDirectionWalked = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Gun = transform.GetChild(0).gameObject.GetComponent<GunHolder>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateGun();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Slide();
        }
        if (Input.GetMouseButtonDown(0))
        {
            var mousePos = CameraController.Camera.ScreenToWorldPoint(Input.mousePosition);
            Gun.Shoot(mousePos, shootSpeed);
        }
        Move();
    }

    public void RotateGun()
    {
        Gun.transform.right = -Direction;
        Gun.FixState(Direction);
    }

    public void Move()
    {
        if (!CanSlide) return;  // TODO: Change to isSliding
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 moveDir = new Vector2(horizontal, vertical).normalized;
        rb.velocity = moveDir * moveSpeed;
        anim.SetBool("IsWalking", rb.velocity.magnitude >= minSlidingSpeed);
    }

    public void Slide()
    {
        if (!CanSlide) return;
        rb.velocity = Vector2.zero;
        rb.AddForce(Direction * slideForce, ForceMode2D.Impulse);
        StartCoroutine(SlideCoroutine());
    }

    private IEnumerator SlideCoroutine()
    {
        CanSlide = false;
        anim.SetBool("IsSliding", true);
        anim.SetBool("IsWalking", false);
        // might not be necessary to have a cooldown if we judge
        // just off the player's speed... we'll see, maybe we can remove this
        //for (float t = 0; t < slideCd; t += Time.deltaTime)
        //{
        //    yield return new WaitForEndOfFrame();
        //}
        while (rb.velocity.magnitude > minSlidingSpeed)
        {
            yield return new WaitForEndOfFrame();
        }
        anim.SetBool("IsSliding", false);
        CanSlide = true;
    }
}
