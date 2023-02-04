using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float FlySpeed = 5;
    public float YawAmount = 120;
    public int score;
    public AudioSource Collectible;
    public Text ScoreText;
    private float Yaw;

    // Start is called before the first frame update
    void Start()
    {
        ScoreText.GetComponent<Text>();
        ScoreText.text = "" + score;
    }

    // Update is called once per frame
    void Update()
    {
        //move forward
        transform.position += transform.forward * FlySpeed * Time.deltaTime;

        //input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //yaw, pitch, roll
        Yaw += horizontalInput * YawAmount * Time.deltaTime;
        float pitch = Mathf.Lerp(0, 90, Mathf.Abs(verticalInput)) * Mathf.Sign(verticalInput);
        float roll = Mathf.Lerp(0, 20, Mathf.Abs(horizontalInput)) * -Mathf.Sign(horizontalInput);

        //rotation
        transform.localRotation = Quaternion.Euler(Vector3.up * Yaw + Vector3.right * pitch + Vector3.forward * roll);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "collectible")
        {
            Destroy(other.gameObject, 0.1f);
            score++;
            ScoreText.text = "" + score;
            Collectible.Play();

            if (score == 5)
            {
                Application.LoadLevel("level2");
            }
        }

        if (other.gameObject.tag == "danger")
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
