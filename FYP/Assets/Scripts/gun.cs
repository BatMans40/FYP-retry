using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]  Bullet bulletprefab;
    [SerializeField]  Transform bulletSpawnPos;
    private Camera cam;

    private Vector2 MousePos
    {
        get
        {
            Vector2 Pos = cam.ScreenToWorldPoint(Input.mousePosition);
            return Pos;
        }
    }

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 direction = MousePos - (Vector2)transform.position;
            Bullet bullet = Instantiate(bulletprefab, bulletSpawnPos.position, Quaternion.identity);
            bullet.Shoot(direction.normalized);
        }
    }
}