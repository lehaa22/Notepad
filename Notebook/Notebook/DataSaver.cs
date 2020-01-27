using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace Notebook
{
    /// <summary>
    /// Работа с текстовым файлом как с файлом сохранения данных
    /// </summary>
    public class DataSaver
    {
        /// <summary>
        /// Хранит текст последней ошибки связанной с сохранением данных
        /// </summary>
        public string TextError { get; set; }
        /// <summary>
        /// Хранит последнюю ошибку связанную с сохранением данных
        /// </summary>
        public Exception Ex { get; set; }
        /// <summary>
        /// Хранит последние данные которые были прочтены из файла 
        /// </summary>
        public ObservableCollection<Row> Data { get; set; }
        /// <summary>
        /// Содержит путь до текущего файла в котром хранятся все данные о задачах созданных юзером
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="fileName">Путь до файла в котором хранятся данные</param>
        public DataSaver(string fileName)
        {
            this.FileName = fileName;
        }
        /// <summary>
        /// Конструктор без параметров, путь до файла будет - ./Data.json
        /// </summary>
        public DataSaver()
        {
            this.FileName = System.IO.Directory.GetCurrentDirectory() + "\\Data.json";
        }
        /// <summary>
        /// Читает данные о задачах из файла который This.FileName
        /// </summary>
        /// <returns>true - если данные прочитаны, false - если ошибка или файл пустой</returns>
        public bool LoadData()
        {
            if (File.Exists(FileName))
            {
                string D = File.ReadAllText(FileName);
                // Если файл пустой - записываем ошибку об этом и возвращаем false
                if (D.Length == 0)
                {
                    TextError = "Пустой файл";
                    return false;
                }
                else
                {
                    // !!!! Я уже работал с этой библиотекой и пока не нашел как без обработчика исключений ловить ошибки дессериализации - например если файл окажется битым
                    try
                    {
                        Data = JsonConvert.DeserializeObject<ObservableCollection<Row>>(D);
                    }
                    catch (Exception e)
                    {
                        TextError = e.Message;
                        Ex = e.InnerException;
                        return false;
                        //throw;
                    }
                }
            }
            else
            {
                TextError = "Файл не сущетсвует";
                return false;
            }
            return true;
        }
        /// <summary>
        /// Сохраняет данные в this.FileName в формате json
        /// </summary>
        /// <param name="Data"></param>
        public void Save(ObservableCollection<Row> Data)
        {
            File.WriteAllText(FileName, JsonConvert.SerializeObject(Data));
        }
        /// <summary>
        /// Создает файл this.FileName
        /// </summary>
        public void CreateFile()
        {
            using (File.Create(FileName)) { };
        }
    }
}
