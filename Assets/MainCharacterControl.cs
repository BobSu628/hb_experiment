using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterControl : MonoBehaviour
{
    public GameObject terrain;
    UnityEngine.AI.NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown (1)) {
             RaycastHit hit;
             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
             if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.gameObject == terrain) {
                 agent.SetDestination(new Vector3(hit.point.x, transform.position.y, hit.point.z));
             }
        }
    }
}
