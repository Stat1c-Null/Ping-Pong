using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed;
    float height;

    string input;
    public bool isRight;

    // Start is called before the first frame update
    void Start()
    {
        height = transform.localScale.y;
        speed = 5f;
    }

    public void Init(bool isRightPlayer)
    {
        isRight = isRightPlayer;

        Vector2 pos = Vector2.zero;

        if(isRightPlayer)
        {
            //Place player to the right
            pos = new Vector2 (GameManager.topRight.x, 0);
            pos -= Vector2.right * transform.localScale.x;//Move player a little bit to the left
            //Set input scheme
            input = "PlayerRight";
        } else
        {
            //Place player to the left
            pos = new Vector2(GameManager.bottomLeft.x, 0);
            pos += Vector2.right * transform.localScale.x;//Move player a little bit to the left
            //Set input scheme
            input = "PlayerLeft";
        }
        //Update player's position
        transform.position = pos;
        //Update the name of the object
        transform.name = input;
    }

    // Update is called once per frame
    void Update()
    {
        //Move players
        float move = Input.GetAxis(input) * Time.deltaTime * speed;//multiply by delta time to match frame update

        //Restrict Player Movement so they wont get outside of the room
        //Restrict player's bottom movement
        if (transform.position.y < GameManager.bottomLeft.y + height / 2 && move < 0)
        {
            move = 0;
        }
        //Restrict player's top movement
        if (transform.position.y > GameManager.topRight.y - height / 2 && move > 0)
        {
            move = 0;
        }

        transform.Translate(move * Vector2.up);
    }
}
