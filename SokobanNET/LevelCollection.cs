using System;
using System.Linq;
using System.Xml.Linq;

namespace SokobanNET
{
    public class LevelCollection
    {
        private XDocument _levelsFile;
        private Level[] _levels;
        
        public LevelCollection(string fileName)
        {
            LoadLevels(fileName);
        }

        public void LoadLevels(string fileName)
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

            var levels = from level in _levelsFile.Descendants("Level")
                         select level.Descendants("L");

            _levels = new Level[levels.Count()];

            int levelNumber = 0;
            foreach (var level in levels)
            {
                int width = (from row in level
                             select row.Value.Length).Max();
                int height = level.Count();
                
                string[] levelData = new string[height];
                int rowNumber = 0;
                foreach (var row in level)
                {
                    levelData[rowNumber] += row.Value;
                    rowNumber++;
                }

                _levels[levelNumber] = new Level() {Data = levelData, Width = width, Height = height};
                levelNumber++;
            }
        }

        public Level this[int i]
        {
            get { return _levels[i]; }
        }
    }
}
