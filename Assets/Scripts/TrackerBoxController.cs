using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerBoxController : MonoBehaviour
{

    public TrackerController[] allChildren;

    // Start is called before the first frame update
    void Start()
    {
        List<TrackerController> trackers = new List<TrackerController>(GetComponentsInChildren<TrackerController>());
        //trackers.RemoveAt(0);
        allChildren = trackers.ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
             foreach(TrackerController t in allChildren){
                t.setTarget(other.transform);
                t.activate();
             }
        }    
    }

     private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player"){
            foreach(TrackerController t in allChildren){
                t.deactivate();
             }
        }    
    }
}
