using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unit04.Game.Casting;
using Unit04.Game.Directing;
using Unit04.Game.Services;


namespace Unit04
{
    /// <summary>
    /// The program's entry point.
    /// </summary>
    class Program
    {
        private static int FRAME_RATE = 48;
        private static int MAX_X = 900;
        private static int MAX_Y = 600;
        private static int CELL_SIZE = 15;
        private static int FONT_SIZE = 15;
        private static int COLS = 60;
        private static int ROWS = 40;
        private static string CAPTION = "Carbon";
        private static List<string> MINERAL_SPRITES = new List<string>{"O", "*", "1"};
        private static Color WHITE = new Color(255, 255, 255);
        private static int DEFAULT_MINERALS = 60;


        /// <summary>
        /// Starts the program using the given arguments.
        /// </summary>
        /// <param name="args">The given arguments.</param>
        static void Main(string[] args)
        {
            bool loadPlayer = false;
            bool loadZenMode = false;
            bool loadMatrix = false;
            bool loadPsychedelicMode = false;

            Console.WriteLine("Choose a mode:");
            Console.WriteLine("[N]ormal\n[Z]en\n[M]atrix\n[P]sychedelic");
            string gameMode = Console.ReadLine();
            switch (gameMode.ToUpper()){
                case "N":
                    loadPlayer = true;
                    break;
                case "Z":
                    loadZenMode = true;
                    break;
                case "M":
                    loadZenMode = true;
                    loadMatrix = true;
                    break;
                case "P":
                    loadPlayer = true;
                    loadPsychedelicMode = true;
                    break;
                default:
                    throw new Exception("invalid gamemode as input");
            }

            // create the cast
            Cast cast = new Cast();

            if (!loadZenMode){
                // create the score banner
                Actor banner = new Actor();
                banner.SetText("");
                banner.SetFontSize(FONT_SIZE * 2);
                banner.SetColor(WHITE);
                banner.SetPosition(new Point(CELL_SIZE, 0));
                cast.AddActor("banner", banner);

                // create the robot
                Actor robot = new Actor();
                robot.SetText("#");
                robot.SetFontSize(FONT_SIZE);
                robot.SetColor(WHITE);
                robot.SetPosition(new Point(MAX_X / 2, MAX_Y - 30));
                cast.AddActor("robot", robot);
            }

            if (!loadZenMode){
                // create the minerals for normal or psychedelic gameplay
                Random random = new Random();
                for (int i = 0; i < DEFAULT_MINERALS; i++)
                {
                    int x = random.Next(1, COLS);
                    int y = random.Next(1, ROWS);
                    Point position = new Point(x, y);
                    position = position.Scale(CELL_SIZE);

                    int r = random.Next(50, 256);
                    int g = random.Next(50, 256);
                    int b = random.Next(50, 256);
                    Color color = new Color(r, g, b);

                    int choice = random.Next(30);
                    bool specialMineral = false;
                    if (loadPsychedelicMode){
                        specialMineral = true;
                    }
                    else{
                        if (choice == 0 || choice == 20){
                            specialMineral = true;
                        }
                    }
                    Mineral mineral = new Mineral((choice < 20) ? "rock" : "gem", specialMineral);
                    string text = MINERAL_SPRITES[(choice < 20) ? 0 : 1];
                    mineral.SetText(text);
                    mineral.SetFontSize(FONT_SIZE);
                    mineral.SetColor(color);
                    mineral.SetPosition(position);
                    cast.AddActor("minerals", mineral);
                }
            }

            else{
                // create the minerals for zen or matrix gameplay
                Random random = new Random();
                for (int i = 0; i < (loadMatrix ? 1000 : 80); i++)
                {
                    int x = random.Next(1, COLS);
                    int y = random.Next(1, ROWS);
                    Point position = new Point(x, y);
                    position = position.Scale(CELL_SIZE);

                    int r = random.Next(50, 256);
                    int g = random.Next(50, 256);
                    int b = random.Next(50, 256);
                    if (loadMatrix){
                        r = 0;
                        b = 0;
                    }
                    Color color = new Color(r, g, b);

                    int choice = random.Next(30);
                    Mineral mineral = new Mineral((choice < 20) ? "rock" : "gem", false);
                    string text = MINERAL_SPRITES[(choice < 20) ? 0 : (!loadMatrix ? 1 : 2)];
                    mineral.SetText(text);
                    mineral.SetFontSize(FONT_SIZE);
                    mineral.SetColor(color);
                    mineral.SetPosition(position);
                    cast.AddActor("minerals", mineral);
                }
            }

            // create the game over screen
            Actor gameOverMessage = new Actor();
            gameOverMessage.SetText("");
            gameOverMessage.SetFontSize(40);
            gameOverMessage.SetColor(WHITE);
            gameOverMessage.SetPosition(new Point(MAX_X / 2 - 122, MAX_Y / 2 - 40));
            cast.AddActor("gameOverMessage", gameOverMessage);

            // create the win message
            Actor winMessage = new Actor();
            winMessage.SetText("");
            winMessage.SetFontSize(40);
            winMessage.SetColor(WHITE);
            winMessage.SetPosition(new Point(MAX_X / 2 - 82, MAX_Y / 2 - 40));
            cast.AddActor("winMessage", winMessage);

            // start the game
            KeyboardService keyboardService = new KeyboardService(CELL_SIZE);
            VideoService videoService 
                = new VideoService(CAPTION, MAX_X, MAX_Y, CELL_SIZE, FRAME_RATE, false);
            Director director = new Director(keyboardService, videoService, loadPlayer, COLS, CELL_SIZE);
            director.StartGame(cast);

            // test comment
        }
    }
}