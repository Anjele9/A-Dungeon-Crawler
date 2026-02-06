namespace DungeonCrawler;

public class Game1 : Core
{
    private DirectionalAnimatedSprite _slime;

    NPC afkSlime;
    Player player;
    List<Entity> entities = new List<Entity>();
    PauseButton pauseButton;

    Vector2 _afkSlimePosition = new Vector2(256, 256);
    private Tilemap _tilemap;
    private Rectangle _roomBounds;
    Texture2D pixel;
    SpriteFont font;
    bool gamePaused = false;
    public Game1() : base("Dungeon Crawler", 1280, 720, false)
    {
    }
    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
        Rectangle screenBounds = GraphicsDevice.PresentationParameters.Bounds;
        _roomBounds = new Rectangle(
        (int)_tilemap.TileWidth,
        (int)_tilemap.TileHeight,
        screenBounds.Width - (int)_tilemap.TileWidth * 2,
        screenBounds.Height - (int)_tilemap.TileHeight * 2
        );

        pixel = new Texture2D(GraphicsDevice, 1, 1);
        pixel.SetData(new[] { Color.White });
    }
    protected override void LoadContent()
    {
        font = Content.Load<SpriteFont>("DefaultFont");
        TextureAtlas atlas = TextureAtlas.FromFile(Content, "Images/slime-definition.xml");
        var slimeConfig = DirectionalSpriteConfigLoader.LoadFromXml("Content/Images/slime-definition.xml", "slime");
        _slime = DirectionalSpriteFactory.Create(slimeConfig, atlas);
        _tilemap = Tilemap.FromFile(Content, "Images/Map1.xml");
        _tilemap.Scale = new Vector2(4.0f, 4.0f);
        player = new Player(DirectionalSpriteFactory.Create(slimeConfig, atlas), new Circle(128, 128, (int)(_slime.Width * 0.5f)), new Vector2(128, 128), 3f);
        afkSlime = new NPC(DirectionalSpriteFactory.Create(slimeConfig, atlas), new Circle(256, 256, (int)(_slime.Width * 0.5f)), _afkSlimePosition, 0f);
        
        entities.Add(player);
        entities.Add(afkSlime);
        
    }
    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        if (Input.Keyboard.anyKeyDown())
        {
            CheckKeyboardInput();
        }
        Input.Update(gameTime);
        if (!gamePaused)
        {
            player.Update(gameTime);
            afkSlime.Update(gameTime);
            base.Update(gameTime);
        }


    }
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);
        SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
        _tilemap.Draw(SpriteBatch);
        player.Draw(SpriteBatch);
        afkSlime.Draw(SpriteBatch);
        player.DrawBounds(SpriteBatch, pixel, Color.Red);
        afkSlime.DrawBounds(SpriteBatch, pixel, Color.Blue);
        SpriteBatch.End();
        
        base.Draw(gameTime);
    }
    private void CheckKeyboardInput()
    {
        if (Input.Keyboard.wasKeyPressed(Keys.P))
        {
            gamePaused = !gamePaused;
            Console.WriteLine($"{gamePaused}");
        }
        if (gamePaused)
            return;
        bool Sprint = Input.Keyboard.isKeyDown(Keys.LeftShift);
        if (Input.Keyboard.anyKeyDown()) player.MoveInput(Input.Keyboard.currentState, Sprint, entities, _roomBounds);
    }
}
