using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notebook
{
    public class NotebookEngine 
    {
        private int ID = 0;
        /// <summary>
        /// Коллекция элементов определяющих задачи
        /// </summary>
        private readonly ObservableCollection<Row> _Rows = new ObservableCollection<Row>();
        /// <summary>
        /// Копия коллекции для доступа из визуал модели
        /// </summary>
        public readonly ReadOnlyObservableCollection<Row> PublicRows;
        /// <summary>
        /// Экземпляр класса для экспорта/импорта данных
        /// </summary>
        DataSaver dataSaver = new DataSaver();
        /// <summary>
        /// Конструктор, подгружает данные из файла data.json
        /// </summary>
        public NotebookEngine()
        {
            // Пробуем загрузить данные из файла, проверяем ошибки и добавляем загруженные строки в память
            if (!dataSaver.LoadData())
            {
                if (dataSaver.TextError == "Файл не сущетсвует")
                {
                    dataSaver.CreateFile();
                    //return;
                }
                else if (dataSaver.TextError == "Пустой файл")
                {
                    //return;
                }
                else
                {
                    // !!!!!!!
                    Debug.WriteLine(">>>>>>>" + dataSaver.TextError);
                    throw new Exception("Ошибка инициализации");
                    // !!!!!!!
                }
            }
            else
            {
                foreach (var item in dataSaver.Data) Add(item);
                Debug.WriteLine(">>>>>>>есть какие-то данные " + dataSaver.Data.Count);
            }
            PublicRows = new ReadOnlyObservableCollection<Row>(_Rows);
            OnPropertyChanged("PublicRows");
        }
        /// <summary>
        /// Добавление задачи
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public bool Add(Row row)
        {
            row.Id = ID++;
            if (row.Created == new DateTime()) row.Created = DateTime.Now;
            _Rows.Add(row);
            OnPropertyChanged("PublicRows");
            return true;
        }
        /// <summary>
        /// Добавление задачи отдельно по полям
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <param name="Theme">Тема</param>
        /// <param name="Text">Текст</param>
        /// <param name="Priority">Приоритет</param>
        /// <param name="Date">Дедлайн</param>
        /// <param name="Created">Дата создания</param>
        /// <param name="Color">Цвет</param>
        /// <param name="ToDo">Задача выполнена?</param>
        /// <returns></returns>
        //Здесь может быть ошибка связанная с Created = new DateTime()? стандартным занчением даты и времени будет не время вызова метода, а время компиляции?
        public bool Add(string Theme, string Text, int Priority, DateTime Date, string Color, DateTime Created = new DateTime(), bool ToDo = false)
        {
            if (Created == new DateTime()) Created = DateTime.Now;
            _Rows.Add(new Row(ID++, Theme, Text, Priority, Date, Created, Color,false));
            OnPropertyChanged("PublicRows");
            return true;
        }
        /// <summary>
        /// Сохранят текущее состояние задач в файл data.json
        /// </summary>
        public void Save()
        {
            dataSaver.Save(_Rows);
        }
        /// <summary>
        /// Удаляет строку из массива задач
        /// </summary>
        /// <param name="row">Экземпляр который нужно удалить</param>
        public void Remove (Row row)
        {
            _Rows.Remove(row);
            OnPropertyChanged("PublicRows");
        }
        public void Edit(Row row,Row newRow)
        {
            _Rows[_Rows.IndexOf(row)] = newRow;
            OnPropertyChanged("PublicRows");
        }

        /// <summary>
        /// Событие измнения данных
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Проброс уведомления в основной класс
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
