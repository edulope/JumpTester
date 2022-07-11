using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    private bool isConquered;

    public Material conqueredMaterial;

    // Start is called before the first frame update
    void Start()
    {
        isConquered = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other) {
        if(other.tag == "Player" && !isConquered){
            isConquered = true;
            FindObjectOfType<HealthManager>().changeCheckPoint(other.transform.position);

            GameObject flag = transform.GetChild(0).gameObject;

            flag.GetComponent<MeshRenderer>().material = conqueredMaterial;

        }
    }
}
