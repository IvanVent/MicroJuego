using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject miniMeteorPrefab;
    public float speed = 5f;
    public float angle = 30f;
    public float maxLifeTime = 3f;
    private Rigidbody _rigid;

    private void OnEnable()
    {
        Invoke("DeactivateMeteor", maxLifeTime);
    }

    private void OnDisable()
    {
        CancelInvoke("DeactivateMeteor");
    }
    private void DeactivateMeteor()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Bullet"))
        {
            //obtener el vector de la bala
            Vector3 bulletVector = other.gameObject.GetComponent<Bullet>().targetVector;
            //Calcular la direccion de la bala
            Vector3 direction1 = Quaternion.Euler(0, 0, angle) * -bulletVector;
            Vector3 direction2 = Quaternion.Euler(0, 0, -angle) * -bulletVector;

            //Crear los meteoros pequeños
            spawnMiniMeteor(direction1);
            spawnMiniMeteor(direction2);



        }
    }

    private void spawnMiniMeteor(Vector3 direction)
    {
        GameObject miniMeteorito = Instantiate(miniMeteorPrefab, transform.position, Quaternion.identity);

        // Asignar la velocidad y dirección al mini meteorito usando el Rigidbody
        Rigidbody rb = miniMeteorito.GetComponent<Rigidbody>();
        rb.velocity = direction * speed;
    }
}
