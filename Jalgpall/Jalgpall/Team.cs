using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jalgpall
{
    public class Team
    {
        public List<Player> Players { get; } = new List<Player>(); // Список обьектов класса "Player"
        public string Name { get; private set; }  // Обычная текстовая переменная
        public Game Game { get; set; } // Поле тип данны класса "Game"
        // Конструктор
        public Team(string name) // Конструктор, который запрашивает текстовое значение и присваевает к полю "Name", к строке 12.
        {
            Name = name;
        }

        public void StartGameH(int width, int height) // начало игры со стороны комманды H
        {
            Console.ForegroundColor= ConsoleColor.Yellow;
            Random rnd = new Random();
            foreach (var player in Players) // Переберает игроков и в случайном порядке расставляет их на поле
            {
                player.SetPosition(
                    rnd.NextDouble() * width,
                    rnd.NextDouble() * height 
                    );
                player.DrawP(player);
            }
        }

        public void StartGameA(int width, int height) // начало игры со стороны комманды А
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Random rnd = new Random();
            foreach (var player in Players) // Переберает игроков и в случайном порядке расставляет их на поле
            {
                player.SetPosition(
                    rnd.NextDouble() * width,
                    rnd.NextDouble() * height
                    );
                player.DrawP(player);
            }
        }

        public void AddPlayer(Player player) // Добавление игрока
        {
            if (player.Team != null) return;
            Players.Add(player);
            player.Team = this;
        }

        public void GenATeam()
        {
            for (int i = 0; i < 11; i++)
            {
                AddPlayer(new Player("A"));
            }
        }

        public void GenHTeam()
        {
            for (int i = 0; i < 11; i++)
            {
                AddPlayer(new Player("H"));
            }
        }

        public (double, double) GetBallPosition() // Получение позиции мяча
        {
            return Game.GetBallPositionForTeam(this);
        }

        public void SetBallSpeed(double vx, double vy) // Устанавливается скорость мяча 
        {
            Game.SetBallSpeedForTeam(this, vx, vy);
        }

        public Player GetClosestPlayerToBall() // Распознование самого ближайшего игрока к мячу 
        {
            Player closestPlayer = Players[0];
            double bestDistance = Double.MaxValue; 
            foreach (var player in Players)
            {
                var distance = player.GetDistanceToBall();
                if (distance < bestDistance)
                {
                    closestPlayer = player;
                    bestDistance = distance;
                }
            }

            return closestPlayer;
        }

        public void Move() // Движение, распознает ближайшего игрока к мячу и начинает перемещать игрока к мячу
        {
            GetClosestPlayerToBall().MoveTowardsBall();
            Players.ForEach(player => player.Move());
            foreach (var player in Players) 
            {
                player.DrawP(player);
            }
        }
    }
}
