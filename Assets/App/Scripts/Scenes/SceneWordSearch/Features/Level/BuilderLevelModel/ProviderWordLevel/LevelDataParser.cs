using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Scenes.SceneWordSearch.Features.Level.BuilderLevelModel.ProviderWordLevel
{
    public class LevelDataParser
    {
        public WordsContainer WordsListContainer { get; private set; }

        [Serializable]
        public class WordsContainer
        {
            public List<string> words;
        }

        public LevelDataParser(TextAsset json)
        {
            WordsListContainer = new WordsContainer();
            GetWordsFromJson(json.text);
        }

        private void GetWordsFromJson(string json)
        {
            JsonUtility.FromJsonOverwrite(json, WordsListContainer);
        }
    }
}