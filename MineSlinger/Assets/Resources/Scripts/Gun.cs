using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();    
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
