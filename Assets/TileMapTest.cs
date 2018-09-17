using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapTest : MonoBehaviour
{
    Tilemap tileMap;

    // Start is called before the first frame update
    void Start()
    {
        Ultima.Files.SetMulPath(@"F:\Jogos\UO\Ultima Online Latest");
        tileMap = FindObjectOfType<Tilemap>();

        for (int y = 1630; y < 1730; y++)
        {
            for (int x = 1350; x < 1450; x++)
            {
                var uoTile = Ultima.Map.Trammel.Tiles.GetLandTile(x, y);
                addTile(y, x, uoTile.Z, GetSprite(uoTile.ID, true));

                //var statics = Ultima.Map.Trammel.Tiles.GetStaticTiles(x, y);
                //foreach (var st in statics)
                //{
                //    addTile(y, x, st.Z, GetSprite(st.ID, false));
                //}
            }
        }
    }

    private void addTile(int y, int x, int z, Sprite sprite)
    {
        var tile = ScriptableObject.CreateInstance<Tile>();
        tile.sprite = sprite;
        tileMap.SetTile(new Vector3Int(x - 1350, y - 1630, z), tile);
        tileMap.SetTransformMatrix(new Vector3Int(x - 1350, y - 1630, z), Matrix4x4.Translate(new Vector3(0, z / 2)));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected Bitmap GetBmp(int id, bool isLand)
    {
        if (isLand)
            return Ultima.Art.GetLand(id);
        else
            return Ultima.Art.GetStatic(id);
    }

    protected Texture2D GetTexture(int id, bool isLand)
    {
        return GetTexture(GetBmp(id, isLand));
    }

    protected Texture2D GetTexture(Bitmap bmp)
    {
        //var bmpdata = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, bmp.PixelFormat);
        //byte[] bytedata = null;

        //try
        //{
        //    int numbytes = bmpdata.Stride * bmp.Height;
        //    bytedata = new byte[numbytes];
        //    System.IntPtr ptr = bmpdata.Scan0;

        //    System.Runtime.InteropServices.Marshal.Copy(ptr, bytedata, 0, numbytes);
        //}
        //finally
        //{
        //    bmp.UnlockBits(bmpdata);
        //}

        MemoryStream ms = new MemoryStream();
        bmp.Save(ms, ImageFormat.Png);

        Texture2D texture = new Texture2D(bmp.Width, bmp.Height);
        texture.LoadImage(ms.ToArray());
        texture.filterMode = FilterMode.Point;
        //texture.LoadRawTextureData(bytedata);

        //RenderTexture teste = new RenderTexture(bmp.Width, bmp.Height, 16, RenderTextureFormat.ARGB1555);
        //texture.ReadPixels(new Rect(0, 0, bmp.Width, bmp.Height), 0, 0);
        //texture.Apply();
        
        return texture;
    }

    protected Sprite GetSprite(int id, bool isLand)
    {
        return GetSprite(GetBmp(id, isLand));
    }
    
    protected Sprite GetSprite(Bitmap bmp)
    {
        return GetSprite(GetTexture(bmp));
    }

    protected Sprite GetSprite(Texture2D texture)
    {
        Rect rect = new Rect(0, 0, texture.width, texture.height);
        var sprite = Sprite.Create(texture, rect, new Vector2(0.0f, 0.0f), 16);
        
        return sprite;
    }
}
