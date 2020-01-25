using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;

namespace Notebook
{
    /// <summary>
    /// Логика взаимодействия для InterfaceAddRow.xaml
    /// </summary>
    public partial class InterfaceAddRow : Window
    {
        public InterfaceAddRow(Row row = default(Row))
        {
            InitializeComponent();
            // Для программного заполнения комбобокса, чтобы можно было позже выводить значение из экземпляра
            List<string> ColorList = new List<string> { "#ff95a5a6", "#ff1abc9c", "#ff2ecc71", "#ff9b59b6", "#ffecf0f1" };
            // Коллекция контролов, заполняем её прямоугольниками указанных цветов
            List<ComboBoxItem> ColorListItems = new List<ComboBoxItem>();
            foreach (var item in ColorList)
            {
                // Прямоугольник
                Rectangle rectangle = new Rectangle();
                // Конвертор цветов 
                var bc = new BrushConverter();
                // Конвертируем цвет, конвертируем и добавляем свойство фона
                rectangle.Fill = (Brush)bc.ConvertFrom(item);
                // Ширина
                rectangle.Width = 16;
                // Высота
                rectangle.Height = 15;
                // Создаем новый экземпляр контрола и добавляем в него наш прямоугольник
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = rectangle;
                // Без этого генерится ошибка связи данных в вывод 
                comboBoxItem.VerticalContentAlignment = VerticalAlignment.Center;
                comboBoxItem.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                // Добавляем в коллекцию полученный контрол
                ColorListItems.Add(comboBoxItem);
            }
            // Связываем коллекцию контролов с комбобоксом
            ColorItem.ItemsSource = ColorListItems;
            // Если конструктор вызван с параметром - выводим данные из полученного экземпляра
            if (!row.Equals(default(Row)))
            {
                Theme.Text = row.Theme;
                Text.Text = row.Text;
                Priority.SelectedIndex = row.Priority-1;
                var a = row.Color.ToLower();
                var b = ColorList.IndexOf(row.Color.ToLower());
                ColorItem.SelectedIndex = ColorList.IndexOf(row.Color.ToLower());
                SelectDate.SelectedDate = row.Date;
                TimeH.SelectedIndex = row.Date.Hour;
                TimeM.SelectedIndex = row.Date.Minute/15;
            }
            // Если конструктор вызван без параметра - указываем стандартные значения для контролов, для которых они не указаны во view
            else
            {
                SelectDate.SelectedDate = DateTime.Now;
            }
            
        }
        /// <summary>
        /// Поле в котором хранится прочитанный из полей экзепляр типа Row
        /// </summary>
        public Row NewRow;
        /// <summary>
        /// Флаг, указывающий получены ли данные, для события windowClosing
        /// </summary>
        private bool DataGet = false;
        /// <summary>
        /// Обработка событий завершения работы с формой. В случае корректно заполненных полей - запишет в NewRow экземпляр считанный с контролов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AcceptClick(object sender, RoutedEventArgs e)
        {
            string theme = Theme.Text;
            if (theme == "") return;
            string text = Text.Text;
            if (text == "") return;

            var ComboBoxItem = (ComboBoxItem)Priority.SelectedItem;
            if (ComboBoxItem == null) return;
            int.TryParse(ComboBoxItem.Content.ToString(), out int priority);


            ComboBoxItem = (ComboBoxItem)ColorItem.SelectedItem;
            if (ComboBoxItem == null) return;
            var rectangle = (Rectangle)ComboBoxItem.Content;
            String color = rectangle.Fill.ToString();
            Debug.WriteLine(color);
            DateTime Date = (DateTime)SelectDate.SelectedDate;

            ComboBoxItem = (ComboBoxItem)TimeH.SelectedItem;
            if (ComboBoxItem == null) return;
            var hours = ComboBoxItem.Content.ToString();
            Date = Date.AddHours(double.Parse(hours));

            ComboBoxItem = (ComboBoxItem)TimeM.SelectedItem;
            if (ComboBoxItem == null) return;
            var minutes = ComboBoxItem.Content.ToString();
            Date = Date.AddMinutes(double.Parse(minutes));
            NewRow = new Row() {
                Text = text,
                Theme = theme,
                Priority = priority,
                Color = color,
                Date = Date
            };
            DataGet = true;
            this.DialogResult = true;
        }
        // Проще реализовать здесь чем в mvvm, не нашел способа отмены закрытия.
        /// <summary>
        /// Обработка закрытия и нажатия кнопки отмена
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DataGet) return;
            var A = MessageBox.Show("Уверены что хотите закрыть окно? Данные не будут сохранены.", "Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (A.ToString() == "Cancel") e.Cancel = true;
        }

    }
}
