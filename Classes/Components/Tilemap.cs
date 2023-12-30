using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Quase.Classes.Engine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Quase.Classes.GameComponents
{
    public class Tilemap
    {
        string _dir;
        float _scale;
        Texture2D _texAtlas;
        dynamic jsonData;
        int mapWidth;
        int mapHeight;
        int rows;
        int cols;
        int TileWidth;
        int TileHeight;
        int LayerCount;
        float _scaleOriginal;
        List<int[]> data = new List<int[]>();
        List<Rectangle> _sourceRects = new List<Rectangle>();
        JArray[] layers;
        JArray collisionObjects;
        List<float[]> collisionData = new List<float[]>();
        public Tilemap(string Directory, Texture2D Tileset, float scale, Vector2 TileDimensions)
        {
            _dir= Directory;
            _scaleOriginal = scale;
            _texAtlas= Tileset;
            TileWidth = (int)TileDimensions.X;
            TileHeight = (int)TileDimensions.Y;
            jsonData = JsonConvert.DeserializeObject(File.ReadAllText(_dir));
            mapWidth = jsonData["width"];
            mapHeight = jsonData["height"];
            LayerCount = jsonData.SelectToken("layers").Count - 1;
            rows = (int)(Tileset.Width /TileDimensions.X);
            cols = (int)(Tileset.Width / TileDimensions.Y);
            //the loop below is responsible for extracting each tile from the atlas passed in
            for(int y = 0; y < cols; y++)
            {
                for(int x = 0; x < rows; x++)
                {
                    _sourceRects.Add(new Rectangle(x * (int)TileDimensions.X, y * (int)TileDimensions.Y, (int)TileDimensions.X, (int)TileDimensions.Y));
                }
            }
            layers = new JArray[LayerCount + 1]; 
            for (int i = 0; i < LayerCount; i++)
            {
                layers[i] = (JArray)jsonData.SelectToken("layers")[i].SelectToken("data");
            }
            //this is necessary as i was getting errors for overflow error
            for (int curLay = 0; curLay < LayerCount; curLay++)
            {
                data.Add(layers[curLay].ToObject<int[]>());
            }
            collisionObjects = (JArray)jsonData.SelectToken("layers")[LayerCount].SelectToken("objects");
            for(int i = 0; i < collisionObjects.Count; i++)
            {
                float[] tempArr = new float[4];
                tempArr[0] = jsonData.SelectToken("layers")[LayerCount].SelectToken("objects")[i].SelectToken("x");
                tempArr[1] = jsonData.SelectToken("layers")[LayerCount].SelectToken("objects")[i].SelectToken("y");
                tempArr[2] = jsonData.SelectToken("layers")[LayerCount].SelectToken("objects")[i].SelectToken("width");
                tempArr[3] = jsonData.SelectToken("layers")[LayerCount].SelectToken("objects")[i].SelectToken("height");
                collisionData.Add(tempArr);
            }
        }
        public void Update()
        {
            for (int i = 0; i < collisionObjects.Count; i++)
            {
                Vector2 PlayerVel = new Vector2(Objects.Player.VelocityX, -Objects.Player.VelocityY);
                Vector2 colPos = (CoordinateEngine.GetScreenPosition(new Vector2((collisionData[i][0] - 39) / 64, (collisionData[i][1] - 39) / -64) * _scale)) - PlayerVel;
                Vector2 colSize = new Vector2(collisionData[i][2], collisionData[i][3]) * _scale;
                Rectangle collider = new Rectangle((int)(colPos.X), (int)(colPos.Y), (int)(colSize.X), (int)(colSize.Y));
                if (collider.X > -64 && collider.X < 1920)
                {
                    Objects.Colliders.Add(collider);
                }
            }
        }
        public void Draw()
        {
            _scale = _scaleOriginal;
            int index = 0;
            for(int layerNum = 0; layerNum < LayerCount; layerNum++)
            {
                for(int y = 0; y < mapHeight; y++)
                {
                    for(int x = 0; x < mapWidth; x++)
                    {
                        if ((int)data[layerNum][index] != 0)
                        {
                            if (Culling.IsVisible(new Vector2(x, -y) * _scale))
                            {
                                Vector2 Pos = CoordinateEngine.GetScreenPosition(new Vector2(x,-y) * _scale);
                                Quase._spriteBatch.Draw(texture: _texAtlas,
                                    position: new Vector2(Pos.X, Pos.Y),
                                    _sourceRects[(int)data[layerNum][index] - 1],
                                    color: Color.White,
                                    rotation: 0f,
                                    new Vector2(TileWidth / 2, TileHeight / 2),
                                    scale: _scale,
                                    SpriteEffects.None,
                                    layerDepth: 1f);
                            }
                        }
                        index++;
                    }            
                }
                index = 0;
            }       
        }
    }
}
