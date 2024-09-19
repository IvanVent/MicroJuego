using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{

    public float speed = 10f;

    public float maxLifeTime = 3f;

    public Vector3 targetVector;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * targetVector * Time.deltaTime);    
    }
    private void OnEnable()
    {
        Invoke("DeactivateBullet", maxLifeTime);
    }

    private void OnDisable()
    {
        CancelInvoke("DeactivateBullet");
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Enemy"))
        {
            IncreaseScore();
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    private void IncreaseScore()
    {
        Player.SCORE++;
        Debug.Log("Score: " + Player.SCORE);
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        GameObject go = GameObject.FindGameObjectWithTag("UI");
        go.GetComponent<Text>().text = "Puntos: " + Player.SCORE;
    }
    private void DeactivateBullet()
    {
        gameObject.SetActive(false);
    }
}
