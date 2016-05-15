# Snake
Ова е имплементација на играта "Snake" која што има можност за бирање на 4 Game modes и 3 различни тежини.Целта на играта е да се направи што е можно повеќе поени така што има освен основната храна има и специјална којашто таа носи повеќе поени и притоа да не се удиш во ѕидовите или блоковите додека се одбере Blocks модот или да се самоуништите.
Поените се добиваат така што нормалната храна носи 1 поен додека специјалната храна која се појавува повремено носи 10 поени.

При стартување на играта може да изберете вид на игра и тежина на играта.

За имплементација на играта се користат следните класи од кои:
<ul>
  <li>Block</li>
  Во Block класата се чуваат информации за блоковите кои се исцртуваат доколку се одбере Blocks модот
  
  <li>Game</li>
  Класата Game ни е апстрактна класа и чуваме информации за играта а ни служи така што од неа наследуваат 4 класи од кои
  <ul>
    <li>Blocks</li>
    Класата Blocks креира игра Blocks и служи како да се генерира мапата. 
    <li>ClassicGame</li>
    ClassicGame класата ни е за креирање на нормална игра на змијата која завршува со удирање во ѕид или самоунишрување.
    <li>Invisible</li>
    Invisible ни е класа која што прави игра каде змијата ни е невидлива а со помош на координатите кои се прикажуваат на долната страна од формата играчот се ориентира.
    <li>TimeAttack</li>
    TimeAttack е класа за креирање игра како нормалната но така што кога ќе истече времето играта завршува.
  </ul>
  <li>Food</li>
  Класата Food е апстрактна класа и во неа се чуваат информации за позицијата на храната и формата.
  А од класата Food наследуваат NormalFood и SpecialFood.
  <ul>
    <li>NormalFood</li>
    Со користење на NormalFood класата се креира нормална храна во форма на црна коцка и носи 1 поен.
    <li>SpecialFood</li>
    Со користење на SpecialFood класата се креира специјална храна во форма на јаболко и носи 10 поени.
  </ul>
  <li>Game</li>
  Во класата Game се чува Snake која што ни претставува змијата ,листа со Food којашто ни служи за чување на храната и листа со Blocks која ја користиме доколку се одбере модот Blocks.
  <li>Part</li>
  Во класата Part се чуваат информации за позицијата на секој дел од змијата ,боја и ширина.
  <li>Snake</li>
  Класата Snake ни ја претставува змијата и во неа се чуваа лиса со Part за да се формира змија и листа со int во која се чува во која насока секој од Part треба да се помести.
  <li>Form2</li>
    Form2 класата ни служи за да одредеме каква игра да креираме и со која тежина.
  <li>Form1</li>
  Во главната класа се чува објект од класата Game која ни ја претставува играта и со помош на неа се контролира играта.
</ul>
```c#
public bool eat(Snake.Direction last)
        {
            Part p = null;
            bool flag = false;
            bool add = false;
            double dis = 50;
            if (food.Count != 0)
                dis = getDistance(new Point(snake.firstPart.getX(), snake.firstPart.getY()), new Point(food.First().getX(), food.First().getY()));
            if (type != GameType.Invisible.ToString())

            {
                if (dis < 10)
                {
                    snake.setColor(Color.Green);
                }
                else if (dis >= 10 && dis < 20)
                {
                    snake.setColor(Color.GreenYellow);
                }
                else if (dis >= 20 && dis < 30)
                {
                    snake.setColor(Color.Yellow);
                }
                else if (dis >= 30 && dis < 40)
                {
                    snake.setColor(Color.YellowGreen);
                }
                else
                {
                    snake.setColor(Color.Red);
                }
            }
            Food f = new NormalFood(snake.firstPart.getX(), snake.firstPart.getY());
            Food s = new SpecialFood(snake.firstPart.getX(), snake.firstPart.getY());
            if (food.Contains(f))
            {
                score++;
                food.Remove(f);
                p = new Part(snake.lastPart.getX(), snake.lastPart.getY(), snake.boja);
                generateFood();
                add = true;
            }
            else if (food.Contains(s))
            {
                specialFood = false;
                food.Remove(s);
                score += 10;
                flag = true;
            }
            snake.move(last);
            if (add)
            {
                snake.AddPart(p);
                add = false;
            }
            return flag;
        }
      ```
Со овој метод змијата се мести и доколку изеде нормална храна се додава нов дел на крајот од змијата а доколку изеде специјална храна не се додава ништо само поените се менуваат.Исто така во зависност од растојанието на главата на змијата и нормалната храна бојата на змијата се менува.

