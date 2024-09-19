using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorPool : MonoBehaviour
{

    [SerializeField] private GameObject meteorPrefab;
    [SerializeField] private int poolSize = 10;
    [SerializeField] private List<GameObject> meteorList;

    private static MeteorPool instance;
    public static MeteorPool Instance{get {return instance;}}
    private void Awake() {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject meteor = Instantiate(meteorPrefab);
            meteor.SetActive(false);
            meteorList.Add(meteor);
            meteor.transform.parent = transform;
        }
    }

    public GameObject RequestMeteor()
    {
        for (int i = 0; i < meteorList.Count; i++)
        {
            if (!meteorList[i].activeSelf)
            {
                meteorList[i].SetActive(true);
                Rigidbody rb = meteorList[i].GetComponent<Rigidbody>();
                if(rb != null)
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
                return meteorList[i];
            }
        }
        return null;
    }
}
