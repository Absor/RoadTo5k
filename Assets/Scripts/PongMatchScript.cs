using UnityEngine;
using System.Collections;

public class PongMatchScript : MonoBehaviour {

    public RectTransform area;
    public RectTransform ball;
    public RectTransform paddle1;
    public RectTransform paddle2;

    private float fieldWidth, fieldHeight;
    private float velocityX, velocityY;
    private float ballRadius;

    void Start()
    {
        fieldWidth = area.rect.width;
        fieldHeight = area.rect.height;
        velocityX = 2;
        
        velocityY = 2;
        ballRadius = ball.rect.height / 2;
    }

    void FixedUpdate()
    {
        ball.localPosition += new Vector3(velocityX, velocityY, 0);
        Debug.Log(ball.localPosition);
        Debug.Log(ball.localPosition + (Vector3.up * ballRadius));
        if (velocityY > 0)
        {
            if (!area.rect.Contains(ball.localPosition + new Vector3(0, ballRadius, 0)))
            {
                velocityY = -velocityY;
            }
        }
        else
        {
            if (!area.rect.Contains(ball.localPosition + new Vector3(0, -ballRadius, 0)))
            {
                velocityY = -velocityY;
            }
        }

        Rect ball2Rect = new Rect(ball.localPosition.x - ballRadius, ball.localPosition.y - ballRadius, ballRadius * 2, ballRadius * 2);
        Rect paddle1Rect = new Rect(paddle1.localPosition.x - 5, paddle1.localPosition.y - 25, 10, 50);
        Rect paddle2Rect = new Rect(paddle2.localPosition.x - 5, paddle2.localPosition.y - 25, 10, 50);
        if (velocityX > 0 && ball2Rect.Overlaps(paddle2Rect))
        {
            velocityX = -velocityX;
        }
        else if (velocityX < 0 && ball2Rect.Overlaps(paddle1Rect))
        {
            velocityX = -velocityX;
        }
    }
}
