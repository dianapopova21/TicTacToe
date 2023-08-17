using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Models;

namespace TicTacToe.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public static GameModel model; 
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            model = new GameModel();
        }

        [HttpGet]
        public IActionResult Index(string id) => View();

        [Route("Home/GetInfo/{a1?}/{a2?}/{a3?}/{b1?}/{b2?}/{b3?}/{c1?}/{c2?}/{c3?}")]
        public IActionResult GetInfo(string a1, string a2, string a3, string b1, string b2, string b3, string c1, string c2, string c3, GameModel model)
        {
            //1. Сохраняем в лист полученые данные с игрового поля
            //2. Создаем 8 масивов с выигрышними комбинациями и объединяем в один двумерный
            //3. С помощью цикла проверяем двумерный масив на наличие выигр. комбинации
            //4. С помощью проверки ищем подходящий ход для компьютера или при помощи цикла while делаем случайный
            //5. Вносим в лист и передаем его на игровое поле

            List<string> list = new List<string>() { a1, a2, a3, b1, b2, b3, c1, c2, c3 };
            string[] win1 = { a1, a2, a3 };
            string[] win2 = { b1, b2, b3 };
            string[] win3 = { c1, c2, c3 };

            string[] win4 = { a1, b1, c1 };
            string[] win5 = { a2, b2, c2 };
            string[] win6 = { a3, b3, c3 };

            string[] win7 = { a1, b2, c3 };
            string[] win8 = { a3, b2, c1 };

            string[][] winComb = { win1, win2, win3, win4, win5, win6, win7, win8 };

            foreach (string[] e in winComb)
            {
                // определяем вариант победы - три крестика или нолика в ряд
                int compare_1 = string.Compare(e[0], e[1]);
                int compare_2 = string.Compare(e[1], e[2]);
                int compare_3 = string.Compare(e[0], e[2]);
                if (compare_1 == 0
                    && compare_2 == 0
                    && compare_3 == 0
                    && !String.IsNullOrEmpty(e[0])
                    && !String.IsNullOrEmpty(e[1])
                    && !String.IsNullOrEmpty(e[2]))
                {
                    list.Add($"Победа {e[0]}");
                    if(list[9] == "Победа ❎") ViewBag.Winner = "Победа крестиков";
                    else if (e[0] == "⭕") ViewBag.Winner = "Победа ноликов";
                    return Json(list);
                }
                int status = 0;
                foreach(string l in list)
                {
                    if (l == null) status++;
                }
                if(status == 0)
                {
                    list.Add("Ничья");
                    return Json(list);
                }
            }

            //проверка по горизонтали а
            if (a1 == "⭕" && a2 == "⭕" && a3 == null) list[2] = "⭕";
            else if (a1 == "⭕" && a2 == null && a3 == "⭕") list[1] = "⭕";
            else if (a1 == null && a2 == "⭕" && a3 == "⭕") list[0] = "⭕";

            else if (b1 == "⭕" && b2 == "⭕" && b3 == null) list[5] = "⭕";
            else if (b1 == "⭕" && b2 == null && b3 == "⭕") list[4] = "⭕";
            else if (b1 == null && b2 == "⭕" && b3 == "⭕") list[3] = "⭕";

            else if (c1 == "⭕" && c2 == "⭕" && c3 == null) list[8] = "⭕";
            else if (c1 == "⭕" && c2 == null && c3 == "⭕") list[7] = "⭕";
            else if (c1 == null && c2 == "⭕" && c3 == "⭕") list[6] = "⭕";

            else if (a1 == "⭕" && b1 == "⭕" && c1 == null) list[6] = "⭕";
            else if (a1 == "⭕" && b1 == null && c1 == "⭕") list[3] = "⭕";
            else if (a1 == null && b1 == "⭕" && c1 == "⭕") list[0] = "⭕";

            else if (a2 == "⭕" && b2 == "⭕" && c2 == null) list[7] = "⭕";
            else if (a2 == "⭕" && b2 == null && c2 == "⭕") list[4] = "⭕";
            else if (a2 == null && b2 == "⭕" && c2 == "⭕") list[1] = "⭕";

            else if (a3 == "⭕" && b3 == "⭕" && c3 == null) list[8] = "⭕";
            else if (a3 == "⭕" && b3 == null && c3 == "⭕") list[5] = "⭕";
            else if (a3 == null && b3 == "⭕" && c3 == "⭕") list[2] = "⭕";

            else if (a1 == "⭕" && b2 == "⭕" && c3 == null) list[8] = "⭕";
            else if (a1 == "⭕" && b2 == null && c3 == "⭕") list[4] = "⭕";
            else if (a1 == null && b2 == "⭕" && c3 == "⭕") list[0] = "⭕";

            else if (a3 == "⭕" && b2 == "⭕" && c1 == null) list[6] = "⭕";
            else if (a3 == "⭕" && b2 == null && c1 == "⭕") list[4] = "⭕";
            else if (a3 == null && b2 == "⭕" && c1 == "⭕") list[2] = "⭕";

            // если в линии два крестика, компьютер ставит нолик в третье поле этой линии
            else if (a1 == "❎" && a2 == "❎" && a3 == null) list[2] = "⭕";
            else if (a1 == "❎" && a2 == null && a3 == "❎") list[1] = "⭕";
            else if (a1 == null && a2 == "❎" && a3 == "❎") list[0] = "⭕";

            else if (b1 == "❎" && b2 == "❎" && b3 == null) list[5] = "⭕";
            else if (b1 == "❎" && b2 == null && b3 == "❎") list[4] = "⭕";
            else if (b1 == null && b2 == "❎" && b3 == "❎") list[3] = "⭕";

            else if (c1 == "❎" && c2 == "❎" && c3 == null) list[8] = "⭕";
            else if (c1 == "❎" && c2 == null && c3 == "❎") list[7] = "⭕";
            else if (c1 == null && c2 == "❎" && c3 == "❎") list[6] = "⭕";

            else if (a1 == "❎" && b1 == "❎" && c1 == null) list[6] = "⭕";
            else if (a1 == "❎" && b1 == null && c1 == "❎") list[3] = "⭕";
            else if (a1 == null && b1 == "❎" && c1 == "❎") list[0] = "⭕";

            else if (a2 == "❎" && b2 == "❎" && c2 == null) list[7] = "⭕";
            else if (a2 == "❎" && b2 == null && c2 == "❎") list[4] = "⭕";
            else if (a2 == null && b2 == "❎" && c2 == "❎") list[1] = "⭕";

            else if (a3 == "❎" && b3 == "❎" && c3 == null) list[8] = "⭕";
            else if (a3 == "❎" && b3 == null && c3 == "❎") list[5] = "⭕";
            else if (a3 == null && b3 == "❎" && c3 == "❎") list[2] = "⭕";

            else if (a1 == "❎" && b2 == "❎" && c3 == null) list[8] = "⭕";
            else if (a1 == "❎" && b2 == null && c3 == "❎") list[4] = "⭕";
            else if (a1 == null && b2 == "❎" && c3 == "❎") list[0] = "⭕";

            else if (a3 == "❎" && b2 == "❎" && c1 == null) list[6] = "⭕";
            else if (a3 == "❎" && b2 == null && c1 == "❎") list[4] = "⭕";
            else if (a3 == null && b2 == "❎" && c1 == "❎") list[2] = "⭕";

            //**************************************************************
            else
            {
                Random rnd = new Random();
                while (true)
                {
                    int rand = rnd.Next(0, 8);

                    if (list[rand] == null)
                    {
                        list[rand] = "⭕"; break;
                    }
                }
            }

            win1[0] = list[0]; win1[1] = list[1]; win1[2] = list[2];   // 1
            win2[0] = list[3]; win2[1] = list[4]; win2[2] = list[5];   // 2
            win3[0] = list[6]; win3[1] = list[7]; win3[2] = list[8];   // 3

            win4[0] = list[0]; win4[1] = list[3]; win4[2] = list[6];   // 4
            win5[0] = list[1]; win5[1] = list[4]; win5[2] = list[7];   // 5
            win6[0] = list[2]; win6[1] = list[5]; win6[2] = list[8];   // 6

            win7[0] = list[0]; win7[1] = list[4]; win7[2] = list[8];   // 7
            win8[0] = list[2]; win8[1] = list[4]; win8[2] = list[6];   // 8

            string[][] result = { win1, win2, win3, win4, win5, win6, win7, win8 };

            foreach (string[] e in result)
            {
                // определяем вариант победы - три крестика или нолика в ряд
                int compare_1 = string.Compare(e[0], e[1]);
                int compare_2 = string.Compare(e[1], e[2]);
                int compare_3 = string.Compare(e[0], e[2]);
                if (compare_1 == 0
                    && compare_2 == 0
                    && compare_3 == 0
                    && !String.IsNullOrEmpty(e[0])
                    && !String.IsNullOrEmpty(e[1])
                    && !String.IsNullOrEmpty(e[2]))
                {
                    //сделать окно
                    list.Add($"Победа {e[0]}");
                    if (list[9] == "Победа ❎");
                    else if (e[0] == "⭕");
                    return Json(list);
                }
                int status = 0;
                foreach (string l in list)
                {
                    if (l == null) status++;
                }
                if (status == 0)
                {
                    list.Add("Ничья");
                    return Json(list);
                }
            }

            return Json(list);
        }

        public IActionResult Game(string id) => View();

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
