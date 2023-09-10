using System.Collections.Generic;
using App.Scripts.Scenes.SceneChess.Features.ChessField.GridMatrix;
using PathFinding;
using UnityEngine;

namespace App.Scripts.Scenes.SceneChess.Features.GridNavigation.Navigator.ChessPiecesNavigation
{
    public class KingNavigation : ChessPieceNavigation
    {
        private readonly ChessGrid _grid;
        private const float NeighbourCellDistance = 1.4f;

        public KingNavigation(Vector2Int from, ChessGrid grid)
        {
            _grid = grid;
            CreateGraph();
            CreatePathGraph(from);
        }
        
        private void CreatePathGraph(Vector2Int startPosition)
        {
            for (int y = 0; y < GridSize; y++)
            {
                for (int x = 0; x < GridSize; x++)
                {
                    Vector2Int currentPosition = new Vector2Int(x, y);
                    List<Vector2Int> availableMovePosition = GetAvailableMoves(new Vector2Int(x, y));
                    foreach (var newPosition in availableMovePosition)
                    {
                        if (IsMoveAvailable(newPosition, currentPosition, startPosition, _grid))
                        {
                            if (Vector2Int.Distance(currentPosition, newPosition) <= NeighbourCellDistance)
                            {
                                Edges.Add(new Edge(Nodes[ConvertVectorPositionToSquareNum(currentPosition)], Nodes[ConvertVectorPositionToSquareNum(newPosition)]));
                                Nodes[ConvertVectorPositionToSquareNum(currentPosition)].Connect(Nodes[ConvertVectorPositionToSquareNum(newPosition)]);
                            }
                            else
                            {
                                Edges.Add(new Edge(Nodes[ConvertVectorPositionToSquareNum(currentPosition)], Nodes[ConvertVectorPositionToSquareNum(newPosition)], NeighbourCellDistance));
                                Nodes[ConvertVectorPositionToSquareNum(currentPosition)].Connect(Nodes[ConvertVectorPositionToSquareNum(newPosition)], NeighbourCellDistance);
                            }

                            Debug.DrawLine(new Vector3(currentPosition.x + 0.5f, currentPosition.y + 0.5f), new Vector3(newPosition.x + 0.5f, newPosition.y + 0.5f), Color.magenta, 10, false);
                        }
                    }
                }
            }
        }

        private List<Vector2Int> GetAvailableMoves(Vector2Int startPoint)
        {
            List<Vector2Int> availableMoves = new List<Vector2Int>();

            availableMoves.Add(new Vector2Int(startPoint.x + 1, startPoint.y + 1));
            availableMoves.Add(new Vector2Int(startPoint.x + 1, startPoint.y));
            availableMoves.Add(new Vector2Int(startPoint.x + 1, startPoint.y - 1));

            availableMoves.Add(new Vector2Int(startPoint.x, startPoint.y + 1));
            availableMoves.Add(new Vector2Int(startPoint.x, startPoint.y));
            availableMoves.Add(new Vector2Int(startPoint.x, startPoint.y - 1));

            availableMoves.Add(new Vector2Int(startPoint.x - 1, startPoint.y + 1));
            availableMoves.Add(new Vector2Int(startPoint.x - 1, startPoint.y));
            availableMoves.Add(new Vector2Int(startPoint.x - 1, startPoint.y - 1));

            foreach (var position in availableMoves.ToArray())
            {
                if (IsPieceInsideField(position) == false)
                {
                    availableMoves.Remove(position);
                }
            }

            return availableMoves;
        }
    }
}