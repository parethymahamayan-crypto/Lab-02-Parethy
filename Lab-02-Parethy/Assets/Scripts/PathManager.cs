using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    [HideInInspector]
    [SerializeField]

    public List<waypoint> path;

    public List<waypoint> GetPath()
    {
        if (path == null)
            path = new List<waypoint>();

        return path;
    }
    
    public void CreateAddPoint()
    {
        waypoint go = new waypoint();
        path.Add(go);
    }

    public GameObject prefab;
    int currentPointIndex = 0;

    public List<GameObject> prefabPoints;

    public waypoint GetNextTarget()
    {
        int nextPointIndex = (currentPointIndex + 1) % (path.Count);
        currentPointIndex = nextPointIndex;
        return path[nextPointIndex];
    }

    public void Start()
    {
        prefabPoints = new List<GameObject>();
        //create prefab colliders for the path locations
        foreach (waypoint p in path)
        {
            GameObject go = Instantiate(prefab);
            go.transform.position = p.pos;
            prefabPoints.Add(go);
        }
    }
    public void Update()
    {
        //update all of the prefabs to the waypoint location
        for (int i = 0; i < path.Count; i++)
        {
            waypoint p = path[i];
            GameObject g = prefabPoints[i];
            g.transform.position = p.pos;
        }
    }
}
