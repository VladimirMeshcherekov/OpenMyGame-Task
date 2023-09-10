using System;
using System.Collections.Generic;
using App.Scripts.Scenes.SceneChess.Features.ChessField.GridMatrix;
using App.Scripts.Scenes.SceneChess.Features.ChessField.Types;
using App.Scripts.Scenes.SceneChess.Features.GridNavigation.Navigator.ChessPiecesNavigation;
using UnityEngine;

namespace App.Scripts.Scenes.SceneChess.Features.GridNavigation.Navigator
{
    public class ChessGridNavigator : IChessGridNavigator
    {
        public List<Vector2Int> FindPath(ChessUnitType unit, Vector2Int from, Vector2Int to, ChessGrid grid)
        {
            //напиши реализацию не меняя сигнатуру функции
            ChessPieceNavigation chessPieceNavigation;
            
            List<Vector2Int> path = new List<Vector2Int>();
            switch (unit)
            {
                case ChessUnitType.Knight:
                    chessPieceNavigation = new KnightNavigation(from, grid);
                    path = chessPieceNavigation.GetPath(from, to);
                    break;
                
                case ChessUnitType.Bishop:
                    chessPieceNavigation = new BishopNavigation(from, grid);
                    path = chessPieceNavigation.GetPath(from, to);
                    break;
                
                case ChessUnitType.Rook:
                    chessPieceNavigation = new RookNavigation(from, grid);
                    path = chessPieceNavigation.GetPath(from, to);
                    break;
                
                case ChessUnitType.Queen:
                    chessPieceNavigation = new QueenNavigation(from, grid);
                    path = chessPieceNavigation.GetPath(from, to);
                    break;
                
                case ChessUnitType.Pon:
                    chessPieceNavigation = new PawnNavigation(from, grid); // пешка это pawn (https://en.wikipedia.org/wiki/Pawn_(chess)) :)
                    path = chessPieceNavigation.GetPath(from, to);
                    break;
                
                case ChessUnitType.King:
                    chessPieceNavigation = new KingNavigation(from, grid);
                    path = chessPieceNavigation.GetPath(from, to);
                    break;
                
                default:
                    throw new Exception("no navigation for " + nameof(unit));
            }
            
            if (path.Count == 0)
            {
                return null;
            }
            
            return path;
        }
    }
}