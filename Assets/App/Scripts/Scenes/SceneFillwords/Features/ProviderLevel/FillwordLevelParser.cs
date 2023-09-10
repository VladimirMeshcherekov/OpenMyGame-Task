using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace App.Scripts.Scenes.SceneFillwords.Features.ProviderLevel
{
    public class FillwordLevelParser
    {
        private int _levelIndex;
        private TextAsset _levelData;
        private TextAsset _wordsData;

        private const byte NewLineACSIICode = 10;
        private const byte SpaceACSIICode = 32;
        private const byte SemicolonACSIICode = 59;
        private const byte CarriageReturnACSIICode = 13;
        
        public List<string> LettersOrderList = new List<string>();

        public FillwordLevelParser(int levelIndex, string levelDataPath, string wordsListPath)
        {
            levelIndex -= 1;
            _levelIndex = levelIndex;

            _levelData = LoadData(levelDataPath);
            _wordsData = LoadData(wordsListPath);

            GetWordsList();
        }

        private void GetWordsList()
        {
            List<string> initialWords = new List<string>();
            string levelData = GetLineFromString(_levelData.ToString(), _levelIndex);
            List<int> wordsNums = GetWordsNums(levelData);

            List<int> linedLettersOrderInWords = GetWordIndexesInLine(GetLettersOrderInWords(levelData));

            foreach (var wordsNum in wordsNums)
            {
                initialWords.Add(GetLineFromString(_wordsData.ToString(), wordsNum));
            }

            string sumWord = GetWordsSum(initialWords);

            string[] lettersOrder = new string[sumWord.Length];

            if (sumWord.Length != linedLettersOrderInWords.Count)
            {
                return;
                throw new Exception("char order length is not correct for this word \n" + "word: " + sumWord + "| length: " +  linedLettersOrderInWords.Count);
            }

            for (int i = 0; i < sumWord.Length; i++)
            {
                lettersOrder[linedLettersOrderInWords[i]] = sumWord[i].ToString();
            }
            
            foreach (var letter in lettersOrder)
            {
                LettersOrderList.Add(letter);
            }
        }

        private TextAsset LoadData(string path)
        {
            return (TextAsset)Resources.Load(path);
        }

        private string GetLineFromString(string original, int lineNum)
        {
            var line = string.Empty;
            var currentLine = 0;

            foreach (var sign in original)
            {
                if (currentLine == lineNum)
                {
                    line += sign;
                }

                if (Encoding.ASCII.GetBytes(sign.ToString())[0] == NewLineACSIICode)
                {
                    currentLine++;
                    if (currentLine > lineNum)
                    {
                        break;
                    }
                }
            }

            if (line == string.Empty)
            {
                throw new Exception("No data for this level");
            }
            
            return line;
        }


        private List<int> GetWordsNums(string levelData)
        {
            bool isWordNum = true;
            string wordNum = string.Empty;
            List<int> wordsNums = new List<int>();

            foreach (var letter in levelData)
            {
                if (Encoding.ASCII.GetBytes(letter.ToString())[0] == SpaceACSIICode)
                {
                    if (isWordNum)
                    {
                        wordsNums.Add(int.Parse(wordNum));
                        wordNum = string.Empty;
                    }

                    isWordNum = !isWordNum;
                }

                if (isWordNum)
                {
                    wordNum += letter;
                }
            }

            return wordsNums;
        }


        private List<List<int>> GetLettersOrderInWords(string levelData)
        {
            List<string> words = ClearStringFromWordsNums(levelData);

            string letterPosition = string.Empty;
            List<List<int>> wordsLettersPositionInGridList = new List<List<int>>();
            List<int> wordLettersPositionInGrid = new List<int>();

            foreach (var word in words)
            {
                foreach (var character in word)
                {
                    if (Encoding.ASCII.GetBytes(character.ToString())[0] == SemicolonACSIICode)
                    {
                        wordLettersPositionInGrid.Add(int.Parse(letterPosition));
                        letterPosition = string.Empty;
                        continue;
                    }

                    letterPosition += character;
                }

                wordLettersPositionInGrid.Add(int.Parse(letterPosition));
                wordsLettersPositionInGridList.Add(wordLettersPositionInGrid);
                wordLettersPositionInGrid = new List<int>();
                letterPosition = string.Empty;
            }
            
            return wordsLettersPositionInGridList;
        }

        private List<string> ClearStringFromWordsNums(string levelData)
        {
            List<string> words = new List<string>();
            var currentWord = string.Empty;
            var spaceNum = 0;

            foreach (var letter in levelData)
            {
                if (Encoding.ASCII.GetBytes(letter.ToString())[0] == SpaceACSIICode)
                {
                    spaceNum++;
                    if (spaceNum % 2 == 0)
                    {
                        words.Add(currentWord);
                        currentWord = string.Empty;
                    }

                    continue;
                }

                if (spaceNum % 2 != 0)
                {
                    currentWord += letter;
                }
            }

            words.Add(currentWord);
            return words;
        }


        private string GetWordsSum(List<string> words)
        {
            string wordSum = string.Empty;
            foreach (var word in words)
            {
                foreach (var letter in word)
                {
                    if (Encoding.ASCII.GetBytes(letter.ToString())[0] == NewLineACSIICode ||
                        Encoding.ASCII.GetBytes(letter.ToString())[0] == SpaceACSIICode ||
                        Encoding.ASCII.GetBytes(letter.ToString())[0] == CarriageReturnACSIICode)
                    {
                        continue;
                    }

                    wordSum += letter;
                }
            }

            return wordSum;
        }

        private List<int> GetWordIndexesInLine(List<List<int>> lettersOrderInWords)
        {
            List<int> linedLettersOrderInWords = new List<int>();

            for (int i = 0; i < lettersOrderInWords.Count; i++)
            {
                for (int j = 0; j < lettersOrderInWords[i].Count; j++)
                {
                    linedLettersOrderInWords.Add(lettersOrderInWords[i][j]);
                }
            }

            return linedLettersOrderInWords;
        }
    }
}