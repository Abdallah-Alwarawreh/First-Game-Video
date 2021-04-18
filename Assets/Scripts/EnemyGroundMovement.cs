using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroundMovement : MonoBehaviour
{
    public float speed;
    public float Distance;
    private bool MovingRight = true;
    public Transform GroundDetection;
    public LayerMask groundLayer;
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        RaycastHit2D Groundinfo = Physics2D.Raycast(GroundDetection.position, Vector2.down, Distance,groundLayer);
        if(Groundinfo.collider == false)
        {
            if (MovingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                MovingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                MovingRight = true;
            }
        }

    }

}
