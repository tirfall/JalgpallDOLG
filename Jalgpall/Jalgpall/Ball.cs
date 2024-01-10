using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jalgpall
{
    public class Ball
    {
        public double X { get; private set; } // Позиции мяча x
        public double Y { get; private set; } // Позиции мяча y

        private double _vx, _vy; // скорость/дистанция 

        public string sym { get; } = "||";

        private Game _game; // Связь мяча с игрой

        //Конструктор 
        public Ball(double x, double y, Game game) //Присваивание мяча к игре, и определение координат
        {
            _game = game;
            X = x;
            Y = y;
        }

        public void SetSpeed(double vx, double vy) // Установка скорости 
        {
            _vx = vx;
            _vy = vy;
        }

        public void Move() // Движение мяча
        {
            double newX = X + _vx;
            double newY = Y + _vy;
            if (_game.Stadium.IsIn(newX, newY))
            {
                X = newX;
                Y = newY;
            }
            else
            {
                _vx = 0;
                _vy = 0;
            }
        }
    }
}
