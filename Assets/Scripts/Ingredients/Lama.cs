using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lama : MonoBehaviour
{
    public List<LamaBalls> balls;
    public float ballSpeed;
    public float ballDelay;

    private float timeSinceLastBall = 0;
    private int currentBall = 0;


    public void FixedUpdate()
    {
        if (timeSinceLastBall <= 0)
        {
            balls[currentBall].gameObject.SetActive(true);
            balls[currentBall].transform.position = transform.position;
            if (LevelManager.instance.currentPlayer.transform.position.x > transform.position.x)
                balls[currentBall].direction = new Vector3(ballSpeed * Time.deltaTime, 0);
            else
                balls[currentBall].direction = new Vector3(-ballSpeed * Time.deltaTime, 0);

            currentBall++;
            if (currentBall == balls.Count) currentBall = 0;
            timeSinceLastBall = ballDelay;
        }
        else
        {
            timeSinceLastBall -= Time.deltaTime;
        }



    }
}
