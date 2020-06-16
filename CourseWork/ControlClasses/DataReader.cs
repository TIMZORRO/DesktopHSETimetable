using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace CourseWork
{
    public class DataReader : IDisposable
    {
        private string[] WeekDays { get; } = new string[] { "понедельник", "вторник", "среда", "четверг", "пятница", "суббота", "воскресение" };
        private string[] Months { get; } = new string[] { "январь", "февраль", "март", "апрель", "май", "июнь", "июль", "август", "сентябрь", "октябрь", "ноябрь", "декабрь" };
        private string[] MonthsRod { get; } = new string[] { "января", "февраля", "марта", "апреля", "мая", "июня", "июля", "августа", "сентября", "октября", "ноября", "декабря" };
        public List<string> Troubles { get; private set; } = new List<string>();

        public List<string[]> Data { get; private set; } = new List<string[]>();

        private Application excelApp = new Application();

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        public void ReadOneFile(string path)
        {
            if (CheckExcel(path))
            {
                ExtractFile(path);
            }
        }

        public void ReadFromDirectory(string path)
        {
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                if (CheckExcel(file))
                {
                    ExtractFile(file);
                }
            }
        }

        public void Dispose()
        {
            Troubles = new List<string>(0);
            Data = new List<string[]>(0);

            excelApp.Quit();

            int hWnd = excelApp.Application.Hwnd;
            uint processID;

            GetWindowThreadProcessId((IntPtr)hWnd, out processID);
            Process.GetProcessById((int)processID).Kill();
        }

        private bool CheckExcel(string filePath)
        {
            string[] partname = filePath.Split('.');
            return partname[partname.Length - 1] == "xls" || partname[partname.Length - 1] == "xlsx";
        }

        private void ExtractFile(string filePath)
        {
            string[] partpath = filePath.Split('\\');
            string[] arr_file = partpath[partpath.Length - 1].ToLower().Split();
            if (arr_file[0] == "расписание" && (arr_file[1] == "занятий" || arr_file[1] == "сессия" || arr_file[1] == "сессии"))
            {
                ExtractBacalavr(filePath);
            }
            else if (arr_file[0] == "магистратура")
            {
                ExtractMagistr(filePath);
            }
            else if (Months.Contains(arr_file[0]))
            {
                ExtractPartTime(filePath);
            }
            else if (arr_file[0][arr_file[0].Length - 1] == 'з')
            {
                ExtractDistance(filePath);
            }
            else if (arr_file[arr_file.Length - 1].Split('.')[0] == "(фак" || arr_file[arr_file.Length - 1].Split('.')[0] == "(фак)")
            {
                ExtractElective(filePath);
            }
            else if (arr_file[0] == "расписание" && arr_file[1].Split('.')[0] == "пересдач")
            {
                ExtractRetake(filePath);
            }
        }

        private void ExtractBacalavr(string filePath)
        {
            Workbook wb = excelApp.Workbooks.Open(filePath, 0, true);
            foreach (Worksheet ws in wb.Worksheets)
            {
                string[] partpath = filePath.Split('\\');
                string[] arr_file = partpath[partpath.Length - 1].ToLower().Split(); // набор слов из названия для дальнейшего определения типа обработки

                // объявление начальных значений ячеек с информацией о курсе, дате и времени
                Range courseCell = ws.Cells[2, "A"];
                Range dateCell = ws.Cells[4, "A"];
                Range timeCell = ws.Cells[4, "B"];

                string[] date = dateCell.Text.ToString().Split(); // в ячейке с датой хранится название дня недели и собственно дата

                while (WeekDays.Contains(date[0].ToLower()))
                {
                    Range groupCell = ws.Cells[3, "C"]; // ячейка с информацией о группе
                    while (groupCell.Text != "")
                    {
                        Range lessonCell = ws.Cells[timeCell.Row, groupCell.Column]; // ячейка с информацией о занятии
                        if (lessonCell.Text != "")
                        {
                            // Ячейки с информацией о занятии может быть нестандартной, 
                            // поэтому при любом исключении в секции чтения данных оттуда
                            // следует считать, что нарушена структура ячейки в исходном файле
                            // В таком случае нужно сохранить данные об этом занятии отдельно, 
                            // чтобы была возможность их отдельно обработать с участием пользователя
                            try
                            {
                                int i = Data.Count;
                                Data.Add(new string[11]);
                                Data[i][0] = "Бакалавриат";
                                Data[i][1] = courseCell.Text.ToString().Split()[0]; // в ячейке хранится информация вида "2 курс", поэтому выбирается только первое слово
                                Data[i][2] = date[1];

                                string pattern = @"\n\s*\n"; // патерн для пустых строк
                                string pattern2 = @"\s+"; // паттерн для 2-х и более пробельных символов
                                Regex reg = new Regex(pattern);
                                Regex reg2 = new Regex(pattern2);

                                // в ячейке с информацией о времени содержится также номер занятия
                                // Номер занятия отделяется от времени пустой строкой
                                // Время указано в виде "время начала - время конца"
                                // Для приложения нужно только время начала
                                // Также часы и минуты могут разделяться двоеточием или точкой
                                // Необходимо привести к единому виду
                                Data[i][3] = reg.Replace(timeCell.Text, "\n").Split('\n')[1].Split('-')[0].Trim().Replace('.', ':');

                                Data[i][4] = groupCell.Text.Trim();

                                // В ячейке с информацией о занятиях хранятся данные в следующей структуре
                                // дисциплина1\n\nпреподаватель1 (аудитория1\[корпус1\][, подгруппа1])
                                // [\nпреподаватель2 (аудитория2\[корпус2\][, подгруппа2])]
                                // [\nдисциплина2\n\nпреподаватель3 (аудитория3\[корпус3\][, подгруппа3]) 
                                // [\nпреподаватель4 (аудитория4\[корпус4\][, подгруппа4])]]
                                // Символами [ ] обозначены необязательные элементы, 
                                // а \[ \] - реально присутствующие в тексте ячейки скобки
                                
                                // Уничтожение лишних строк и разделение по строкам
                                string[] lessonLines = reg.Replace(lessonCell.Text.Trim(), "\n").Split('\n');

                                // Если строка точно нестандартная - выбросить исключение для перехода в обработчик
                                if (lessonLines.Length == 1)
                                {
                                    throw new Exception();
                                }
                                // TODO: Переписать блоки для занятий и для сессии в один блок
                                else if (arr_file[1] == "занятий") // для файлов с обычными занятиями
                                {
                                    for (int n = 0, m = 1; m < lessonLines.Length; n += 2, m += 2)
                                    {
                                        if (n > 0)
                                        {
                                            i = Data.Count;
                                            Data.Add((string[])Data.Last().Clone());
                                            Data[i][10] = null;
                                        }
                                        lessonLines[m] = reg2.Replace(lessonLines[m], " ");

                                        string[] help = lessonLines[n].Split('(');
                                        Data[i][5] = help[0].Trim().ToLower();
                                        if (help.Length > 1)
                                        {
                                            Data[i][10] = "Лекция";
                                        }

                                        help = lessonLines[m].Split();
                                        Data[i][6] = help[0].Trim() + " " + help[1].Trim();
                                        help = lessonLines[m].Split('(', '[', ',', ')', ']');
                                        Data[i][7] = help[1].Trim();
                                        Data[i][8] = help[2].Trim();
                                        if (help.Length > 5)
                                            Data[i][9] = help[4].Trim();
                                        else
                                            Data[i][9] = "0";
                                        if (Data[i][10] != null) { }
                                        else if (lessonCell.MergeCells || lessonCell.Font.Underline == 2)
                                            Data[i][10] = "Лекция";
                                        else if (Data[i][9] == "0")
                                            Data[i][10] = "Семинар";
                                        else
                                            Data[i][10] = "Практика";
                                    }
                                }

                                else
                                {
                                    string[] help = lessonLines[0].Split('(')[0].Split();
                                    for (int j = 0; j < help.Length; j++)
                                        if (help[j].ToLower() != "экзамен")
                                            Data[i][5] += " " + help[j];
                                    Data[i][5] = Data[i][5].Trim().ToLower();
                                    Data[i][10] = "ЭКЗАМЕН";

                                    for (int m = 1; m < lessonLines.Length; m++)
                                    {
                                        if (m > 1)
                                        {
                                            i = Data.Count;
                                            Data.Add((string[])Data.Last().Clone());
                                        }
                                        if (lessonLines[m].ToLower().Contains("экзамен"))
                                        {
                                            string[] strs = lessonLines[m].Split('(')[0].Split();
                                            for (int j = 0; j < strs.Length; j++)
                                                if (strs[j].ToLower() != "экзамен")
                                                    Data[i][5] += " " + strs[j];

                                            m++;
                                        }
                                        lessonLines[m] = reg2.Replace(lessonLines[m], " ");

                                        string[] profinfo = lessonLines[m].Split(',');
                                        help = lessonLines[m].Split('(', '[', ']');
                                        Data[i][7] = help[1].Trim();
                                        Data[i][8] = help[2].Trim();

                                        if (help.Last() != ")")
                                            Data[i][9] = help.Last().Trim().Substring(1, help.Last().Trim().Length-2).Trim();
                                        else
                                            Data[i][9] = "0";
                                        for (int p = 0; p < profinfo.Length; p++)
                                        {
                                            if (profinfo[p].Trim().Split(')')[0].Length == 1) break;
                                            if (p > 0)
                                            {
                                                i = Data.Count;
                                                Data.Add((string[])Data.Last().Clone());
                                            }

                                            help = profinfo[p].Trim().Split();
                                            Data[i][6] = help[0].Trim() + " " + help[1].Trim();
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                string error = "";
                                for (int i = 0; i < 5; i++)
                                    error += Data.Last()[i] + " ";
                                error += lessonCell.Text;
                                Troubles.Add(error);
                                Data.Remove(Data.Last());
                            }
                        }

                        groupCell = ws.Cells[groupCell.Row, lessonCell.MergeArea.Column + lessonCell.MergeArea.Count];
                    }
                    timeCell = ws.Cells[timeCell.Row + 1, timeCell.Column];
                    if (dateCell.MergeArea.Count + dateCell.Row - 1 < timeCell.Row)
                    {
                        dateCell = ws.Cells[dateCell.Row + dateCell.MergeArea.Count, dateCell.Column];
                        date = dateCell.Text.ToString().Split();
                    }
                }
            }
            wb.Close();
        }

        private void ExtractMagistr(string file)
        {
            Workbook wb = excelApp.Workbooks.Open(file, 0, true);
            foreach (Worksheet ws in wb.Worksheets)
            {
                string[] partpath = file.Split('\\');
                string[] arr_file = partpath[partpath.Length - 1].ToLower().Split();
                Range dCell = ws.Cells[10, "A"];
                Range vCell = ws.Cells[10, "C"];

                while (vCell.Text != "")
                {
                    Range gCell = ws.Cells[8, "D"];
                    while (gCell.Text != "")
                    {
                        Range zCell = ws.Cells[vCell.Row, gCell.Column];
                        string pattern = @"\s+";
                        Regex reg = new Regex(pattern);
                        string[] zan = reg.Replace(zCell.Text.Trim(), " ").Split('/');
                        if (zCell.Text != "" && zan.Length != 1)
                        {
                            try
                            {
                                if (zan.Length <= 3)
                                {
                                    int i = Data.Count;
                                    Data.Add(new string[11]);
                                    Data[i][0] = "Магистратура";
                                    Data[i][1] = arr_file[1].Trim();
                                    Data[i][2] = dCell.Text.Trim();
                                    Data[i][3] = vCell.Text.Trim().Replace('.', ':');
                                    Data[i][4] = gCell.Text.Trim().Split()[1];
                                    Data[i][5] = zan[0].Trim().ToLower();
                                    string[] help = zan[2].Trim().Split('(', ')');
                                    Data[i][6] = help[0].Trim();
                                    Data[i][7] = help[1].Replace(")", "");
                                    Data[i][8] = "0";
                                    Data[i][9] = "0";
                                    Data[i][10] = zan[1].Trim();
                                }
                                else if (zan.Length == 4)
                                {
                                    int i = Data.Count;
                                    Data.Add(new string[11]);
                                    Data[i][0] = "Магистратура";
                                    Data[i][1] = arr_file[1].Trim();
                                    Data[i][2] = dCell.Text.Trim();
                                    Data[i][3] = vCell.Text.Trim().Replace('.', ':');
                                    Data[i][4] = gCell.Text.Trim().Split()[1];
                                    Data[i][5] = zan[0].Trim().ToLower();
                                    string[] help = zan[2].Trim().Split('(');
                                    Data[i][6] = help[0].Trim();
                                    Data[i][7] = zan[3].Replace(")", "");
                                    Data[i][8] = help[2].Trim().Substring(0, 1);
                                    Data[i][9] = "0";
                                    Data[i][10] = zan[1].Trim();
                                }
                                else
                                {
                                    pattern = @"\n\s*\n";
                                    reg = new Regex(pattern);
                                    string text = reg.Replace(zCell.Text.Trim(), "\n");
                                    pattern = @"\n";
                                    reg = new Regex(pattern);
                                    text = reg.Replace(text, "/");
                                    pattern = @"\s+";
                                    reg = new Regex(pattern);
                                    text = reg.Replace(text, " ");

                                    zan = text.Split('/');

                                    for (int j = 0; j < zan.Length; j += 4)
                                    {
                                        int i = Data.Count;
                                        Data.Add(new string[11]);
                                        Data[i][0] = "Магистратура";
                                        Data[i][1] = arr_file[1].Trim();
                                        Data[i][2] = dCell.Text.Trim();
                                        Data[i][3] = vCell.Text.Trim();
                                        Data[i][4] = gCell.Text.Trim().Split()[1];
                                        Data[i][5] = zan[j].Trim().ToLower();
                                        string[] help = zan[j + 2].Trim().Split('(');
                                        Data[i][6] = help[0];
                                        Data[i][7] = zan[j + 3].Replace(")", "");
                                        Data[i][8] = help[2].Trim().Substring(0, 1);
                                        Data[i][9] = "0";
                                        Data[i][10] = zan[j + 1].Trim();
                                    }
                                }
                            }

                            catch
                            {
                                string error = "";
                                for (int i = 0; i < 5; i++)
                                    error += Data.Last()[i] + " ";
                                error += zCell.Text;
                                Troubles.Add(error);
                                Data.Remove(Data.Last());
                            }
                        }
                        gCell = ws.Cells[gCell.Row, zCell.MergeArea.Column + zCell.MergeArea.Count];
                    }
                    vCell = ws.Cells[vCell.Row + 1, vCell.Column];
                    if (dCell.MergeArea.Count + dCell.Row - 1 < vCell.Row)
                        dCell = ws.Cells[dCell.Row + dCell.MergeArea.Count, dCell.Column];
                }
            }
            wb.Close();
        }

        private void ExtractPartTime(string file)
        {
            Workbook wb = excelApp.Workbooks.Open(file, 0, true);
            foreach (Worksheet ws in wb.Worksheets)
            {
                Range dCell = ws.Cells[4, "A"];
                Range vCell = ws.Cells[5, "B"];
                if (vCell.Text == "")
                {
                    vCell = ws.Cells[vCell.Row + 1, vCell.Column];
                }

                string pattern = @"\s+";
                Regex reg = new Regex(pattern);
                string[] date = reg.Replace(dCell.Text.Trim(), " ").Split();
                while (WeekDays.Contains(date[0].ToLower()))
                {
                    Range gCell = ws.Cells[3, "C"];
                    while (gCell.Text != "")
                    {
                        Range zCell = ws.Cells[vCell.Row - 1, gCell.Column];
                        Range pCell = ws.Cells[vCell.Row, gCell.Column];
                        if (zCell.Text != "")
                        {
                            try
                            {
                                int i = Data.Count;
                                Data.Add(new string[11]);
                                string[] wsName = reg.Replace(ws.Name.Trim(), " ").Split();

                                Data[i][0] = "Очно-заочная " + wsName[2].Substring(1, wsName[2].Length - 2);
                                Data[i][1] = wsName[0];
                                int month = 0;
                                for (; month < Months.Length; month++)
                                    if (date[2].Replace(",", "").ToLower() == Months[month] || date[2].Replace(",", "").ToLower() == MonthsRod[month])
                                        break;
                                DateTime moment = DateTime.Now;
                                Data[i][2] = date[1] + "." + (++month).ToString() + "." + moment.Year;
                                Data[i][3] = vCell.Text.Split('-')[0].Trim().Replace('.', ':');
                                Data[i][4] = reg.Replace(gCell.Text.Trim(), " ").Split('(')[0].Trim();
                                Data[i][5] = zCell.Text.ToLower().Replace("экзамен", "").Trim().ToLower(); 
                                string[] help = reg.Replace(pCell.Text.Trim(), " ").Split();
                                Data[i][6] = help[0] + " " + help[1];
                                help = reg.Replace(pCell.Text.Trim(), " ").Split('(')[1].Split(',');
                                Data[i][7] = help[0].Replace("ауд.", "").Trim();
                                Data[i][8] = help[1].Replace("к.", "").Replace(")", "").Trim();
                                Data[i][9] = "0";

                                if (zCell.Font.Underline == 2)
                                    Data[i][10] = "Экзамен";
                                else if (zCell.MergeCells)
                                    Data[i][10] = "Лекция";
                                else
                                    Data[i][10] = "Семинар";
                            }

                            catch
                            {
                                string error = "";
                                for (int i = 0; i < 5; i++)
                                    error += Data.Last()[i] + " ";
                                error += zCell.Text + " " + pCell.Text;
                                Troubles.Add(error);
                                Data.Remove(Data.Last());
                            }
                        }
                        gCell = ws.Cells[gCell.Row, zCell.MergeArea.Column + zCell.MergeArea.Count];
                    }
                    vCell = ws.Cells[vCell.Row + 2, vCell.Column];
                    if (Int32.TryParse(vCell.Text, out int _))
                        vCell = ws.Cells[vCell.Row + 1, vCell.Column];
                    if (dCell.MergeArea.Count + dCell.Row - 1 < vCell.Row) 
                    { 
                        dCell = ws.Cells[vCell.Row - 1, dCell.Column];
                        if (dCell.Text == "" && vCell.Text != "")
                            dCell = ws.Cells[vCell.Row, dCell.Column];
                        date = reg.Replace(dCell.Text.Trim(), " ").Split();
                    }
                }
            }
        }

        private void ExtractDistance(string file)
        {
            Workbook wb = excelApp.Workbooks.Open(file, 0, true);
            foreach (Worksheet ws in wb.Worksheets)
            {
                string pattern = @"\s+";
                Regex reg = new Regex(pattern);
                string[] wsName = reg.Replace(ws.Name.Trim(), " ").Split();
                if (wsName.Length == 1)
                    break;

                string[] partpath = file.Split('\\');
                string[] arr_file = partpath[partpath.Length - 1].ToLower().Split();
                Range dCell = ws.Cells[7, "A"];
                Range vCell = ws.Cells[8, "B"];
                if (vCell.Text == "")
                {
                    vCell = ws.Cells[vCell.Row + 1, vCell.Column];
                }

                while (dCell.Text!="")
                {
                    Range gCell = ws.Cells[5, "C"];
                    while (gCell.Text != "")
                    {
                        Range zCell = ws.Cells[vCell.Row - 1, gCell.Column];
                        Range pCell = ws.Cells[vCell.Row, gCell.Column];
                        if (zCell.Text != "")
                        {
                            try
                            {
                                int i = Data.Count;
                                Data.Add(new string[11]);

                                Data[i][0] = "Заочная";
                                Data[i][1] = arr_file[0].Substring(0, 2);
                                string[] date_arr = reg.Replace(dCell.Text.Trim(), " ").Split();
                                int month = 0;
                                for (; month < Months.Length; month++)
                                    if (date_arr[1].Replace(",", "").ToLower() == Months[month])
                                        break;
                                Data[i][2] = date_arr[0] + "." + (++month).ToString() + "." + date_arr[2];
                                Data[i][3] = vCell.Text.Split('-')[0].Trim().Replace('.', ':');
                                Data[i][4] = reg.Replace(gCell.Text.Trim(), " ").Split('(')[0].Trim();
                                if (pCell.Text == "")
                                {
                                    string pattern2 = @"\n\s*\n";
                                    Regex reg2 = new Regex(pattern2);
                                    string[] minors = reg2.Replace(zCell.Text.Trim(), "\n").Split('\n');
                                    if (minors.Length <= 1) throw new Exception();
                                    for (int j = 1; j < minors.Length; j++)
                                    {
                                        if (j > 1)
                                        {
                                            i = Data.Count;
                                            Data.Add((string[])Data.Last().Clone());
                                        }
                                        string[] help = reg.Replace(minors[j].Trim(), " ").Split('(')[0].Trim().Split();
                                        for (int k = 0; k < help.Length - 2; k++)
                                            Data[i][5] += " " + help[k];
                                        Data[i][5] = Data[i][5].Trim().ToLower();
                                        Data[i][6] = help[help.Length - 2] + " " + help[help.Length - 1];
                                        help = reg.Replace(minors[j].Trim(), " ").Split('(')[1].Trim().Split(',');
                                        Data[i][7] = help[0].Replace("ауд.", "").Trim();
                                        Data[i][8] = help[1].Replace("к.", "").Replace(")", "").Trim();
                                        Data[i][9] = "0";
                                        Data[i][10] = "Семинар";
                                    }
                                }
                                else
                                {
                                    Data[i][5] = zCell.Text.ToLower().Replace("экзамен", "").Trim();
                                    string[] teachers = pCell.Text.Trim().Split(',', '\n');
                                    for (int j = 0; j < teachers.Length; j += 2)
                                    {
                                        if (j > 0)
                                        {
                                            i = Data.Count;
                                            Data.Add((string[])Data.Last().Clone());
                                        }
                                        string[] help = teachers[j].Split('(');
                                        Data[i][6] = help[0].Trim();
                                        Data[i][7] = help[1].Replace("ауд.", "").Trim();
                                        Data[i][8] = teachers[j + 1].Replace("к.", "").Replace(")", "").Trim();
                                        Data[i][9] = "0";



                                        if (zCell.Font.Underline == 2)
                                            Data[i][10] = "Экзамен";
                                        else if (zCell.MergeCells)
                                            Data[i][10] = "Лекция";
                                        else
                                            Data[i][10] = "Семинар";
                                    }
                                }
                            }

                            catch
                            {
                                string error = "";
                                for (int i = 0; i < 5; i++)
                                    error += Data.Last()[i] + " ";
                                error += zCell.Text + " " + pCell.Text;
                                Troubles.Add(error);
                                Data.Remove(Data.Last());
                            }
                        }
                        gCell = ws.Cells[gCell.Row, zCell.MergeArea.Column + zCell.MergeArea.Count];
                    }
                    vCell = ws.Cells[vCell.Row + 2, vCell.Column];
                    if (Int32.TryParse(vCell.Text, out int _))
                        vCell = ws.Cells[vCell.Row + 1, vCell.Column];
                    if (dCell.MergeArea.Count + dCell.Row - 1 < vCell.Row)
                    {
                        dCell = ws.Cells[dCell.MergeArea.Count + dCell.Row, dCell.Column];
                        if (dCell.Text.Split(',').Length == 1)
                            dCell = ws.Cells[dCell.MergeArea.Count + dCell.Row, dCell.Column];
                    }
                }
            }
            wb.Close();
        }

        private void ExtractRetake(string file)
        {
            Workbook wb = excelApp.Workbooks.Open(file, 0, true);
            foreach (Worksheet ws in wb.Worksheets)
            {
                Range cCell = ws.Cells[4, "A"];
                Range gCell = ws.Cells[6, "B"];
                
                while (cCell.Text != "")
                {
                    Range pCell = ws.Cells[cCell.Row, "C"];
                    Range tCell = ws.Cells[cCell.Row + 1, "C"];
                    Range dCell = ws.Cells[gCell.Row, "C"];
                    while (pCell.Text != "")
                    {
                        try
                        {
                            int i = Data.Count;
                            Data.Add(new string[11]);
                            string[] professors = pCell.Text.Trim().Split('(', ')')[1].Split(',');
                            Data[i][0] = "Бакалавриат";
                            Data[i][1] = cCell.Text.ToString().Split()[0].Trim();
                            Data[i][2] = dCell.Text.Trim().Split('\n')[0].Trim();
                            Data[i][3] = dCell.Text.Trim().Split('\n')[1].Trim().Split()[2].Replace('.', ':');
                            Data[i][4] = gCell.Text.Trim().Split(',')[0].Trim();
                            Data[i][5] = pCell.Text.Trim().Split('(')[0].Trim().ToLower();

                            string help = dCell.Text.ToString();
                            if (help.Trim().Split('\n')[2].Split('(').Length == 1)
                            {
                                Data[i][7] = dCell.Text.Trim().Split('\n')[2].Trim();
                                Data[i][8] = "2";
                            }
                            else
                            {
                                Data[i][7] = dCell.Text.Trim().Split('\n')[2].Trim().Split()[1];
                                Data[i][8] = dCell.Text.Trim().Split('\n')[2].Trim().Split()[3].Split(')')[0];
                            }
                            Data[i][9] = "0";
                            Data[i][10] = "Пересдача " + tCell.Text.Trim();
                            for (int n=0; n < professors.Length; n++)
                            {
                                if (n > 0)
                                {
                                    i = Data.Count;
                                    Data.Add((string[])Data.Last().Clone());
                                }
                                Data[i][6] = professors[n].Trim(); ;
                            }
                        }
                        catch (Exception e)
                        {
                            string error = "";
                            for (int i = 0; i < 2; i++)
                                error += Data.Last()[i] + " ";
                            error += gCell.Text + " " + pCell.Text + " " + dCell.Text;
                            Troubles.Add(error);
                            Data.Remove(Data.Last());
                        }
                        pCell = ws.Cells[pCell.Row, pCell.MergeArea.Column + pCell.MergeArea.Count];
                        tCell = ws.Cells[tCell.Row, tCell.MergeArea.Column + tCell.MergeArea.Count];
                        dCell = ws.Cells[dCell.Row, dCell.MergeArea.Column + dCell.MergeArea.Count];
                    }
                    cCell = ws.Cells[cCell.MergeArea.Row + cCell.MergeArea.Count, cCell.Column];
                    gCell = ws.Cells[cCell.Row + 3, gCell.Column];
                }
            }
        }

        private void ExtractElective(string file)
        {
            Workbook wb = excelApp.Workbooks.Open(file, 0, true);
            foreach (Worksheet ws in wb.Worksheets)
            {
                Range dCell = ws.Cells[11, "B"];
                Range vCell = ws.Cells[11, "D"];
                while (vCell.Text != "")
                {
                    Range zCell = ws.Cells[vCell.Row, "E"];
                    string pattern = @"\s+";
                    Regex reg = new Regex(pattern);
                    string[] zan = reg.Replace(zCell.Text.Trim(), " ").Split('/');
                    if (zCell.Text != "")
                    {
                        try
                        {
                            int i = Data.Count;
                            Data.Add(new string[11]);
                            Data[i][0] = "Факультатив";
                            Data[i][1] = "0";
                            Data[i][2] = dCell.Text.Trim();
                            Data[i][3] = vCell.Text.Split('-')[0].Trim().Replace('.', ':');
                            Data[i][4] = "Факультатив";
                            Data[i][5] = zan[0].Trim().ToLower();
                            string[] help = zan[2].Trim().Split('(');
                            Data[i][6] = help[0].Trim(); ;

                            if (help.Length > 2)
                            {
                                if (help[1] == "")
                                {
                                    string[] strs = help[2].Split(')');

                                    Data[i][7] = strs[1].Trim();
                                    Data[i][8] = strs[0].Replace("к", "").Replace(".", "").Trim();
                                }
                                else
                                {
                                    Data[i][7] = help[1].Trim();
                                    Data[i][8] = help[2].Replace("к", "").Replace(".", "").Replace(")", "").Trim();
                                }
                            }
                            else
                            {
                                Data[i][7] = help[1].Trim().Substring(0, help[1].Trim().Length - 1);
                                Data[i][8] = "0";
                            }

                            Data[i][9] = "0";
                            Data[i][10] = zan[1].Trim();
                        }
                        catch
                        {
                            string error = "";
                            for (int i = 0; i < 5; i++)
                                error += Data.Last()[i] + " ";
                            error += zCell.Text;
                            Troubles.Add(error);
                            Data.Remove(Data.Last());
                        }
                    }
                    vCell = ws.Cells[vCell.Row + 1, vCell.Column];
                    if (dCell.MergeArea.Count + dCell.Row - 1 < vCell.Row)
                        dCell = ws.Cells[dCell.Row + dCell.MergeArea.Count, dCell.Column];
                }
            }
            wb.Close();
        }
    }
}
