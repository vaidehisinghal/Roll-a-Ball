using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed=0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    public AudioSource backTrack;
    public AudioSource pickUp;
    public AudioSource winSound;

    // Start is called before the first frame update
    void Start()
    {
        winTextObject.SetActive(false);
        rb= GetComponent<Rigidbody>();
        count=0;
        SetCountText();
        backTrack.Play();
        
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;

    }

    void SetCountText()
    {
        countText.text = "Count:" + count.ToString();
        if(count>=10)
        {
            winTextObject.SetActive(true);
            backTrack.Stop();
            pickUp.Stop();
            winSound.Play();

        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX,0.0f,movementY);
        rb.AddForce(movement * speed);
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            backTrack.Pause();
            pickUp.Play();
            backTrack.UnPause();
            count=count+1;
            SetCountText();
        }
        
    }
}
