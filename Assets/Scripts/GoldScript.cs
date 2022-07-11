using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldScript : MonoBehaviour
{

    public int value;

    public float rotateSpeed;

    public GameObject pickupEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, rotateSpeed*Time.deltaTime, 0f);
    }

    public void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            FindObjectOfType<GameManager>().AddGold(value);

            Instantiate(pickupEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }
}
