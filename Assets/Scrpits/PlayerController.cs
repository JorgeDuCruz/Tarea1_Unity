using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;


public class PlayerController : MonoBehaviour
{
    // Rigidbody of the player.
    private Rigidbody rb; 
    public int count;
    private float time;
    private float bestTime;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI bestText;
    public GameObject winTextObject;

    // Movement along X and Y axes.
    private float movementX;
    private float movementY;

    // Speed at which the player moves.
    public float speed = 0; 
    
    private Vector3 posicionInicial;

    // Start is called before the first frame update.
    void Start()
    {
        // Get and store the Rigidbody component attached to the player.
        Debug.Log("Player");
        rb = GetComponent<Rigidbody>();
        count = 12;
        time = 0;
        bestTime = GameManager.bestTime;
        SetCountText();
        setBestTime();
        winTextObject.SetActive(false);

        posicionInicial = transform.position;
    }
 
    // This function is called when a move input is detected.
    void OnMove(InputValue movementValue)
    {
        // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Store the X and Y components of the movement.
        movementX = movementVector.x; 
        movementY = movementVector.y; 
    }
    void SetCountText() 
   {
       countText.text =  "Count: " + count.ToString();
       if (count <= 0)
       {
           winTextObject.SetActive(true);
           checkBestTime();
           Destroy(GameObject.FindGameObjectWithTag("Enemy"));
           GameManager.gameController.ActivarEstadoEspera();
       }
   }

    private void checkBestTime()
    {
        if (time < bestTime || bestTime == 0)
        {
            bestTime = time;
            setBestTime();
        }
    }

    private void setBestTime()
    {
        bestText.text = "Best : "+bestTime.ToString("F2");
        GameManager.bestTime = bestTime;
        
    }

    void setTimeTetxt()
    {
        timeText.text = "Time: "+time.ToString("F2");
    }

    // FixedUpdate is called once per fixed frame-rate frame.
    private void FixedUpdate() 
    {
        // Create a 3D movement vector using the X and Y inputs.
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        time += 0.02F;
        setTimeTetxt();
        if (Input.GetKeyDown(KeyCode.R))
        {
            print("Presionando");
            checkBestTime();
        }

        // Apply force to the Rigidbody to move the player.
        rb.AddForce(movement * speed); 
    }
    
    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("PickUp")) 
        {
            other.gameObject.SetActive(false);
            count -= 1;
            SetCountText();
        }

        if (other.gameObject.CompareTag("Portal"))
        {
            transform.position = posicionInicial;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
{
   if (collision.gameObject.CompareTag("Enemy"))
   {
       
       // Update the winText to display "You Lose!"
       winTextObject.gameObject.SetActive(true);
       winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
       // Destroy the current object
       GameManager.gameController.ActivarEstadoEspera();
       this.gameObject.SetActive(false);
   }
}
}