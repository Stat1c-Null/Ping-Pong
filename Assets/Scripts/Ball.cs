using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    float speed;
    float radius;
    Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        direction = Vector2.one.normalized;//Direction is (1,1) normalized
        radius = transform.localScale.x / 2;//Half of width
    }

    // Update is called once per frame
    void Update()
    {
        //Move the ball
        transform.Translate(direction * speed * Time.deltaTime);

        //Bounce off walls
        if(transform.position.y < GameManager.bottomLeft.y + radius && direction.y < 0)
        {
            direction.y = -direction.y;
        }
        if (transform.position.y > GameManager.topRight.y - radius && direction.y > 0)
        {
            direction.y = -direction.y;
        }

        //Game Over
        if (transform.position.x < GameManager.bottomLeft.x + radius && direction.x < 0)
        {
            Debug.Log("Right Player Wins!");
        }

        if (transform.position.x > GameManager.topRight.x - radius && direction.x > 0)
        {
            Debug.Log("Left Player Wins!");
        }
    }
    //Collide with players
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            bool isRight = other.GetComponent<Player>() .isRight;
            
            //Collide with Right Player
            if (isRight == true && direction.x > 0)
            {
                direction.x = -direction.x;
            }
            //Collide with Left Player
            if (isRight == false && direction.x < 0)
            {
                direction.x = -direction.x;
            }
        }
    }
}
