using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jalgpall
{
    public class Player
    {
        //атрибуты
        public string Name { get; } // Имя игрока ,Публичная переменная "Name" 
        public double X { get; private set; } // Горизонтальная координата игрока
        public double Y { get; private set; } // Вертикальная координата игрока
        private double _vx, _vy; //Расстояние мяча и игрока
        public Team? Team { get; set; } = null; // Team изначально имеет "null" он может как принадлежать так и не принадлежать команде
        // Команда в которой играет игрок.

        private const double MaxSpeed = 5; // Максимальная скорость мяча 
        private const double MaxKickSpeed = 25; // Максимальная скорость удара мяча
        private const double BallKickDistance = 15; // Дистанция на которую возможно ударить мяч

        private Random _random = new Random(); //Создается рандомное число
        //Конструкторы
        public Player(string name) // Конструктор, который запрашивает текстовое значение и присваевает к полю "Name"
        {
            Name = name;
        }

        public Player(string name, double x, double y, Team team)  //Констуктор еще присваевает координаты и команду, в данном случае игрок уже оказывает на поле в определенноц команде
        {
            Name = name;
            X = x;
            Y = y;
            Team = team;
        }

        public void SetPosition(double x, double y) // Функция берет значения созданные выше, и принимает их в переменный заполняя своим аргументом, то есть можно будет в дальнейшем указывать ее параметры чтоб передвигать игрока
        {
            X = x;
            Y = y;
        }

        public (double, double) GetAbsolutePosition() // Будет определять команду игрока. И уже от этого определит координату команда(левых), команда(правых).
        {
            return Team!.Game.GetPositionForTeam(Team, X, Y);
        }

        public double GetDistanceToBall() //Расчитывание растояние до мяча
        {
            var ballPosition = Team!.GetBallPosition();
            var dx = ballPosition.Item1 - X;
            var dy = ballPosition.Item2 - Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public void MoveTowardsBall() //Расчитывание движения до мяч
        {
            var ballPosition = Team!.GetBallPosition(); // Узнаем позицию мяча 
            var dx = ballPosition.Item1 - X;
            var dy = ballPosition.Item2 - Y;
            var ratio = Math.Sqrt(dx * dx + dy * dy) / MaxSpeed; // Теорема Пифагора + Скорость движения мяча
            _vx = dx / ratio;
            _vy = dy / ratio;
        }

        public void Move() //Движение
        {
            if (Team.GetClosestPlayerToBall() != this) // Если команда !равно, то мяч не меняет координаты
            {
                _vx = 0;
                _vy = 0;
            }

            if (GetDistanceToBall() < BallKickDistance) //Если дистанция мяча меньше чем дистанция на которую можно бить. ТО-
                //генерирую скорость движения мяча
            {
                Team.SetBallSpeed(
                    MaxKickSpeed * _random.NextDouble(),
                    MaxKickSpeed * (_random.NextDouble() - 0.5)
                    );
            }

            var newX = X + _vx;
            var newY = Y + _vy;
            var newAbsolutePosition = Team.Game.GetPositionForTeam(Team, newX, newY);
            if (Team.Game.Stadium.IsIn(newAbsolutePosition.Item1, newAbsolutePosition.Item2)) // Получаем 1 или 0 
            {
                X = newX;
                Y = newY;
            }
            else
            {
                _vx = _vy = 0;
            }
        }
        public void DrawP(Player player)
        {
            Console.SetCursorPosition((int)X, (int)Y);
            Console.Write(player.Name);
        }
    }
}
