using System.Collections.Generic;
using App.Scripts.Scenes.SceneChess.Features.ChessField.GridMatrix;
using PathFinding;
using UnityEngine;

namespace App.Scripts.Scenes.SceneChess.Features.GridNavigation.Navigator.ChessPiecesNavigation
{
    public abstract class ChessPieceNavigation
    {
        protected const int GridSize = 8;
        protected Path Path;
        protected Graph Graph;

        protected List<Node> Nodes = new List<Node>();
        protected List<Edge> Edges = new List<Edge>();

        protected void CreateGraph()
        {
            for (int y = 0; y < GridSize; y++)
            {
                for (int x = 0; x < GridSize; x++)
                {
                    var node = new Node(new Vector3(x, y));
                    Nodes.Add(node);
                }
            }
        }

        protected int ConvertVectorPositionToSquareNum(Vector2Int vectorPosition)
        {
            return vectorPosition.x + (vectorPosition.y * 8);
        }

        protected Vector2Int ConvertNumPositionToVector(int position)
        {
            return new Vector2Int(position % GridSize, position / GridSize);
        }

        protected bool IsPieceInsideField(Vector2Int position)
        {
            if (position.x > GridSize - 1 || position.x < 0 || position.y > GridSize - 1 || position.y < 0)
            {
                return false;
            }

            return true;
        }

        protected bool IsMoveAvailable(Vector2Int newPosition, Vector2Int currentPosition, Vector2Int startPosition, ChessGrid grid)
        {
            if ((grid.Get(currentPosition) == null && grid.Get(newPosition) == null) || newPosition == startPosition || currentPosition == startPosition)
            {
                return true;
            }

            return false;
        }

        public List<Vector2Int> GetPath(Vector2Int startPosition, Vector2Int endPosition)
        {
            Graph = new Graph(Nodes, Edges);
            Path = Graph.Find(ConvertVectorPositionToSquareNum(startPosition));

            List<Node> resultNodes;
            Path.Traverse(Graph, ConvertVectorPositionToSquareNum(endPosition), out resultNodes);
            List<Vector2Int> resultPath = new List<Vector2Int>();

            for (int i = 0; i < resultNodes.Count; i++)
            {
                for (int j = 0; j < Nodes.Count; j++)
                {
                    if (resultNodes[i] == Nodes[j] && resultPath.Contains(ConvertNumPositionToVector(j)) == false)
                    {
                        resultPath.Add(ConvertNumPositionToVector(j));
                        j = 0;
                    }
                }
            }

            if (resultPath.Count == 0)
            {
                return resultPath;
            }

            resultPath.Reverse();
            resultPath.RemoveAt(0);
            return resultPath;
        }
    }
}