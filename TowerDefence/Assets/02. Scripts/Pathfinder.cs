using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    public enum Options
    {
        FixedPoints,
        BFS,
        DFS
    }
    [SerializeField] private Options _option;
    private static List<Transform> _path;
    public bool TryFindOptimizedPath(Transform start, Transform end, out List<Transform> optimizedPath)
    {
        bool found = false;
        optimizedPath = null;
        switch (_option)
        {
            case Options.FixedPoints:
                found = SetPathWithFixedPathPoints(start, end);
                break;
            case Options.BFS:
                found = false;
                break;
            case Options.DFS:
                found = false;
                break;
            default:
                break;
        }

        if (found)
            optimizedPath = _path;

        return found;
    }

    private static bool SetPathWithFixedPathPoints(Transform start, Transform end)
    {
        foreach (Paths.Path path in Paths.Instance.GetPaths())
        {
            if(path.GetPathPoints()[0] == start)
            {
                _path = new List<Transform>(path.GetPathPoints());
                return true;
            }
        }
        return false;
    }
}
