using System.IO;
using System.Reflection;
using System.Xml.Linq;

namespace Snakity
{
    public static class SettingsMan
    {
        private static XElement _settings;
        private static readonly string SettingsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Settings.xml");
        public static int Highscore
        {
            get
            {
                if (_settings == null)
                    Load();
                return int.Parse(_settings.Element("highscore").Value);
            }
            set
            {
                _settings.Element("highscore").Value = value.ToString();
                Save();
            }
        }
        
        public static bool SmoothPlayer
        {
            get
            {
                if (_settings == null)
                    Load();
                return bool.Parse(_settings.Element("smoothplayer").Value);
            }
            set
            {
                _settings.Element("smoothplayer").Value = value.ToString();
                Save();
            }
        }
        
        public static bool SmoothTerrain
        {
            get
            {
                if (_settings == null)
                    Load();
                return bool.Parse(_settings.Element("smoothterrain").Value);
            }
            set
            {
                _settings.Element("smoothterrain").Value = value.ToString();
                Save();
            }
        }
        
        public static bool Color
        {
            get
            {
                if (_settings == null)
                    Load();
                return bool.Parse(_settings.Element("color").Value);
            }
            set
            {
                _settings.Element("color").Value = value.ToString();
                Save();
            }
        }

        private static void Load()
        {
            _settings = File.Exists(SettingsPath) ? XElement.Load(SettingsPath) : new XElement("settings");
            if (_settings.Element("highscore") == null || _settings.Element("highscore").Value == null)
                _settings.Add(new XElement("highscore", 0));
            if (_settings.Element("smoothplayer") == null || _settings.Element("smoothplayer").Value == null)
                _settings.Add(new XElement("smoothplayer", true));
            if (_settings.Element("smoothterrain") == null || _settings.Element("smoothterrain").Value == null)
                _settings.Add(new XElement("smoothterrain", true));
            if (_settings.Element("color") == null || _settings.Element("color").Value == null)
                _settings.Add(new XElement("color", true));
        }

        private static void Save()
        {
            if (_settings == null)
                Load();
            _settings.Save(SettingsPath);
        }
    }
}