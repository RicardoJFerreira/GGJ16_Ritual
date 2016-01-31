using UnityEngine;
using System.Collections;

public class BallonScript : MonoBehaviour
{

    public float timeUntilChangeOfDirection;
    public float timeThanTakesToChangeDirection;

    float xPosition;

    public Animator animator;

    private bool animationStarted = false;
    private float timeThatTakesToLoad = 0.8f;
    private float timeAnimationStarted;

    public float ballonSpeed;
    // Use this for initialization
    void Start()
    {
        //timeUntilChangeOfDirection = 1.0f;
        //timeThanTakesToChangeDirection = 1.0f;
        Random.seed = (int) Time.time;
        animator = GameObject.Find("explosion").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -10.0f)
        {
            if (timeUntilChangeOfDirection <= 0.0f)
            {
                xPosition = transform.position.x + Random.Range(-700.0f, 700.0f);
                timeUntilChangeOfDirection = timeThanTakesToChangeDirection;
            }
            else
            {
                timeUntilChangeOfDirection -= Time.deltaTime;
            }


            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(xPosition, 500, transform.position.z), ballonSpeed);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, transform.position.z),
                ballonSpeed);

        }

        if (!animationStarted && transform.position.x == 0 && transform.position.y == 0)
        {
            animator.Play("explode_2");
            animationStarted = true;
            timeAnimationStarted = Time.time;
        }

        if (animationStarted && (Time.time >= (timeAnimationStarted + timeThatTakesToLoad)))
        {
            Application.LoadLevel("level_1");
        }
    }
}
