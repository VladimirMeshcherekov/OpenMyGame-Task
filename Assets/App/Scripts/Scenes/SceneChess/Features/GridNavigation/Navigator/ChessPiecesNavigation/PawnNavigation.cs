using System.Collections.Generic;
using App.Scripts.Scenes.SceneChess.Features.ChessField.GridMatrix;
using PathFinding;
using UnityEngine;

namespace App.Scripts.Scenes.SceneChess.Features.GridNavigation.Navigator.ChessPiecesNavigation
{
    public class PawnNavigation : ChessPieceNavigation
    {
        private readonly ChessGrid _grid;

        public PawnNavigation(Vector2Int from, ChessGrid grid)
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
                    List<Vector2Int> availableMovePosition = GetAvailableMovePositions(new Vector2Int(x, y));
                    foreach (var newPosition in availableMovePosition)
                    {
                        if (IsMoveAvailable(newPosition, currentPosition, startPosition, _grid))
                        {
                            Edges.Add(new Edge(Nodes[ConvertVectorPositionToSquareNum(currentPosition)], Nodes[ConvertVectorPositionToSquareNum(newPosition)]));
                            Nodes[ConvertVectorPositionToSquareNum(currentPosition)].Connect(Nodes[ConvertVectorPositionToSquareNum(newPosition)]);
                            
                            Debug.DrawLine(new Vector3(currentPosition.x + 0.5f, currentPosition.y + 0.5f), new Vector3(newPosition.x + 0.5f, newPosition.y + 0.5f), Color.magenta, 10, false);
                        }
                    }
                }
            }
        }

        private List<Vector2Int> GetAvailableMovePositions(Vector2Int startPoint)
        {
            List<Vector2Int> availableMovePositions = new List<Vector2Int>();
            availableMovePositions.Add(new Vector2Int(startPoint.x, startPoint.y + 1));

            foreach (var position in availableMovePositions.ToArray())
            {
                if (IsPieceInsideField(position) == false)
                {
                    availableMovePositions.Remove(position);
                }
            }
            return availableMovePositions;
        }
    }
}