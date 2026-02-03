namespace MonoGameLibrary.Graphics;

public class Tileset
{
    private readonly TextureRegion[] _tiles;
    public int Columns { get; set; }
    public int Rows { get; set; }
    public int TileWidth { get; set; }
    public int TileHeight { get; set; }
    public int Count { get; }

    public TextureRegion GetTile(int index) => _tiles[index]; // Return the tile in that index.

    public TextureRegion GetTile(int column, int row)
    {
        int index = row * Columns + column;
        return GetTile(index);
    }
    public Tileset(TextureRegion textureRegion, int tileWidth, int tileHeight)
    {
        TileWidth = tileWidth;
        TileHeight = tileHeight;
        Columns = textureRegion.Width / tileWidth;
        Rows = textureRegion.Height / tileHeight;
        Count = Rows * Columns;
        _tiles = new TextureRegion[Count];
        for (int i = 0; i < Count; i++)
        {
            int x = (i % Columns) * tileWidth;
            int y = (i / Columns) * tileHeight;
            _tiles[i] = new TextureRegion(textureRegion.Texture, textureRegion.SourceRectangle.X + x, textureRegion.SourceRectangle.Y + y, tileWidth, tileHeight);
        }

    }
}