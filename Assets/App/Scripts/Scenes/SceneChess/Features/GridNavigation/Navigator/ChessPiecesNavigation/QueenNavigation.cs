using System.Collections.Generic;
using App.Scripts.Scenes.SceneChess.Features.ChessField.GridMatrix;
using PathFinding;
using UnityEngine;

namespace App.Scripts.Scenes.SceneChess.Features.GridNavigation.Navigator.ChessPiecesNavigation
{
    public class QueenNavigation : ChessPieceNavigation
    {
        private readonly ChessGrid _grid;
        private const float NeighbourCellDistance = 1.4f;

        public QueenNavigation(Vector2Int from, ChessGrid grid)
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
            
           availableMoves.AddRange(GetAvailableLeftMoves(startPoint));
           availableMoves.AddRange(GetAvailableRightMoves(startPoint));
           availableMoves.AddRange(GetAvailableDownMoves(startPoint));
           availableMoves.AddRange(GetAvailableUpMoves(startPoint));
           
           availableMoves.AddRange(GetAvailableLeftUpMoves(startPoint));
           availableMoves.AddRange(GetAvailableRightUpMoves(startPoint));
           availableMoves.AddRange(GetAvailableLeftDownMoves(startPoint));
           availableMoves.AddRange(GetAvailableRightDownMoves(startPoint));
           
            return availableMoves;
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
        /*//*/
        private List<Vector2Int> GetAvailableLeftUpMoves(Vector2Int startPoint)
        {
            List<Vector2Int> availableMoves = new  List<Vector2Int>();
            
            for (int i = 1; i <= GridSize; i++)
            {
                Vector2Int newPosition = new Vector2Int(startPoint.x - i, startPoint.y + i);
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
        
         private List<Vector2Int> GetAvailableRightUpMoves(Vector2Int startPoint)
        {
            List<Vector2Int> availableMoves = new  List<Vector2Int>();
            
            for (int i = 1; i <= GridSize; i++)
            {
                Vector2Int newPosition = new Vector2Int(startPoint.x + i, startPoint.y + i);
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
         
         private List<Vector2Int> GetAvailableLeftDownMoves(Vector2Int startPoint)
        {
            List<Vector2Int> availableMoves = new  List<Vector2Int>();
            
            for (int i = 1; i <= GridSize; i++)
            {
                Vector2Int newPosition = new Vector2Int(startPoint.x - i, startPoint.y - i);
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
         
         private List<Vector2Int> GetAvailableRightDownMoves(Vector2Int startPoint)
        {
            List<Vector2Int> availableMoves = new  List<Vector2Int>();
            
            for (int i = 1; i <= GridSize; i++)
            {
                Vector2Int newPosition = new Vector2Int(startPoint.x + i, startPoint.y - i);
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