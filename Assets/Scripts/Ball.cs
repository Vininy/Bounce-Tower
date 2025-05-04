using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameObject splashPrefab;
    public Material[] ballMaterials;
    Rigidbody rb;
    public float bounceForce = 400f;
    AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        rb = GetComponent<Rigidbody>();
        int appearanceIndex = PlayerPrefs.GetInt("BallAppearance", 0);
        if (ballMaterials != null && ballMaterials.Length > appearanceIndex)
        {
            GetComponent<MeshRenderer>().material = ballMaterials[appearanceIndex];
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        rb.velocity = new Vector3(rb.velocity.x, bounceForce * Time.deltaTime, rb.velocity.z);
        audioManager.Play("Land");
        GameObject newsplit = Instantiate(splashPrefab, new Vector3(transform.position.x, other.transform.position.y + 0.186f, transform.position.z), transform.rotation);
        newsplit.transform.localScale = Vector3.one * Random.Range(0.8f, 1.2f);
        newsplit.transform.parent = other.transform;
        string materialName = other.transform.GetComponent<MeshRenderer>().material.name;
        if (materialName == "Seguro (Instance)")
        {
        }
        else if (materialName == "Inseguro (Instance)")
        {
            GameManager.gameOver = true;
            audioManager.Play("GameOver");
        }
        else if (materialName == "Chegada (Instance)" && !GameManager.levelWin)
        {
            GameManager.levelWin = true;
            audioManager.Play("LevelWin");
        }
    }
}
