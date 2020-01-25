using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notebook
{
    public struct Row
    {
        /// <summary>
        /// Для идентификации конкретного экземпляра в дальнейшей работе
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Тема 
        /// </summary>
        public string Theme { get; set; }
        /// <summary>
        /// Текст 
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Приоритет 
        /// </summary>
        public int Priority { get; set; }
        /// <summary>
        /// Дедлайн 
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Дата и время создания
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// Задача выполнена?
        /// </summary>
        public bool ToDo { get; set; }
        /// <summary>
        /// Цвет задачи
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// Когда нужно выполнить задачу относительно текущего времени
        /// </summary>
        public string DateForUser
        {
            get { return GetDateForUser(Date); }
        }
        /// <summary>
        /// Цвет отражающий просрочена ли задача
        /// </summary>
        public string BGcolorUserDate
        {
            get
            {
                return Math.Round(Date.Subtract(DateTime.Now).TotalDays) < 0 ? "#c0392b" : "#27ae60";
            }
        }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <param name="Theme">Тема</param>
        /// <param name="Text">Текст</param>
        /// <param name="Priority">Приоритет</param>
        /// <param name="Date">Дедлайн</param>
        /// <param name="Created">Дата создания</param>
        /// <param name="Color">Цвет</param>
        /// <param name="ToDo">Задача выполнена?</param>
        public Row(int id, string Theme, string Text, int Priority, DateTime Date, DateTime Created, string Color, bool ToDo)
        {
            this.Id = id;
            this.Theme = Theme;
            this.Text = Text;
            this.Priority = Priority;
            this.Date = Date;
            this.Created = Created;
            this.Color = Color;
            this.ToDo = ToDo;
        }
        /// <summary>
        /// Возвращает представление даты и времени относительно текущего, интуитвно понятные юзверю
        /// </summary>
        /// <param name="Date">Объект DataTime</param>
        /// <returns></returns>
        private static string GetDateForUser(DateTime Date)
        {
            var diff = Date.Subtract(DateTime.Now);
            if (Math.Round(diff.TotalDays) == 0)
            {
                return " через " + Date.ToShortTimeString();
            }
            else
            {
                bool isPast = false;
                double ModDiff = Math.Round(diff.TotalDays);
                if (diff.TotalDays < 0)
                {
                    isPast = true;
                    ModDiff = Math.Round(diff.TotalDays * -1);
                }
                if (ModDiff > 365)
                {
                    var years = Math.Round(ModDiff / 365);
                    if (years == 1)
                    {
                        return isPast ? "год назад" : "через год";
                    }
                    else
                    {
                        var a = years < 5 ? "лет" : "года";
                        return isPast ? $"{years} {a} назад" : $"через {years} {a}";
                    }
                }
                else if (ModDiff > 30)
                {
                    var mouths = Math.Round(ModDiff / 30);
                    if (mouths == 1)
                    {
                        return isPast ? "месяц назад" : "через месяц";
                    }
                    else
                    {
                        var a = mouths < 5 ? "месяца" : "месяцев";
                        return isPast ? $"{mouths} {a} назад" : $"через {mouths} {a}";
                    }
                }
                else if (ModDiff > 7)
                {
                    var weeks = Math.Round(diff.TotalDays / 7);
                    if (weeks == 1)
                    {
                        return isPast ? "неделю назад" : "через неделю";
                    }
                    else
                    {
                        return isPast ? $"{weeks} недели назад" : $"через {weeks} недели";
                    }
                }
                else
                {
                    if (ModDiff == 1)
                    {
                        return isPast ? "вчера" : "завтра";
                    }
                    else if (ModDiff == 2)
                    {
                        return isPast ? "позавчера" : "послезавтра";
                    }
                    else
                    {
                        return isPast ? $"{ModDiff} дня назад" : $"через {ModDiff} дня";
                    }
                }
            }
        }
    }
}
