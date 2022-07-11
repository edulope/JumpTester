using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerController : MonoBehaviour
{
    private bool activated;
    private Transform target;
    public float velocity;
    public CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        activated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(activated){
             controller.Move((Vector3.Normalize(target.position - transform.position)) * velocity * Time.deltaTime);
        }
    }

    public void setTarget(Transform t){
        target = t;
    }

    public void activate(){
        activated = true;
    }

    public void deactivate(){
        activated = false;
    }
}
