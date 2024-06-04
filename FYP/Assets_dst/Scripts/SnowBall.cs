using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SnowBall : MonoBehaviour
{
    public GameObject Projectile;
    public float Mindamage;
    public float Maxdamage;
    public float Projectileforce;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            GameObject spell = Instantiate(Projectile, transform.position, Quaternion.identity);
            Vector2 Mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 Mypos = transform.position;
            Vector2 direction = (Mousepos - Mypos).normalized;
            spell.GetComponent<Rigidbody2D>().AddForce(direction * Projectileforce);
            spell.GetComponent<ProjectileDmg>().damage = Random.Range(Mindamage, Maxdamage);
        }
        
    }
}
 