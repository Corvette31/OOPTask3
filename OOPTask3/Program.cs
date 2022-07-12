using System;
using System.Collections.Generic;

namespace OOPTask3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Database dataBase = new Database();
            bool isRun = true;

            while (isRun)
            {
                Console.WriteLine("Работа с базой данны, команды:\n1 - Вывести список игроков\n2 - Добавить игрока\n3 - Забанить игрока\n4 - Разбанить игрока\n5 - Удалить игрока\n6 - Выход\n");
                Console.Write("Введите команду: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        dataBase.ShowTable();
                        break;
                    case "2":
                        dataBase.AddPlayer();
                        break;
                    case "3":
                        dataBase.BanPlayer();
                        break;
                    case "4":
                        dataBase.UnbanPlayer();
                        break;
                    case "5":
                        dataBase.DeletePlayer();
                        break;
                    case "6":
                        isRun = false;
                        break;
                    default:
                        Console.WriteLine("Не известная команда!");
                        break;
                }

                Console.Write("\nНажмите любую клавишу для продолжения: ");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }

    class Player
    {
        private string _nickName;
        private int _level;
        private bool _isBanned;

        public int UniqueNumber { get; private set; }

        public Player(string nickName, int level, int uniqueNumber)
        {
            UniqueNumber = uniqueNumber;
            _nickName = nickName;
            _level = level;
            _isBanned = false;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Игрок - {_nickName} , Уникальный номер - {UniqueNumber} , Уровень - {_level} , Бан - {_isBanned}");
        }

        public void Ban()
        {
            _isBanned = true;
        }

        public void Unban()
        {
            _isBanned = false;
        }
    }

    class Database
    {
        private int _indexNextPlayer;
        private List<Player> _players;

        public Database()
        {
            _indexNextPlayer = 0;
            _players = new List<Player>();
            Random random = new Random();
            string[] nickNames = { "Носок судьбы", "СвяТой_ТапоК", "3Jlou_4uTep", "KoTuk", "Высоковольтный Майонез" };
            int maxLevel = 100;

            for (int i = 0; i < nickNames.Length; i++)
            {
                _indexNextPlayer++;
                _players.Add(new Player(nickNames[i], random.Next(maxLevel), _indexNextPlayer));
            }
        }

        public void AddPlayer()
        {
            Console.Write("Введите ник игрока: ");
            string nickName = Console.ReadLine();

            Console.Write("Введите уровень игрока: ");
            int level = GetNuberFromUser();

            _indexNextPlayer++;
            _players.Add(new Player(nickName, level, _indexNextPlayer));
            Console.WriteLine("Игрок добавлен");
        }

        public void DeletePlayer()
        {
            Console.Write("Введите номер игрока которого нужно удалить: ");
            int uniqueNumber = GetNuberFromUser();

            var player = GetPlayer(uniqueNumber);
            if (player != null)
            {
                _players.Remove(player);
                Console.WriteLine("Игрок удален!");
            }
            else
            {
                Console.WriteLine("Игрок не найден!");
            }
        }

        public void BanPlayer()
        {
            Console.Write("Введите номер игрока которого нужно забанить: ");
            int uniqueNumber = GetNuberFromUser();

            Player player = GetPlayer(uniqueNumber);
            if (player != null)
            {
                player.Ban();
                Console.WriteLine($"Игрок забанен!");
            }
            else
            {
                Console.WriteLine("Игрок не найден!");
            }
        }

        public void UnbanPlayer()
        {
            Console.Write("Введите номер игрока которого нужно разбанить: ");
            int uniqueNumber = GetNuberFromUser();

            Player player = GetPlayer(uniqueNumber);
            if (player != null)
            {
                player.Unban();
                Console.WriteLine($"Игрок разбанен!");
            }
            else
            {
                Console.WriteLine("Игрок не найден!");
            }
        }

        public void ShowTable()
        {
            foreach (var player in _players)
            {
                player.ShowInfo();
            }
        }

        private Player GetPlayer(int uniqueNumber)
        {
            foreach (var player in _players)
            {
                if (player.UniqueNumber == uniqueNumber)
                {
                    return player;
                }
            }

            return null;
        }

        private int GetNuberFromUser()
        {
            int uniqueNumber;

            if (int.TryParse(Console.ReadLine(), out uniqueNumber) == false)
            {
                Console.WriteLine("Не корректное значение!");
            }

            return uniqueNumber;
        }
    }
}
