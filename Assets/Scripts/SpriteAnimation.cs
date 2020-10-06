using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class SpriteAnimation : MonoBehaviour
{
    public Sprite defaultSprite;

    public Sprite[] frames;
    public bool loop = false;
    public float framesPerSecond = 5f;

    private float waitTime;
    private SpriteRenderer spriteRenderer;
    private int currentFrame;

    private GameObject player;
    private Vector2 playerVector;

    

    public bool playing = false;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("MainCamera");


        waitTime = 1 / framesPerSecond; // set wait to match fps
        Play();
        Orientation();
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

    private void Orientation()
    {
        Vector2 direction = new Vector2(transform.forward.x, transform.forward.z);

        playerVector = new Vector2(player.transform.position.x, player.transform.position.z);

        float deltaAngle = Vector2.Angle(direction, playerVector);
        Debug.Log("Angle: " + deltaAngle);

        if (deltaAngle < 45f)
        {
            Debug.Log("Forward");
        }

       
    }
}
