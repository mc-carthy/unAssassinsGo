using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour 
{
    private Vector2 coordinate;
    public Vector2 Coordinate { get { return Utility.Vector2Round(coordinate); }}

    private List<Node> neighbourNodes = new List<Node>();
    public List<Node> NeighbourNodes { get { return neighbourNodes; }}

    private List<Node> linkedNodes = new List<Node>();
    public List<Node> LinkedNodes { get { return linkedNodes; }}

    public GameObject geometry;
    public GameObject linkPrefab;
    public float scaleTime = 0.3f;
    public iTween.EaseType easeType = iTween.EaseType.easeInExpo;
    public float delay = 1f;
    public LayerMask obstacleLayer;
    public bool isLevelGoal = false;

    private Board board;
    private bool isInitialised = false;

    private void Awake()
    {
        board = Object.FindObjectOfType<Board>();
        coordinate = new Vector2(transform.position.x, transform.position.z);
    }

    private void Start()
    {
        if (geometry != null)
        {
            geometry.transform.localScale = Vector3.zero;

            if (board != null)
            {
                neighbourNodes = FindNeighbours(board.AllNodes);
            }
        }
    }

    public void ShowGeometry()
    {
        if (geometry != null)
        {
            iTween.ScaleTo(geometry, iTween.Hash(
                "time", scaleTime,
                "scale", Vector3.one,
                "easetype", easeType,
                "delay", delay
            ));
        }
    }

    public List<Node> FindNeighbours(List<Node> nodes)
    {
        List<Node> list = new List<Node>();

        foreach(Vector2 dir in Board.directions)
        {
            Node foundNeighbour = nodes.Find(n => n.Coordinate == (Coordinate + dir));
            
            if (foundNeighbour != null && !list.Contains(foundNeighbour))
            {
                list.Add(foundNeighbour);
            }
        }

        return list;
    }

    public void InitNode()
    {
        if (!isInitialised)
        {
            ShowGeometry();
            InitNeighbours();
            isInitialised = true;
        }
    }

    private void InitNeighbours()
    {
        StartCoroutine(InitNeighboursRoutine());
    }

    private IEnumerator InitNeighboursRoutine()
    {
        yield return new WaitForSeconds(delay);

        foreach(Node n in neighbourNodes)
        {
            if (!linkedNodes.Contains(n))
            {
                Obstacle obstacle = FindObstacle(n);

                if (obstacle == null)
                {
                    LinkNode(n);
                    n.InitNode();
                }
            }
        }
    }

    private void LinkNode(Node target)
    {
        if (linkPrefab != null)
        {
            GameObject linkInstance = Instantiate(linkPrefab, transform.position, Quaternion.identity);
            linkInstance.transform.parent = transform;

            Link link = linkInstance.GetComponent<Link>();
            if (link != null)
            {
                link.DrawLink(transform.position, target.transform.position);
            }

            if (!linkedNodes.Contains(target))
            {
                linkedNodes.Add(target);
            }

            if (!target.LinkedNodes.Contains(this))
            {
                target.LinkedNodes.Add(this);
            }
        }
    }

    private Obstacle FindObstacle(Node target)
    {
        Vector3 dir = target.transform.position - transform.position;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, dir, out hit, Board.spacing + 0.1f, obstacleLayer))
        {
            // Debug.Log("Hit obstacle between " + transform.position.x + "-" + transform.position.z + " and " + target.transform.position.x + "-" + target.transform.position.z);
            return hit.collider.gameObject.GetComponent<Obstacle>();
        }

        return null;
    }
}
