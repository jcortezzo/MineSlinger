using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHolder : MonoBehaviour
{
    private SpriteRenderer sr;
    private Transform muzzle;

    [field:SerializeField]
    public GameObject BULLET_PREFAB { get; private set; }

    void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();    
    }
    // Start is called before the first frame update
    void Start()
    {
        muzzle = transform.GetChild(0).transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot(Vector3 mousePos, float speed)
    {
        Bullet b = Instantiate(BULLET_PREFAB, 
                               muzzle.position, 
                               Quaternion.identity, 
                               null).GetComponent<Bullet>();
        var mousePos2d = new Vector2(mousePos.x, mousePos.y);
        var muzzlePos2d = new Vector2(muzzle.position.x, muzzle.position.y);
        var mouseAngle = (mousePos2d - muzzlePos2d).normalized;
        b.Rigidbody.velocity = mouseAngle * speed;

        b.transform.right = mouseAngle;
    }

    // Ripped from Yargs Ahoy! legit no idea how this works lol
    public void FixState(Vector2 direction)
    {
        float weaponAngle = -Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        sr.gameObject.transform.rotation = Quaternion.AngleAxis(weaponAngle, -this.transform.forward);
        if (weaponAngle > 0)
        {
            sr.sortingOrder = sr.sortingOrder + 1;
        } 
        else
        {
            sr.sortingOrder = sr.sortingOrder - 1;
        }

        if ((weaponAngle > 0 && weaponAngle < 90) || (weaponAngle > -90 && weaponAngle < 0))
        {
            sr.flipY = false;
            sr.flipX = true;
        }
        else
        {
            sr.flipY = true;
            sr.flipX = true;
        }
    }
}
