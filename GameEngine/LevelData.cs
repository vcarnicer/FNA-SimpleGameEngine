using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.Xml.Serialization;

namespace GameEngine
{
    public class LevelData
    {
        [XmlElement("Player", Type = typeof(Player))] //Para el serializador: si encuentras una clase con ese nombre, asígnale ese tipo
        [XmlElement("Enemy", Type = typeof(Enemy))] 
        [XmlElement("PowerUp", Type = typeof(PowerUp))]
        public List<GameObject> objects { get; set; }
        public List<Wall> walls { get; set; }
        public List<Decor> decor { get; set; }
        public int mapWidth { get; set; }
        public int mapHeight { get; set; }
    }
}
