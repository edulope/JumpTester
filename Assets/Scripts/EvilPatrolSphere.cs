using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilPatrolSphere : MonoBehaviour
{
    public int currentIndex;
    public float speed;
    public float minDir;
    public Transform[] allChildren;

    // Start is called before the first frame update
    void Start()
    {
        List<Transform> transforms = new List<Transform>(GetComponentsInChildren<Transform>());
        transforms.RemoveAt(0);
        allChildren = transforms.ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(allChildren[currentIndex].position, transform.position);
        Debug.Log(distance);
        if(distance <= minDir){
            Debug.Log("mudou");
            currentIndex = currentIndex + 1;
            if(currentIndex >= allChildren.Length) currentIndex = 0;
        }
        Vector3 dir = (allChildren[currentIndex].position - transform.position).normalized;
        Vector3 movement = dir * Time.deltaTime * speed;
        transform.position += movement;
        foreach(Transform t in allChildren)
                t.position -= movement;
    }
}
