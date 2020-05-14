using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunShotTest : MonoBehaviour
{

    public Bullet Bullet;
    public List<Transform> points;
    private int angle = -45;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < 3; i++)
            {
                Bullet bullet = GameObject.Instantiate(Bullet, transform.position, transform.rotation) as Bullet;
                bullet.transform.Rotate(bullet.transform.rotation.x, bullet.transform.rotation.y, points[i].eulerAngles.z);
                Debug.Log(points[i].up);
                bullet.Rigidbody.velocity = points[i].up  * 75;
                angle += 45;
            }
            angle = -45;
        }
    }
}
