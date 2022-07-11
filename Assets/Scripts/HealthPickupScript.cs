using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickupScript : MonoBehaviour
{
    
    public float idleEffectTime; 
    private float idleEffectTimer;
    public GameObject idleEffect;

    // Start is called before the first frame update
    void Start()
    {
       idleEffectTimer = 0;

    }

    // Update is called once per frame
    void Update()
    {
        idleEffectTimer = idleEffectTimer - Time.deltaTime;
        if(idleEffectTimer <= 0){
            Instantiate(idleEffect, transform.position, transform.rotation);
            idleEffectTimer = idleEffectTime;
        }
    }

    public void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            FindObjectOfType<HealthManager>().Heal(1);
            Destroy(gameObject);
        }
    }
}
