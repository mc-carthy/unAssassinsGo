﻿using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour 
{
    public static float spacing = 2f;

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
        GetNodeList();
    }

    private void GetNodeList()
    {
        Node[] nodeList = GameObject.FindObjectsOfType<Node>();
        allNodes = new List<Node>(nodeList);
    }

}