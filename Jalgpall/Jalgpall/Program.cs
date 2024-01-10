
namespace Jalgpall 
{
    class Program 
    {
        static void Main() 
        {

            Team t1 = new Team("Home");

            Team t2 = new Team("Away");

            Stadium s = new Stadium(100, 30);

            t1.GenHTeam();

            t2.GenATeam();

            Game g = new Game(t1, t2, s);

            //g.GetPositionForAwayTeam(Stadium.Width, Stadium.Height);

            g.Start();
            while (true) 
            {
                Console.Clear();
                Console.SetCursorPosition(82,27);
                s.Draw();
                g.Move();
                Thread.Sleep(800);
            }
        }
    }
}
