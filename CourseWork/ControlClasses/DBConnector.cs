using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace CourseWork
{
    public class DBConnector
    {
        public List<string> Courses { get; private set; } = new List<string>();
        public List<string> Professors { get; private set; } = new List<string>();
        public string[] Tables { get; } = new string[] { "timetable", "auditoriums", "courses", "edu_groups", "les_types", "professors", "edu_prog", "edu_types" };
        private string Trouble { get; set; }
        public static string[] RussianNameColumn { get; } = new string[] { "Дисциплина", "Дата и время", "Учебная группа", "Подгруппа", "Аудитория", "Корпус", "Тип занятий", "Преподаватель", "Тип обучения", "Образовательная программа", "Курс" };
        public static string[] QueriesBuilding { get; } = new string[]
        {
            "courses|ID|name|Дисциплина|timetable|course",
            "timetable|-|date|Дата и время|-|-",
            "edu_groups|ID|name|Учебная группа|timetable|edu_group",
            "timetable|-|partgroup|Подгруппа|-|-",
            "auditoriums|ID|audit|Аудитория|timetable|auditorium",
            "auditoriums|ID|build|Корпус|timetable|auditorium",
            "les_types|ID|name|Тип занятия|timetable|les_type",
            "professors|ID|name|Преподаватель|timetable|professor",
            "edu_types|ID|name|Тип обучения|edu_prog|edu_type",
            "edu_prog|ID|name|ОП|edu_group|edu_prog",
            "edu_groups|ID|year|Курс|timetable|edu_group"
        };
        private delegate bool str_arr_Compare(string[] strs1, string[] strs2);
        public DataTable Timetable { get; private set; } = new DataTable();
        private string OldServerInfo = "Data Source=localhost; Integrated Security=SSPI; Initial Catalog=TeachersTimetable;";
        private readonly string connectionString = "Server=tcp:practice-timetable-test.database.windows.net,1433;Initial Catalog=Timetable-test;Persist Security Info=False;User ID=Tim;Password=N123b123v123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
       
        public void ListUpdate()
        {
            Courses.Add("-");
            Professors.Add("-");
            SqlConnection sqlConnection = new SqlConnection("Data Source=localhost; Integrated Security=SSPI; Initial Catalog=TeachersTimetable;");
            sqlConnection.Open();
            SqlCommand command = sqlConnection.CreateCommand();
            command.CommandText = "select name from " + Tables[5];
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable data = new DataTable();
            adapter.Fill(data);
            foreach (DataRow row in data.Rows)
            {
                string str = row[0].ToString().Trim();
                if (!Professors.Contains(str))
                    Professors.Add(str);
            }

            command.CommandText = "select name from " + Tables[2];
            adapter = new SqlDataAdapter(command);
            data = new DataTable();
            adapter.Fill(data);
            foreach (DataRow row in data.Rows)
            {
                string str = row[0].ToString().Trim();
                if (!Courses.Contains(str))
                    Courses.Add(str);
            }

            Courses.Sort();
            Professors.Sort();
        }

        public void SendQueryToDB(List<string> filter)
        {
            QuerySended(QueryBuilded(filter));
        }

        private string QueryBuilded(List<string> filter)
        {
            List<string> tables = new List<string>();
            List<string> colomns = new List<string>();
            List<string> connect_col = new List<string>();
            List<string> conditions = new List<string>(); 

            foreach (string str in filter)
            {
                string[] help = str.Split('|');
                if (!tables.Contains(help[0]))
                    tables.Add(help[0]);
                if (!tables.Contains(help[4]))
                    tables.Add(help[4]);

                string colomn = help[0] + "." + help[2] + " as " + @"""" + help[3] + @"""";
                if (!colomns.Contains(colomn))
                    colomns.Add(colomn);

                if (help[4] != "-")
                {
                    string connection = help[0] + "." + help[1] + "=" + help[4] + "." + help[5];
                    if (!connect_col.Contains(connection))
                        connect_col.Add(connection);
                }

                if (help.Length == 9) 
                {
                    string partquery = help[0] + "." + help[6] + " between ";
                    string[] date = help[7].Split('.', ' ');
                    partquery += "'" + date[2] + date[1] + date[0] + " 00:00:00' and ";
                    date = help[8].Split('.', ' ');
                    partquery += "'" + date[2] + date[1] + date[0] + " 23:59:59'";
                    conditions.Add(partquery);
                }
                else
                    for (int i = 6; i < help.Length; i += 2)
                        if (help[i + 1] != "'-'")
                            conditions.Add(help[0] + "." + help[i] + "=" + help[i + 1]);
            }

            string ans = "select ";
            foreach (string col in colomns)
            {
                string colomn_in_table = col.Split('.')[1];

                if (colomn_in_table[0] != '-')
                    ans += col + ", ";
            }
            ans = ans.Trim().Substring(0, ans.Trim().Length - 1) + " from ";

            foreach (string tbl in tables)
                if (tbl != "-")
                    ans += tbl + ", ";
            ans = ans.Trim().Substring(0, ans.Trim().Length - 1);

            if (conditions.Count != 0 || connect_col.Count != 0)
            {
                ans += " where ";

                foreach (string con in connect_col)
                        ans += con + " AND ";

                foreach (string cond in conditions)
                    ans += cond + " AND ";
                ans = ans.Trim().Substring(0, ans.Trim().Length - 3);
            }

            //MessageBox.Show(ans);

            return ans;
        }

        private void QuerySended(string query)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            Timetable = new DataTable();
            sqlConnection.Open();
            SqlCommand command = sqlConnection.CreateCommand();
            command.CommandText = query;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            try
            {
                adapter.Fill(Timetable);
            }
            catch (Exception e) 
            {
                Trouble = e.Message;
                MessageBox.Show("Произошла ошибка обращения к данным. Попробуйте изменить параметры столбцов или перезапустить приложение\n\nКод ошибки: " + Trouble, "Системная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public void TruncateTable(string table)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand command = sqlConnection.CreateCommand();
                command.CommandText = "TRUNCATE TABLE " + table;
                command.ExecuteNonQuery();
            }
        }

        public void DBUpdate(DataReader dataReader)
        {
            List<string[]> LocalCourses = new List<string[]>();
            Courses = new List<string>();
            Professors = new List<string>();
            List<string> LessonsType = new List<string>();
            List<string[]> Auditoriumes = new List<string[]>();
            List<string[]> EduGroups = new List<string[]>();
            List<string[]> EduProgrames = new List<string[]>();
            List<string> EduTypes = new List<string>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand command = sqlConnection.CreateCommand();

                int i = 0;
                foreach (string[] strs in dataReader.Data)
                {
                    // Тип обучения
                    string[] lesson = strs;
                    if (!EduTypes.Contains(lesson[0]))
                    {
                        EduTypes.Add(lesson[0]);
                        command.CommandText = $"INSERT edu_types values ({EduTypes.Count - 1}, '{lesson[0]}')";
                        command.ExecuteNonQuery();
                        lesson[0] = (EduTypes.Count - 1).ToString();
                    }
                    else
                    {
                        Predicate<string> srt_predicate = (str) => str == lesson[0];
                        lesson[0] = EduTypes.FindIndex(srt_predicate).ToString();
                    }

                    // ОП
                    string[] prog = new string[2];
                    int prog_ID = 0;
                    prog[0] = lesson[4].Split('-')[0].ToUpper();
                    prog[1] = lesson[0];
                    str_arr_Compare compare_2arr = (strs1, strs2) => strs1[0] == strs2[0] && strs1[1] == strs2[1];
                    bool be = false;
                    foreach (string[] help in EduProgrames)
                        if (compare_2arr(help, prog))
                        {
                            be = true;
                            break;
                        }
                    if (!be)
                    {
                        EduProgrames.Add(prog);
                        command.CommandText = $"INSERT edu_prog values ({EduProgrames.Count - 1}, '{prog[0]}', {prog[1]})";
                        command.ExecuteNonQuery();
                        prog_ID = EduProgrames.Count - 1;
                    }
                    else
                    {
                        Predicate<string[]> arr_predicate = (arr) => arr[0] == prog[0] && arr[1] == prog[1];
                        prog_ID = EduProgrames.FindIndex(arr_predicate);
                    }

                    // Дисциплина
                    string[] course = new string[3];
                    course[0] = lesson[5];
                    course[1] = lesson[1];
                    course[2] = prog_ID.ToString();
                    str_arr_Compare compare_3arr = (strs1, strs2) => strs1[0] == strs2[0] && strs1[1] == strs2[1] && strs1[2] == strs2[2];
                    be = false;
                    foreach (string[] help in LocalCourses)
                        if (compare_3arr(help, course))
                        {
                            be = true;
                            break;
                        }
                    if (!be)
                    {
                        LocalCourses.Add(course);
                        command.CommandText = $"INSERT courses values ({LocalCourses.Count - 1}, '{course[0]}', {course[1]}, {course[2]})";
                        command.ExecuteNonQuery();
                        lesson[5] = (LocalCourses.Count - 1).ToString();
                    }
                    else
                    {
                        Predicate<string[]> arr_predicate = (arr) => arr[0] == course[0] && arr[1] == course[1] && arr[2] == course[2];
                        lesson[5] = LocalCourses.FindIndex(arr_predicate).ToString();
                    }
                    if (!Courses.Contains(course[0]))
                        Courses.Add(course[0]);

                    // Группа
                    string[] group = new string[3];
                    group[0] = lesson[4];
                    group[1] = lesson[1];
                    group[2] = prog_ID.ToString();
                    be = false;
                    foreach (string[] help in EduGroups)
                        if (compare_3arr(help, group))
                        {
                            be = true;
                            break;
                        }
                    if (!be)
                    {
                        EduGroups.Add(group);
                        command.CommandText = $"INSERT edu_groups values ({EduGroups.Count - 1}, '{group[0]}', {group[1]}, {group[2]})";
                        command.ExecuteNonQuery();
                        lesson[4] = (EduGroups.Count - 1).ToString();
                    }
                    else
                    {
                        Predicate<string[]> arr_predicate = (arr) => arr[0] == group[0] && arr[1] == group[1] && arr[2] == group[2];
                        lesson[4] = EduGroups.FindIndex(arr_predicate).ToString();
                    }

                    // Аудитория
                    string[] audit = new string[2];
                    audit[0] = lesson[7];
                    audit[1] = lesson[8];
                    be = false;
                    foreach (string[] help in Auditoriumes)
                        if (compare_2arr(help, audit))
                        {
                            be = true;
                            break;
                        }
                    if (!be)
                    {
                        Auditoriumes.Add(audit);
                        command.CommandText = $"INSERT auditoriums values ({Auditoriumes.Count - 1}, '{audit[0]}', '{audit[1]}')";
                        command.ExecuteNonQuery();
                        lesson[7] = (Auditoriumes.Count - 1).ToString();
                    }
                    else
                    {
                        Predicate<string[]> arr_predicate = (arr) => arr[0] == audit[0] && arr[1] == audit[1];
                        lesson[7] = Auditoriumes.FindIndex(arr_predicate).ToString();
                    }

                    // Преподаватели
                    if (!Professors.Contains(lesson[6]))
                    {
                        Professors.Add(lesson[6]);
                        command.CommandText = $"INSERT professors values ({Professors.Count - 1}, '{lesson[6]}')";
                        command.ExecuteNonQuery();
                        lesson[6] = (Professors.Count - 1).ToString();
                    }
                    else
                    {
                        Predicate<string> srt_predicate = (str) => str == lesson[6];
                        lesson[6] = Professors.FindIndex(srt_predicate).ToString();
                    }

                    // Тип занятия
                    if (!LessonsType.Contains(lesson[10]))
                    {
                        LessonsType.Add(lesson[10]);
                        command.CommandText = $"INSERT les_types values ({LessonsType.Count - 1}, '{lesson[10]}')";
                        command.ExecuteNonQuery();
                        lesson[10] = (LessonsType.Count - 1).ToString();
                    }
                    else
                    {
                        Predicate<string> srt_predicate = (str) => str == lesson[10];
                        lesson[10] = LessonsType.FindIndex(srt_predicate).ToString();
                    }

                    // Дата и время
                    string[] date = lesson[2].Split('.');
                    string[] time = lesson[3].Split(':');
                    if (date[0].Length < 2) date[0] = "0" + date[0];
                    if (date[1].Length < 2) date[1] = "0" + date[1];
                    if (date[2].Length < 4) date[2] = "20" + date[2];
                    string dateTime = date[2] + date[1] + date[0] + " " + time[0] + ":" + time[1] + ":00";

                    // Расписание
                    command.CommandText = $"INSERT timetable (ID, course, professor, auditorium, edu_group, partgroup, les_type, date) values ({i++}, {lesson[5]}, {lesson[6]}, {lesson[7]}, {lesson[4]}, {lesson[9]}, {lesson[10]}, '{dateTime}')";
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
