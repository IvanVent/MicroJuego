using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public float thrustForce = 5f;
    public float rotationSpeed = 120f;

    public GameObject gun, bulletPrefab;

    private Rigidbody _rigid;
    private  float xBorderLimit = 9.5f;
    private float yBorderLimit = 5.5f;
    public static int SCORE = 0;
    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        var newPos = transform.position;
        if(newPos.x > xBorderLimit){
            newPos.x = -xBorderLimit+1;
        }else if(newPos.x < -xBorderLimit){
            newPos.x = xBorderLimit-1;
        }else if(newPos.y > yBorderLimit){
            newPos.y = -yBorderLimit+1;
        }else if(newPos.y < -yBorderLimit){
            newPos.y = yBorderLimit-1;
        }
        transform.position = newPos;
        float thrust = Input.GetAxis("Vertical") * Time.deltaTime;
        float rotation = Input.GetAxis("Horizontal") * Time.deltaTime;

        Vector3 thrustDirection = transform.right;

        _rigid.AddForce(thrustDirection * thrust * thrustForce);

        transform.Rotate(Vector3.forward, -rotation * rotationSpeed);
        if(!PauseMenu.isPaused){
        
            if(Input.GetKeyDown(KeyCode.Space)){
                //GameObject bullet = Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);
                GameObject bullet= BulletPool.Instance.RequestBullet();
                bullet.transform.position = gun.transform.position;

                Bullet balaScript = bullet.GetComponent<Bullet>();

                balaScript.targetVector = transform.right;
            }
        }
    }

    
   private void OnCollisionEnter(Collision collition) {
    if (collition.gameObject.CompareTag("Enemy"))
        {
            SCORE = 0;
            SceneManager.LoadScene("SampleScene");
        }else{
            Debug.Log("Colision con otra cosa");
        }
   }


  
}
