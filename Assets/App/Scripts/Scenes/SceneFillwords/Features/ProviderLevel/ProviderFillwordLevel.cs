using System.Collections.Generic;
using App.Scripts.Scenes.SceneFillwords.Features.FillwordModels;
using UnityEngine;

namespace App.Scripts.Scenes.SceneFillwords.Features.ProviderLevel
{
    public class ProviderFillwordLevel : IProviderFillwordLevel
    {
        public GridFillWords LoadModel(int index)
        {
            //напиши реализацию не меняя сигнатуру функции
            
            int levelIndex = index;
            FillwordLevelParser levelParser = new FillwordLevelParser(levelIndex, LevelDataPath(), WordsDataPath());
            List<string> lettersOrderList = levelParser.LettersOrderList;

            while (lettersOrderList.Count == 0)
            {
                levelIndex++;
                levelParser = new FillwordLevelParser(levelIndex, LevelDataPath(), WordsDataPath());
                lettersOrderList = levelParser.LettersOrderList;
            }
            
            Vector2Int gridSize = GetGridSize(lettersOrderList);

            GridFillWords gridFillWords = new GridFillWords(gridSize);

            int sellNum = 0;
            for (int i = 0; i < gridSize.y; i++)
            {
                for (int j = 0; j < gridSize.x; j++)
                {
                    CharGridModel charGridModel = new CharGridModel(lettersOrderList[sellNum][0]);
                    sellNum++;
                    gridFillWords.Set(i, j, charGridModel);
                }
            }

            return gridFillWords;
        }

        private Vector2Int GetGridSize<T>(List<T> elementsToGrid) // it doesn't specified in the tech specification that the grid should be a square
        {
            int elementsCount = elementsToGrid.Count;
            int sqrtElementsCount = (int)Mathf.Sqrt(elementsCount);

            for (int i = sqrtElementsCount; i > 0; i--)
            {
                if (elementsCount % i == 0)
                {
                    return new Vector2Int(elementsCount / i, i);
                }
            }

            return new Vector2Int(0, 0);
        }
        
        private string LevelDataPath()
        {
            return "Fillwords/pack_0";
        }

        private string WordsDataPath()
        {
            return "Fillwords/words_list";
        }
    }
}