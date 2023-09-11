using System.Collections.Generic;
using App.Scripts.Scenes.SceneChess.Features.ChessField.GridMatrix;
using PathFinding;
using UnityEngine;

namespace App.Scripts.Scenes.SceneChess.Features.GridNavigation.Navigator.ChessPiecesNavigation
{
    public class PawnNavigation : ChessPieceNavigation
    {
         private readonly ChessGrid _grid;
       
        private const int MaxRecursionSteps = 4;

        public PawnNavigation(Vector2Int from, ChessGrid grid)
        {
            _grid = grid;
            CreateGraph();
            SetupPathGraph(from);
        }

        private void SetupPathGraph(Vector2Int from)
        {
            CreatePathGraph(from, 0);
        }

        private void CreatePathGraph(Vector2Int startPoint, int recursionStep)
        {
            recursionStep++;
            List<Vector2Int> availableMovePosition = AvailableUnitMovePosition(startPoint);

            foreach (var position in availableMovePosition)
            {
                if (_grid.Get(position) == null)
                {
                    if (Vector2Int.Distance(startPoint, position) <= 1.5f)
                    {
                        Edges.Add(new Edge(Nodes[ConvertVectorPositionToSquareNum(startPoint)], Nodes[ConvertVectorPositionToSquareNum(position)]));
                        Nodes[ConvertVectorPositionToSquareNum(startPoint)].Connect(Nodes[ConvertVectorPositionToSquareNum(position)]);

                        Edges.Add(new Edge(Nodes[ConvertVectorPositionToSquareNum(position)], Nodes[ConvertVectorPositionToSquareNum(startPoint)]));
                        Nodes[ConvertVectorPositionToSquareNum(position)].Connect(Nodes[ConvertVectorPositionToSquareNum(startPoint)]);
                    }
                    else
                    {
                        Edges.Add(new Edge(Nodes[ConvertVectorPositionToSquareNum(startPoint)], Nodes[ConvertVectorPositionToSquareNum(position)], 1.1f));
                        Nodes[ConvertVectorPositionToSquareNum(startPoint)].Connect(Nodes[ConvertVectorPositionToSquareNum(position)], 1.1f);

                        Edges.Add(new Edge(Nodes[ConvertVectorPositionToSquareNum(position)], Nodes[ConvertVectorPositionToSquareNum(startPoint)], 1.1f));
                        Nodes[ConvertVectorPositionToSquareNum(position)].Connect(Nodes[ConvertVectorPositionToSquareNum(startPoint)], 1.1f);
                    }

                    Debug.DrawLine(new Vector3(startPoint.x + 0.5f, startPoint.y + 0.5f),
                        new Vector3(position.x + 0.5f, position.y + 0.5f), Color.magenta, 10, false);
                }
            }

            if (recursionStep >= MaxRecursionSteps)
            {
                return;
            }

            foreach (var newPosition in availableMovePosition)
            {
                if (_grid.Get(newPosition) == null)
                {
                    int newRecursionStep = recursionStep;
                    CreatePathGraph(newPosition, newRecursionStep);
                }
            }
        }

        private List<Vector2Int> AvailableUnitMovePosition(Vector2Int startPoint)
        {
            List<Vector2Int> availableMovePosition = new List<Vector2Int>();

            availableMovePosition.Add(new Vector2Int(startPoint.x, startPoint.y + 1));

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