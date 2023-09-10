using System;
using System.Collections.Generic;
using App.Scripts.Scenes.SceneWordSearch.Features.Level.Models.Level;
using UnityEngine;

namespace App.Scripts.Scenes.SceneWordSearch.Features.Level.BuilderLevelModel.ProviderWordLevel
{
    public class ProviderWordLevel : IProviderWordLevel
    {
        public LevelInfo LoadLevelData(int levelIndex)
        {
            //напиши реализацию не меняя сигнатуру функции

            var parser = new LevelDataParser(LevelData(levelIndex));
            LevelInfo levelInfo = new LevelInfo();
            levelInfo.words = parser.WordsListContainer.words;
            return levelInfo;
        }

        private TextAsset LevelData(int levelIndex)
        {
            TextAsset levelData = (TextAsset)Resources.Load("WordSearch/Levels/" + levelIndex.ToString());

            if (levelData == null)
            {
                throw new Exception("No valid data for level " + levelIndex);
            }

            return levelData;
        }
    }
}