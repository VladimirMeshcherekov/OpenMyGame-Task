using System.Collections.Generic;
using App.Scripts.Scenes.SceneChess.Features.ChessField.GridMatrix;
using PathFinding;
using UnityEngine;

namespace App.Scripts.Scenes.SceneChess.Features.GridNavigation.Navigator.ChessPiecesNavigation
{
    public class KnightNavigation : ChessPieceNavigation
    {
        private readonly ChessGrid _grid;
      
        public KnightNavigation(Vector2Int from, ChessGrid grid)
        {
            _grid = grid;
            CreateGraph();
            CreatePathGraph(from);
        }
        
        private void CreatePathGraph(Vector2Int startPosition)
        {
            for (var y = 0; y < GridSize; y++)
            {
                for (var x = 0; x < GridSize; x++)
                {
                    var currentPosition = new Vector2Int(x, y);
                    List<Vector2Int> availableMovePosition = AvailableUnitMovePosition(new Vector2Int(x, y));
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

        private List<Vector2Int> AvailableUnitMovePosition(Vector2Int startPoint)
        {
            List<Vector2Int> availableMovePosition = new List<Vector2Int>();
            availableMovePosition.Add(new Vector2Int(startPoint.x + 2, startPoint.y + 1));
            availableMovePosition.Add(new Vector2Int(startPoint.x + 2, startPoint.y - 1));
            availableMovePosition.Add(new Vector2Int(startPoint.x - 2, startPoint.y + 1));
            availableMovePosition.Add(new Vector2Int(startPoint.x - 2, startPoint.y - 1));
            /**/
            availableMovePosition.Add(new Vector2Int(startPoint.x + 1, startPoint.y + 2));
            availableMovePosition.Add(new Vector2Int(startPoint.x + 1, startPoint.y - 2));
            availableMovePosition.Add(new Vector2Int(startPoint.x - 1, startPoint.y + 2));
            availableMovePosition.Add(new Vector2Int(startPoint.x - 1, startPoint.y - 2));

            foreach (var position in availableMovePosition.ToArray())
            {
                if (IsPieceInsideField(position) == false)
                {
                    availableMovePosition.Remove(position);
                }
            }
            return availableMovePosition;
        }
    }
}