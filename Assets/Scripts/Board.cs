using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour 
{
    private Node playerNode;
    public Node PlayerNode { get { return playerNode; }}
    
    public static float spacing = 2f;

    PlayerMovement player;

    public static readonly Vector2[] directions =
    {
        new Vector2(spacing, 0f),
        new Vector2(-spacing, 0f),
        new Vector2(0f, spacing),
        new Vector2(0f, -spacing)
    };

    private List<Node> allNodes = new List<Node>();
    public List<Node> AllNodes { get { return allNodes; }}

    private void Awake()
    {
        player = Object.FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>();
        GetNodeList();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0f, 1f, 1f, 0.5f);
        if (playerNode != null)
        {
            Gizmos.DrawSphere(playerNode.transform.position, 0.2f);
        }
    }

    public Node FindNodeAt(Vector3 pos)
    {
        Vector2 boardCoord = Utility.Vector2Round(new Vector2(pos.x, pos.z));

        return allNodes.Find(n => n.Coordinate == boardCoord);
    }

    public Node FindPlayerNode()
    {
        if (player != null && !player.isMoving)
        {
            return FindNodeAt(player.transform.position);
        }
        return null;
    }

    public void UpdatePlayerNode()
    {
        playerNode = FindPlayerNode();
    }

    private void GetNodeList()
    {
        Node[] nodeList = GameObject.FindObjectsOfType<Node>();
        allNodes = new List<Node>(nodeList);
    }

}
