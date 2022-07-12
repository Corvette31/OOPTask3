using System;
using System.Collections.Generic;

namespace OOPTask3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Database players = new Database();
            bool isRun = true;

            CreatePlayers(players);

            while (isRun)
            {
                Console.WriteLine("Работа с базой данны, команды:\n1 - Вывести список игроков\n2 - Добавить игрока\n3 - Забанить игрока\n4 - Разбанить игрока\n5 - Удалить игрока\n6 - Выход\n");
                Console.Write("Введите команду: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        players.ShowDatabase();
                        break;
                    case "2":
                        AddPlayer(players);
                        break;
                    case "3":
                        SetBanPlayer(players, true);
                        break;
                    case "4":
                        SetBanPlayer(players, false);
                        break;
                    case "5":
                        DeletePlayer(players);
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

        static private void CreatePlayers(Database players)
        {
            Random random = new Random();
            string[] nickNames = { "Носок судьбы", "СвяТой_ТапоК", "3Jlou_4uTep", "KoTuk", "Высоковольтный Майонез" };
            int maxLevel = 100;

            for (int i = 0; i < nickNames.Length; i++)
            {
                Player player = new Player(nickNames[i], random.Next(maxLevel));
                players.AddPlayer(player);
            }
        }

        static void AddPlayer(Database players)
        {
            int level;

            Console.Write("Введите ник игрока: ");
            string nickName = Console.ReadLine();

            Console.Write("Введите уровень игрока: ");

            if (int.TryParse(Console.ReadLine(), out level) == false )
            {
                Console.WriteLine("Не корректное значение!");
                return;
            }

            players.AddPlayer(new Player(nickName, level));
        }

        static void DeletePlayer(Database players)
        {
            int uniqueNumber;

            Console.Write("Введите номер игрока которого нужно удалить: ");

            if (int.TryParse(Console.ReadLine(), out uniqueNumber) == false)
            {
                Console.WriteLine("Не корректное значение!");
                return;
            }

            players.DeletePlayer(uniqueNumber);
        }

        static void SetBanPlayer(Database players, bool ban)
        {
            int uniqueNumber;
            string messageBan = ban ? "забанить" : "разбанить";

            Console.Write($"Введите номер игрока которого нужно {messageBan}: ");

            if (int.TryParse(Console.ReadLine(), out uniqueNumber) == false)
            {
                Console.WriteLine("Не корректное значение!");
                return;
            }

            players.SetBan(uniqueNumber, ban);
        }
    }

    class Player
    {
        public int _uniqueNumber { get; private set; }
        private string _nickName;
        private int _level;
        private bool _isBanned;

        public Player(string nickName, int level)
        {
            _uniqueNumber = 0;
            _nickName = nickName;
            _level = level;
            _isBanned = false;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Игрок - {_nickName} , Уникальный номер - {_uniqueNumber} , Уровень - {_level} , Бан - {_isBanned}");
        }

        public void AssignUniqueNumber(int number)
        {
            _uniqueNumber = number;
        }

        public void SetBan(bool banned)
        {
            _isBanned = banned;
        }
    }

    class Database
    {
        private int _index;
        private List<Player> _players;

        public Database()
        {
            _index = 0;
            _players = new List<Player>();
        }

        public void AddPlayer(Player player)
        {
            _index++;
            _players.Add(player);
            player.AssignUniqueNumber(_index);
        }

        public void DeletePlayer(int uniqueNumber)
        {
            var player = GetPlayer(uniqueNumber);
            if (_players.Contains(player))
            {
                _players.Remove(player);
            }
            else
            {
                Console.WriteLine("Угрок не найден!");
            }
        }

        public void SetBan(int uniqueNumber, bool ban)
        {
            var player = GetPlayer(uniqueNumber);
            if (_players.Contains(player))
            {
                player.SetBan(ban);
            }
            else
            {
                Console.WriteLine("Угрок не найден!");
            }
        }

        public void ShowDatabase()
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
                if (player._uniqueNumber == uniqueNumber)
                {
                    return player;
                }
            }

            return null;
        }
    }
}
