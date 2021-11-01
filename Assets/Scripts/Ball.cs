using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //[SerializeField]
    public float speed;
    float radius;
    Vector2 direction;
    int leftScore = 0;
    int rightScore = 0;

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
            rightScore += 1;
            Debug.Log("Right Player Hits the Goal! Player Right: " + rightScore);
            transform.position = new Vector2(0, 0);
        }

        if (transform.position.x > GameManager.topRight.x - radius && direction.x > 0)
        {
            leftScore += 1;
            Debug.Log("Left Player Hits the Goal! Player Left: " + leftScore);
            transform.position = new Vector2(0, 0);
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
