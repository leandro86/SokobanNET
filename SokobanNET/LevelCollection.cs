﻿using System;
using System.Linq;
using System.Xml.Linq;

namespace SokobanNET
{
    public class LevelCollection
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Copyright { get; private set; }
        public int NumberOfLevels { get; private set; }
        
        private XDocument _levelsFile;
        private Level[] _levels;

        public LevelCollection(string fileName)
        {
            LoadCollection(fileName);
        }

        public void LoadCollection(string fileName)
        {
            try
            {
                _levelsFile = XDocument.Load(fileName);
            }
            catch (Exception e)
            {
                // TODO: replace with custom exception
                throw new Exception("File doesn't exist");
            }

            Title = _levelsFile.Root.Element("Title").Value;
            Description = _levelsFile.Root.Element("Description").Value.Trim();
            Copyright = _levelsFile.Root.Element("LevelCollection").Attribute("Copyright").Value;
            NumberOfLevels = _levelsFile.Descendants("Level").Count();
        }

        public Level this[int levelNumber]
        {
            get { return GetLevel(levelNumber); }
        }

        private Level GetLevel(int levelNumber)
        {
            var level = _levelsFile.Descendants("Level").Skip(levelNumber).First().Elements();

            int levelWidth = (from row in level
                              select row.Value.Length).Max();
            int levelHeight = level.Count();

            string[] levelData = new string[levelHeight];
            int rowNumber = 0;
            foreach (var row in level)
            {
                levelData[rowNumber] += row.Value;
                rowNumber++;
            }

            return new Level() { Data = levelData, Width = levelWidth, Height = levelHeight };
        }
    }
}