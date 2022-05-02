using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    Node[] newPath;
    int targetIndex;
    [SerializeField] float speed;
    public void OnPathFound(List<Node> path)
    {
        path.Reverse();
        newPath = path.ToArray();
        StopCoroutine("FollowPath");
        StartCoroutine("FollowPath");
    }
    IEnumerator FollowPath()
    {
        Vector3 currentNode = newPath[0].Position;
        while (true)
        {
            if (transform.position == currentNode)
            {
                targetIndex++;
                if (targetIndex >= newPath.Length)
                    yield break;
                currentNode = newPath[targetIndex].Position;
            }
            transform.position = Vector3.MoveTowards(transform.position, currentNode, speed);
            yield return null;
        }
    }
}
