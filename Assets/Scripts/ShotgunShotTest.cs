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
            for (int i = 0; i < 5; i++)
            {
                //Transform points = transform;
                //points.eulerAngles = new Vector3(0, 0, -30 + (i * 15) + transform.eulerAngles.z);

                
            }
        }
    }
}
