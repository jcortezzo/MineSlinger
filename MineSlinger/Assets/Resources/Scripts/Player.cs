using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float slideForce;
    [SerializeField]
    private float slideCd;

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
    private float minSlidingSpeed = 1f;

    private Rigidbody2D rb;
    private Animator anim;

    void Awake()
    {
        CanSlide = true;
        lastDirectionWalked = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Slide();
        }
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
        // might not be necessary to have a cooldown if we judge
        // just off the player's speed... we'll see, maybe we can remove this
        for (float t = 0; t < slideCd; t += Time.deltaTime)
        {
            yield return new WaitForEndOfFrame();
        }
        while (rb.velocity.magnitude > minSlidingSpeed)
        {
            yield return new WaitForEndOfFrame();
        }
        anim.SetBool("IsSliding", false);
        CanSlide = true;
    }
}