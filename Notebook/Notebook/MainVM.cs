using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Notebook
{
    /// <summary>
    /// Главная визульная модель. Здесь описано взаимодействие интерфейса с моделью рассчетов.
    /// </summary>
    public class MainVM : INotifyPropertyChanged
    {
        /// <summary>
        /// Событие изменения значений и уведомление о необходимости отобразить новые данные
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Проброс уведомления в основной класс
        /// </summary>
        /// <param name="propertyName">Имя обновленного поля</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        /// <summary>
        /// Инциализируем модель рассчетов
        /// </summary>
        readonly NotebookEngine _notebookEngine = new NotebookEngine();
        /// <summary>
        /// Возвращает текущее состояние PublicRows
        /// </summary>
        /// Странно, что VS не видит ссылку из xamal
        public ReadOnlyObservableCollection<Row> ListRow => _notebookEngine.PublicRows;
        /// <summary>
        /// Делегат выхода из программы
        /// </summary>
        public DelegateCommand ExitCommand { get; }
        /// <summary>
        /// Делегат кнопки добавления новой строки в список дел
        /// </summary>
        public DelegateCommand AddCommand { get; }
        /// <summary>
        /// Делегат кнопки сортировки строк, принимает имя поля по которому нужно сортировать и ListBox
        /// </summary>
        public DelegateCommand<List<object>> SortCommand { get; }
        /// <summary>
        /// Делегат кнопки удаления строки, принимает экземпляр ROW
        /// </summary>
        public DelegateCommand<Row?> EditCommand { get; }
        /// <summary>
        /// Делегат кнопки редактирования задачи
        /// </summary>
        public DelegateCommand<Row?> RemoveCommand { get; }
        /// <summary>
        /// Конструктор
        /// </summary>
        public MainVM()
        {
            // Проброс уведомления в основной класс 
            _notebookEngine.PropertyChanged += (s, e) => OnPropertyChanged(e.PropertyName);
            #region Описываем делегаты команд вызываемых UI
            AddCommand = new DelegateCommand(() =>
            {
                // Создаем новое окно
                InterfaceAddRow interfaceAddRow = new InterfaceAddRow();
                // Открываем новое окно
                var F = interfaceAddRow.ShowDialog();
                // Если не отмена - добавляем строчку
                if (F.Value)
                {
                    _notebookEngine.Add(interfaceAddRow.NewRow);
                }
            });
            SortCommand = new DelegateCommand<List<object>>(list =>
            {
                // Первый параметр - поле, по которому нужна сортировка
                string target = (string)list[0];
                // Второй параметр - объект ListBox в котором сортируем
                ListBox listBox = (ListBox)list[1];
                // Если уже есть параметр сортировки и он с тем же полем - меняем 
                if (listBox.Items.SortDescriptions.Count != 0 && listBox.Items.SortDescriptions[0].PropertyName == target)
                {
                    var Dir = listBox.Items.SortDescriptions[0].Direction == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
                    listBox.Items.SortDescriptions.Clear();
                    listBox.Items.SortDescriptions.Add(new SortDescription(target, Dir));
                }
                else
                {
                    listBox.Items.SortDescriptions.Clear();
                    listBox.Items.SortDescriptions.Add(new SortDescription(target, ListSortDirection.Ascending));
                }
            });
            ExitCommand = new DelegateCommand(() =>
            {
                _notebookEngine.Save();
            });
            RemoveCommand = new DelegateCommand<Row?>(row =>
            {
                if (row != null)
                {
                    _notebookEngine.Remove((Row)row);
                }
                else
                {
                    MessageBox.Show("Не выбрана задача для удаления","Удаление задачи",MessageBoxButton.OK,MessageBoxImage.Error);
                }
            });
            EditCommand = new DelegateCommand<Row?>(row =>
            {

                if (row != null)
                {
                    InterfaceAddRow interfaceAddRow = new InterfaceAddRow((Row)row);
                    var F = interfaceAddRow.ShowDialog();
                    // Если не отмена - заменяем строчку
                    if (F.Value)
                    {
                        _notebookEngine.Edit((Row)row, interfaceAddRow.NewRow);
                    }
                }
                else
                {
                    MessageBox.Show("Не выбрана задача для удаления", "Удаление задачи", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
            #endregion
        }

    }
}
