using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class SpriteAnimation : MonoBehaviour
{
    public Sprite defaultSprite;
    public Sprite ForwardSprite;
    public Sprite LeftSprite;
    public Sprite RightSprite;
    public Sprite BackSprite;
    public Sprite BackSideSprite;
    public Sprite FrontSideSprite;

    public Sprite[] frames;
    public bool loop = false;
    public float framesPerSecond = 5f;

    private float waitTime;
    private SpriteRenderer spriteRenderer;
    private int currentFrame;

    private GameObject player;

    public bool playing = false;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("MainCamera");

        waitTime = 1 / framesPerSecond; // set wait to match fps
        //Play();
        //Orientation();
    }

    public void Play()
    {
        StartCoroutine(PlayAnim());
    }

    private IEnumerator PlayAnim()
    {
        playing = true;
        currentFrame = 0;
        spriteRenderer.sprite = frames[currentFrame];
        currentFrame++;

        while (currentFrame < frames.Length)
        {
            yield return new WaitForSeconds(waitTime);
            spriteRenderer.sprite = frames[currentFrame++];
            //Debug.Log("frame: " + currentFrame);
        }

        if (loop)
        {
            Play();
            Orientation();
        }


        else
            playing = false;
    }

    public void Update()
    {
        Orientation();
    }

    private void Orientation()
    {
        Vector3 targetDirection = player.transform.position - transform.parent.position;
        Vector3 forward = transform.parent.forward;


        float deltaAngle = Vector3.SignedAngle(targetDirection, forward, Vector3.up);


        Debug.Log("Angle: " + deltaAngle);

        transform.LookAt(targetDirection);
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);


        if (deltaAngle >= -22.5f && deltaAngle <= 22.5f)
        {
            spriteRenderer.sprite = ForwardSprite;
            spriteRenderer.flipX = false;
            //transform.rotation = Quaternion.AngleAxis(0f, Vector3.up);
            Debug.Log("North!");
        }

        else if (deltaAngle > 22.5f && deltaAngle < 67.5f)
        {
            spriteRenderer.sprite = FrontSideSprite;
            spriteRenderer.flipX = true;
            //transform.rotation = Quaternion.AngleAxis(-45f, Vector3.up);
            Debug.Log("North-East!");
        }
        else if (deltaAngle >= 67.5f && deltaAngle <= 112.5f)
        {
            spriteRenderer.sprite = LeftSprite;
            spriteRenderer.flipX = true;
            //transform.rotation = Quaternion.AngleAxis(90f, Vector3.up);
            Debug.Log("East!");
        }
        else if (deltaAngle > 112.5f && deltaAngle < 157.5f)
        {
            spriteRenderer.sprite = BackSideSprite;
            spriteRenderer.flipX = true;
            //transform.rotation = Quaternion.AngleAxis(45f, Vector3.up);
            Debug.Log("South-East!");
        }

        else if (deltaAngle < -22.5f && deltaAngle > -67.5f)
        {
            spriteRenderer.sprite = FrontSideSprite;
            spriteRenderer.flipX = false;
            //transform.rotation = Quaternion.AngleAxis(45f, Vector3.up);
            Debug.Log("North-West!");
        }
        else if (deltaAngle <= -67.5f && deltaAngle >= -112.5f)
        {
            spriteRenderer.sprite = RightSprite;
            spriteRenderer.flipX = false;
            //transform.rotation = Quaternion.AngleAxis(90f, Vector3.up);
            Debug.Log("West!");
        }
        else if (deltaAngle < -112.5f && deltaAngle > -157.5f)
        {
            spriteRenderer.sprite = BackSideSprite;
            spriteRenderer.flipX = false;
            //transform.rotation = Quaternion.AngleAxis(-45f, Vector3.up);
            Debug.Log("South-West!");
        }

        else if (deltaAngle >= 157.5f || deltaAngle <= -157.5f)
        {
            spriteRenderer.sprite = BackSprite;
            spriteRenderer.flipX = false;
            //transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
            Debug.Log("South!");
        }
    }
}
