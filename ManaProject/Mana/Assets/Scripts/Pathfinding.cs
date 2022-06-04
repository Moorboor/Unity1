using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
    private const int MoveDiagonalCost = 14; // sqrt(200) = 14 
    private const int MoveStraightCost = 10;

    public Grid<PathNode> grid;
    private List<PathNode> openList;
    private List<PathNode> closedList;


    // Constructor 
    public Pathfinding(int width, int height)
    {
        grid = new Grid<PathNode>(width, height, 10f, Vector3.zero, (Grid<PathNode> g, int x, int z) => new PathNode(g, x, z));
    }

    public Grid<PathNode> GetGrid()
    {
        return grid;
    }

    public List<PathNode> FindPath(int startX, int startZ, int endX, int endZ)
    {
        PathNode startNode = grid.GetGridObject(startX, startZ);
        PathNode endNode = grid.GetGridObject(endX, endZ);

        openList = new List<PathNode> { startNode };
        closedList = new List<PathNode>();

        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int z = 0; z < grid.GetHeight(); z++)
            {
                PathNode pathNode = grid.GetGridObject(x, z);
                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.cameFromNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();
 
        while(openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(openList);
            if (currentNode == endNode)
            {
                return CalculatePath(endNode);
            }
 
            openList.Remove(currentNode);
            closedList.Add(currentNode);


            foreach (PathNode neighbourNode in GetNeighboursList(currentNode))
            {
                if (closedList.Contains(neighbourNode)) continue;

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                if (tentativeGCost < neighbourNode.gCost)
                {
                    neighbourNode.cameFromNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();

                    if (!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }
        // We are out of nodes on the open list
        return null;


    }
    private List<PathNode> GetNeighboursList(PathNode currentNode)
    {
        List<PathNode> neighboursList = new List<PathNode>(); // list instantiation with ()

        if (currentNode.x - 1 >= 0)
        {
            // Left
            neighboursList.Add(GetNode(currentNode.x - 1, currentNode.z));
            // Left Down
            if (currentNode.z - 1 >= 0) neighboursList.Add(GetNode(currentNode.x - 1, currentNode.z - 1));
            // Left Up
            if (currentNode.z + 1 < grid.GetHeight()) neighboursList.Add(GetNode(currentNode.x - 1, currentNode.z + 1));
        }
        if (currentNode.x + 1 < grid.GetWidth())
        {
            // Right
            neighboursList.Add(GetNode(currentNode.x + 1, currentNode.z));
            // Right Down
            if (currentNode.z - 1 >= 0) neighboursList.Add(GetNode(currentNode.x + 1, currentNode.z - 1));
            // Right Up
            if (currentNode.z + 1 < grid.GetHeight()) neighboursList.Add(GetNode(currentNode.x + 1, currentNode.z + 1));
        }

        if (currentNode.z - 1 >= 0)
        {
            // Down
            neighboursList.Add(GetNode(currentNode.x, currentNode.z - 1));
        }
        if (currentNode.z + 1 < grid.GetHeight())
        {
            // Up
            neighboursList.Add(GetNode(currentNode.x, currentNode.z + 1));
        }
        return neighboursList;
    }
    private PathNode GetNode(int x, int z)
    {
        return grid.GetGridObject(x, z);
    }
    private List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);
        PathNode currentNode = endNode;
        while(currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }
        path.Reverse();
        return path;
    }

    private int CalculateDistanceCost(PathNode a, PathNode b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int zDistance = Mathf.Abs(a.z - b.z);
        int remaining = Mathf.Abs(xDistance - zDistance);
 
        return MoveDiagonalCost * Mathf.Min(xDistance, zDistance) + MoveStraightCost * remaining;
    }

    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFCostNode = pathNodeList[0];
        for (int i = 0; i < pathNodeList.Count; i++)
        {
            if(pathNodeList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = pathNodeList[i];
            }
        }
        return lowestFCostNode;
    }
}
