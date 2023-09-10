using System;
using System.Collections.Generic;
using System.Linq;
using App.Scripts.Libs.Factory;
using App.Scripts.Scenes.SceneWordSearch.Features.Level.Models.Level;
using UnityEngine;

namespace App.Scripts.Scenes.SceneWordSearch.Features.Level.BuilderLevelModel
{
    public class FactoryLevelModel : IFactory<LevelModel, LevelInfo, int>
    {
        public LevelModel Create(LevelInfo value, int levelNumber)
        {
            var model = new LevelModel();

            model.LevelNumber = levelNumber;

            model.Words = value.words;
            model.InputChars = BuildListChars(value.words);

            return model;
        }

        private List<char> BuildListChars(List<string> words)
        {
            //напиши реализацию не меняя сигнатуру функции

            if (words == null || words.Count == 0)
            {
                throw new Exception("words list is not defined");
            }
            
            List<char> chars = new List<char>();

            foreach (var word in words)
            {
                foreach (var letter in word)
                {
                    if (FindDuplicatesInWord(letter, word) > FindDuplicatesInList(letter, chars))
                    {
                        for (int i = 0; i < FindDuplicatesInWord(letter, word) - FindDuplicatesInList(letter, chars); i++)
                        {
                            chars.Add(letter);
                        }
                    }
                }
            }

            return chars;
            
        }


        private int FindDuplicatesInWord(char letter, string word)
        {
            return word.Count(newLetter => newLetter == letter);
        }

        private int FindDuplicatesInList<T>(T item, List<T> listToCheck)
        {
            return listToCheck.Count(newItem => newItem.Equals(item));
        }
        
    }
}