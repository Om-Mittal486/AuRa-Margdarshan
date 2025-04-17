using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationTarget : MonoBehaviour
{
    [SerializeField]
    private Camera topDownCamera;
    [SerializeField]
    private GameObject navTargetObject;

    private NavMeshPath path;
    private LineRenderer line;

    private bool lineToggle = false;

    private void Start()
    {
        path = new NavMeshPath();
        line = GetComponent<LineRenderer>();

        if (line == null)
        {
            Debug.LogWarning("LineRenderer not found on this GameObject.");
        }
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            lineToggle = !lineToggle;
        }

        if (lineToggle)
        {
            bool pathFound = NavMesh.CalculatePath(transform.position, navTargetObject.transform.position, NavMesh.AllAreas, path);

            if (pathFound && path.status == NavMeshPathStatus.PathComplete)
            {
                line.positionCount = path.corners.Length;
                line.SetPositions(path.corners);
                line.enabled = true;
            }
            else
            {
                Debug.LogWarning("NavMesh path not found or incomplete.");
                line.enabled = false;
            }
        }
    }
}