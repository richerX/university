using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Slider_App
{
    public partial class SliderAppMainWindow : Form
    {
        // Введение основных настроек.
        Color curBackColor;
        Color curFontColor;
        List<string> openFiles = new List<string>();
        Font curTextFont;

        // Основная функция приложения.
        public SliderAppMainWindow()
        {
            InitializeComponent();
            // Задание границ окна.
            Size = new Size(1920, 1080);
            MinimumSize = Screen.PrimaryScreen.Bounds.Size / 2;
            MaximumSize = Screen.PrimaryScreen.Bounds.Size;
            // Фильтр диалога сохранения и сохранения шрифта.
            saveFileDialog.Filter = "Text File(*.txt)|*.txt|Rich Text File(*.rtf)|*.rtf";
            curTextFont = Font;
            // Горячие клавиши.
            NewButton.ShortcutKeys = Keys.Control | Keys.N;
            Open.ShortcutKeys = Keys.Control | Keys.O;
            Save.ShortcutKeys = Keys.Control | Keys.S;
            SaveAs.ShortcutKeys = Keys.Control | Keys.S | Keys.Shift;
            ExitButton.ShortcutKeys = Keys.Control | Keys.Q;
            // Чтение сохраненных настроек и открытие файлов.
            ReadSettingsFile();
            ReadOpenedFiles();
        }

        // Возвращает текущее текстовое поле.
        private RichTextBox GetCurTextBox()
        {
            TabPage tabPage = tabControl.SelectedTab;
            if (tabPage != null)
                return tabPage.Controls[0] as RichTextBox;
            return null;
        }

        // Автосохранение всех файлов.
        private void timerSave_Tick(object sender, EventArgs e)
        {
            foreach (TabPage tab in tabControl.TabPages)
            {
                // Проверка файла на существование.
                if (tab.Text.EndsWith(".txt") || tab.Text.EndsWith(".rtf"))
                {
                    var filepath = Path.GetFullPath($"{tab.Text}");
                    File.WriteAllText(filepath, GetCurTextBox().Text);
                    tab.Text = filepath;
                }
            }
            MessageBox.Show("Проведено автосохранение открытых файлов.\n(Файлы, которые ни разу не сохранялись, не автосохраняются.)");
        }

        // Чтение файла с настройками.
        private void ReadSettingsFile()
        {
            string[] lines = File.ReadAllLines("settings.txt");
            // Цвет фона.
            int[] array = Array.ConvertAll(lines[0].Split(), int.Parse);
            curBackColor = Color.FromArgb(array[0], array[1], array[2], array[3]);
            // Цвет шрифта.
            array = Array.ConvertAll(lines[1].Split(), int.Parse);
            curFontColor = Color.FromArgb(array[0], array[1], array[2], array[3]);
            // Таймер автосохранения.
            timerSave.Interval = int.Parse(lines[2]) * 60000;
            autoSaveTimeBox.Text = lines[2];
        }

        // Запись файла с настройками.
        private void WriteSettingsFile()
        {
            string[] lines = new string[] { GetStringColor(curBackColor), GetStringColor(curFontColor), autoSaveTimeBox.Text };
            File.WriteAllLines("settings.txt", lines);
        }

        // Чтение файла с настройками.
        private void ReadOpenedFiles()
        {
            var openFilesArray = File.ReadAllLines("files.txt");
            foreach (var file in openFilesArray)
            {
                try
                {
                    var tabPage = new TabPage(file);
                    // Создание нового текстового поля.
                    var richTextBox = new RichTextBox();
                    // Настройка текстового поля.
                    richTextBox.Dock = DockStyle.Fill;
                    richTextBox.ContextMenuStrip = contextMenuStrip;
                    richTextBox.BackColor = curBackColor;
                    richTextBox.ForeColor = curFontColor;
                    richTextBox.Text = File.ReadAllText(file);
                    // Добавление нового текстового поля.
                    tabPage.Controls.Add(richTextBox);
                    tabControl.TabPages.Add(tabPage);
                    openFiles.Add(file);
                }
                catch { }
            }
            WriteOpenedFiles();
        }

        // Запись файла с настройками.
        private void WriteOpenedFiles()
        {
            File.WriteAllLines("files.txt", openFiles);
        }

        // Превращает цвет в ARGB строку.
        private string GetStringColor(Color color)
        {
            return $"{color.A} {color.R} {color.G} {color.B}";
        }

        // Действия при закрытии формы.
        private void SliderAppMainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            WriteOpenedFiles();
            if (tabControl.TabPages.Count == 0 || !HaveChangedTabs())
                return;
            // Сохранение всех файлов.
            var res = MessageBox.Show("Выполнить сохранение?", "", MessageBoxButtons.YesNoCancel);
            if (res == DialogResult.Yes)
            {
                foreach (TabPage tab in tabControl.TabPages)
                {
                    RichTextBox curTextBox = tab.Controls[0] as RichTextBox;
                    // Сохранений текстов, у которых уже есть файл.
                    if (tab.Text.EndsWith(".txt") || tab.Text.EndsWith(".rtf"))
                    {
                        var filepath = Path.GetFullPath($"{tab.Text}");
                        File.WriteAllText(filepath, curTextBox.Text);
                        tab.Text = filepath;
                    }
                    // Сохранение текстов без файла.
                    else
                    {
                        if (saveFileDialog.ShowDialog() != DialogResult.Cancel)
                        {
                            File.WriteAllText(saveFileDialog.FileName, curTextBox.Text);
                            tab.Text = saveFileDialog.FileName;
                            openFiles.Add(saveFileDialog.FileName);
                        }
                    }
                }
                MessageBox.Show("Сохранил все файлы.");
            }
            else if (res == DialogResult.Cancel)
                e.Cancel = true;
        }

        // Проверяет есть ли изменения в файлах.
        private bool HaveChangedTabs()
        {
            foreach (TabPage tab in tabControl.TabPages)
            {
                RichTextBox curTextBox = tab.Controls[0] as RichTextBox;
                string path = tab.Text;
                // Текст в программе.
                var textInProgramm = curTextBox.Text;
                if (!File.Exists(path))
                    return true;
                // Текст в файле.
                var savedFileText = File.ReadAllText(path);
                if (textInProgramm != savedFileText)
                    return true;
            }
            return false;
        }

        /*
         * Дальше идёт регион с определением 
         * действий всех кнопок.
         */

        // Кнопка "Файл - Новый".
        private void NewButton_Click(object sender, EventArgs e)
        {
            var tabPage = new TabPage("Новый документ");
            // Создание нового текстового поля.
            var richTextBox = new RichTextBox();
            // Настройка текстового поля.
            richTextBox.Dock = DockStyle.Fill;
            richTextBox.ContextMenuStrip = contextMenuStrip;
            richTextBox.BackColor = curBackColor;
            richTextBox.ForeColor = curFontColor;
            richTextBox.Font = curTextFont;
            // Добавление нового текстового поля.
            tabPage.Controls.Add(richTextBox);
            tabControl.TabPages.Add(tabPage);
        }

        // Кнопка "Файл - Открыть".
        private void Open_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            var tabPage = new TabPage(openFileDialog.FileName);
            // Создание нового текстового поля.
            var richTextBox = new RichTextBox();
            // Настройка текстового поля.
            richTextBox.Dock = DockStyle.Fill;
            richTextBox.ContextMenuStrip = contextMenuStrip;
            richTextBox.BackColor = curBackColor;
            richTextBox.ForeColor = curFontColor;
            richTextBox.Font = curTextFont;
            richTextBox.Text = File.ReadAllText(openFileDialog.FileName);
            // Добавление нового текстового поля.
            tabPage.Controls.Add(richTextBox);
            tabControl.TabPages.Add(tabPage);
            // Добавление файла в лог.
            openFiles.Add(openFileDialog.FileName);
            WriteOpenedFiles();
        }

        // Кнопка "Файл - Сохранить как".
        private void SaveAs_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            // Сохранение файла.
            File.WriteAllText(saveFileDialog.FileName, GetCurTextBox().Text);
            tabControl.SelectedTab.Text = saveFileDialog.FileName;
            openFiles.Add(saveFileDialog.FileName);
        }

        // Кнопка "Файл - Сохранить".
        private void Save_Click(object sender, EventArgs e)
        {
            if (!tabControl.SelectedTab.Text.EndsWith(".txt") && !tabControl.SelectedTab.Text.EndsWith(".rtf"))
                SaveAs_Click(sender, e);
            else
            {
                // Сохранение файла.
                var filepath = Path.GetFullPath($"{tabControl.SelectedTab.Text}");
                File.WriteAllText(filepath, GetCurTextBox().Text);
                tabControl.SelectedTab.Text = filepath;
            }
        }

        // Кнопка "Файл - Выход".
        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Кнопка "Правка - Копировать".
        private void Copy_Click(object sender, EventArgs e)
        {
            GetCurTextBox().Copy();
        }

        // Кнопка "Правка - Вставить".
        private void Put_Click(object sender, EventArgs e)
        {
            GetCurTextBox().Paste();
        }

        // Кнопка "Правка - Вырезать".
        private void Cut_Click(object sender, EventArgs e)
        {
            GetCurTextBox().Cut();
        }

        // Кнопка "Правка - Выделить всё".
        private void SelectAll_Click(object sender, EventArgs e)
        {
            GetCurTextBox().SelectAll();
        }

        // Кнопка "Формат - Настройки шрифта".
        private void FontButton_Click(object sender, EventArgs e)
        {
            if (fontDialog.ShowDialog() == DialogResult.Cancel)
                return;
            // Смена шрифта во всех вкладках.
            foreach (TabPage tab in tabControl.TabPages)
            {
                RichTextBox curTextBox = tab.Controls[0] as RichTextBox;
                curTextBox.Font = fontDialog.Font;
            }
            curTextFont = fontDialog.Font;
        }

        // Кнопка "Настройки - Цвет фона".
        private void ColorButton_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.Cancel)
                return;
            // Смена цвета фона во всех вкладках.
            foreach (TabPage tab in tabControl.TabPages)
            {
                RichTextBox curTextBox = tab.Controls[0] as RichTextBox;
                curTextBox.BackColor = colorDialog.Color;
            }
            curBackColor = colorDialog.Color;
            WriteSettingsFile();
        }

        // Кнопка "Настройки - Цвет шрифта".
        private void ColorButton2_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.Cancel)
                return;
            // Смена цвета шрифта во всех вкладках.
            foreach (TabPage tab in tabControl.TabPages)
            {
                RichTextBox curTextBox = tab.Controls[0] as RichTextBox;
                curTextBox.ForeColor = colorDialog.Color;
            }
            curFontColor = colorDialog.Color;
            WriteSettingsFile();
        }

        // Кнопка "Закрыть вкладку".
        private void CloseTabButton_Click(object sender, EventArgs e)
        {
            var currentTab = tabControl.SelectedTab;
            if (currentTab != null)
            {
                // Закрытие вкладки.
                tabControl.TabPages.Remove(currentTab);
                try
                {
                    openFiles.Remove(currentTab.Text);
                    WriteOpenedFiles();
                }
                catch { }
            }
        }

        // Кнопка ПКМ - создание выпадающего меню.
        private void richTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                GetCurTextBox().ContextMenuStrip = contextMenuStrip;
        }

        // Кнопка формат из выпадающего меню.
        private void FormatContextButton_Click(object sender, EventArgs e)
        {
            if (fontDialog.ShowDialog() == DialogResult.Cancel)
                return;
            GetCurTextBox().SelectionFont = fontDialog.Font;
        }

        // Кнопка "Время автосохранения".
        private void autoSaveTimeBox_Change(object sender, EventArgs e)
        {
            timerSave.Interval = int.Parse(autoSaveTimeBox.Text) * 60000;
            WriteSettingsFile();
        }
    }
}
