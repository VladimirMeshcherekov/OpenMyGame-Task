using System.Collections.Generic;
using App.Scripts.Scenes.SceneChess.Features.ChessField.GridMatrix;
using PathFinding;
using UnityEngine;

namespace App.Scripts.Scenes.SceneChess.Features.GridNavigation.Navigator.ChessPiecesNavigation
{
    public class RookNavigation : ChessPieceNavigation
    {
        private readonly ChessGrid _grid;
        private const float NeighbourCellDistance = 1.4f;

        public RookNavigation(Vector2Int from, ChessGrid grid)
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
                    List<Vector2Int> availableMovePosition = AvailableMovePositions(new Vector2Int(x, y));
                    foreach (var newPosition in availableMovePosition)
                    {
                        if ((_grid.Get(currentPosition) == null && _grid.Get(newPosition) == null) || newPosition == startPosition ||  currentPosition == startPosition)
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

        private List<Vector2Int> AvailableMovePositions(Vector2Int startPoint)
        {
            
            List<Vector2Int> availableMovePositions = new List<Vector2Int>();
            availableMovePositions.AddRange(GetAvailableUpMoves(startPoint));
            availableMovePositions.AddRange(GetAvailableDownMoves(startPoint));
            availableMovePositions.AddRange(GetAvailableRightMoves(startPoint));
            availableMovePositions.AddRange(GetAvailableLeftMoves(startPoint));
            
            return availableMovePositions;
        }
        
         private List<Vector2Int> GetAvailableLeftMoves(Vector2Int startPoint)
        {
            List<Vector2Int> availableMoves = new  List<Vector2Int>();

            for (int i = 1; i <= GridSize; i++)
            {
                Vector2Int newPosition = new Vector2Int(startPoint.x + i, startPoint.y);
                if (IsPieceInsideField(newPosition) == false)
                {
                    continue;
                }

                if (_grid.Get(newPosition) != null)
                {
                    break;
                }

                availableMoves.Add(newPosition);
            }

            return availableMoves;
        }
        
        private List<Vector2Int> GetAvailableRightMoves(Vector2Int startPoint)
        {
            List<Vector2Int> availableMoves = new  List<Vector2Int>();
            
            for (int i = 1; i <= GridSize; i++)
            {
                Vector2Int newPosition = new Vector2Int(startPoint.x - i, startPoint.y);
                if (IsPieceInsideField(newPosition) == false)
                {
                    continue;
                }

                if (_grid.Get(newPosition) != null)
                {
                    break;
                }

                availableMoves.Add(newPosition);
            }

            return availableMoves;
        }        
        
        private List<Vector2Int> GetAvailableUpMoves(Vector2Int startPoint)
        {
            List<Vector2Int> availableMoves = new  List<Vector2Int>();
            
            for (int i = 1; i <= GridSize; i++)
            {
                Vector2Int newPosition = new Vector2Int(startPoint.x, startPoint.y + i);
                if (IsPieceInsideField(newPosition) == false)
                {
                    continue;
                }

                if (_grid.Get(newPosition) != null)
                {
                    break;
                }

                availableMoves.Add(newPosition);
            }

            return availableMoves;
        }
             
        private List<Vector2Int> GetAvailableDownMoves(Vector2Int startPoint)
        {
            List<Vector2Int> availableMoves = new  List<Vector2Int>();
            
            for (int i = 1; i <= GridSize; i++)
            {
                Vector2Int newPosition = new Vector2Int(startPoint.x, startPoint.y - i);
                if (IsPieceInsideField(newPosition) == false)
                {
                    continue;
                }

                if (_grid.Get(newPosition) != null)
                {
                    break;
                }

                availableMoves.Add(newPosition);
            }

            return availableMoves;
        }   
        
    }
}