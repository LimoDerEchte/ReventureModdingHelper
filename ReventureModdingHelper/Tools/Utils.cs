using System.IO;
using MelonLoader;
using UnityEngine;

namespace ReventureModdingHelper.Tools
{
    public class Utils
    {
        // By c68 (Unity Forum); modified
        public static Sprite LoadNewSprite(string filePath, float pixelsPerUnit = 8.0f)
        {
            string path = Path.Combine(MelonUtils.UserDataDirectory, filePath);
            Texture2D spriteTexture = LoadTexture(path);
            Sprite newSprite = Sprite.Create(spriteTexture, new Rect(0, 0, spriteTexture.width, spriteTexture.height),new Vector2(0.5F,0.5F), pixelsPerUnit);
            return newSprite;
        }
 
        // By c68 (Unity Forum); modified
        public static Texture2D LoadTexture(string filePath) {
            if (File.Exists(filePath)){
                var fileData = File.ReadAllBytes(filePath);
                var tex2D = new Texture2D(2, 2);
                tex2D.filterMode = FilterMode.Point;
                if (tex2D.LoadImage(fileData))
                    return tex2D;
            }  
            return null;
        }
    }
}