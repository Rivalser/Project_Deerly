using System;

class Program
{
    static void Main(string[] args)
    {
        EnemyAI enemyAI = new EnemyAI();

        bool gameIsRunning = true;
        while (gameIsRunning)
        {
            // Játékos és ellenség aktuális pozíciójának és életpontjának frissítése
            int playerPosition = GetPlayerPosition();
            int enemyPosition = GetEnemyPosition();
            int enemyHP = GetEnemyHP();

            // Távolság kiszámítása a játékos és az ellenség között
            int distanceToPlayer = Math.Abs(playerPosition - enemyPosition);

            // Az aktuális játékállapot
            int[] gameState = { enemyHP, distanceToPlayer };

            // Ellenség legjobb lépésének meghatározása
            int bestMove = enemyAI.FindBestMove(gameState);

            // Cselekvés végrehajtása
            if (bestMove == 1 && enemyHP > 3)
                EnemyAttack();
            else
                EnemyRetreat();

            // Események frissítése és további játéklogika...
            System.Threading.Thread.Sleep(100);  // Kis szünet a ciklusban
        }
    }

    // Játékos pozíció lekérdezése
    static int GetPlayerPosition()
    {
        // Játékos pozíciójának lekérdezése (például karakter koordinátája)
        return 5;  // Példaérték
    }

    // Ellenség pozíció lekérdezése
    static int GetEnemyPosition()
    {
        // Ellenség pozíciójának lekérdezése (például AI karakter koordinátája)
        return 10;  // Példaérték
    }

    // Ellenség életpont lekérdezése
    static int GetEnemyHP()
    {
        // Az ellenség aktuális életpontjának lekérdezése
        return 6;  // Példaérték
    }

    // Ellenség támadása
    static void EnemyAttack()
    {
        Console.WriteLine("Ellenség támad!");
    }

    // Ellenség visszavonulása
    static void EnemyRetreat()
    {
        Console.WriteLine("Ellenség visszavonul.");
    }
}

// AI osztály az ellenség számára
class EnemyAI
{
    const int AttackRange = 5;  // Hatótáv a játékos támadásához

    // Minimax algoritmus
    public int Minimax(int[] state, int depth, bool isMaximizing)
    {
        int hp = state[0];               // Ellenség életpontja
        int distanceToPlayer = state[1];  // Játékos távolsága

        // Ha az ellenség életpontja 3 vagy kevesebb, ne támadjon
        if (hp <= 3)
            return -10;  // Alacsony érték: visszavonulás

        // Maximális lépés (AI támadás)
        if (isMaximizing)
        {
            if (distanceToPlayer <= AttackRange)
                return 10;  // Támadási érték (ha a játékos hatótávolságon belül van)

            return -10;  // Visszavonulás, ha a játékos túl messze van
        }
        // Minimális lépés (visszavonulás)
        else
        {
            if (distanceToPlayer > AttackRange)
                return -10;  // Visszavonulási érték
            return 10;  // Támadás, ha a játékos közel van
        }
    }

    // Legjobb lépés keresése az ellenség számára
    public int FindBestMove(int[] state)
    {
        int bestMove = -1;
        int bestValue = int.MinValue;

        // Két lehetőség: támadás (1) vagy visszavonulás (0)
        for (int move = 0; move < 2; move++)
        {
            int hp = state[0];
            int distance = state[1];

            // Támadás opció
            if (move == 1)
            {
                int moveValue = Minimax(new int[] { hp, distance }, 0, true);
                if (moveValue > bestValue)
                {
                    bestMove = move;
                    bestValue = moveValue;
                }
            }
            // Visszavonulás opció
            else
            {
                int moveValue = Minimax(new int[] { hp, distance }, 0, false);
                if (moveValue > bestValue)
                {
                    bestMove = move;
                    bestValue = moveValue;
                }
            }
        }
        return bestMove;
    }
}