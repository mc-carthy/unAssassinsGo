using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour 
{
    private Vector2 coordinate;
    public Vector2 Coordinate { get { return Utility.Vector2Round(coordinate); }}

    private List<Node> neighbourNodes = new List<Node>();
    public List<Node> NeighbourNodes { get { return neighbourNodes; }}

    public GameObject geometry;
    public float scaleTime = 0.3f;
    public iTween.EaseType easeType = iTween.EaseType.easeInExpo;
    public bool autorun = false;
    public float delay = 1f;

    private Board board;

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

            if (autorun)
            {
                ShowGeometry();
            }

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
}
