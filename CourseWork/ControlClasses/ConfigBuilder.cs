using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CourseWork
{
    class ConfigBuilder
    {
        public List<string> Colomns { get; set; } = new List<string>();
        public List<string> Filters { get; set; } = new List<string>();
        public List<object[]> ColorConditions { get; set; } = new List<object[]>();
        private readonly string start_file = AppDomain.CurrentDomain.BaseDirectory + @"config\start file\current_state.config";

        public void StartConfig()
        {
            try
            {
                XDocument xDoc = XDocument.Load(start_file);
                XElement xRoot = xDoc.Root;
                string path = xRoot.Element("file").Attribute("path").Value;

                ConfigReader(path);
            }
            catch
            {
                Colomns = new List<string> { "Дисциплина", "Дата и время", "Учебная группа", "Подгруппа", "Аудитория", "Корпус", "Тип занятий", "Преподаватель" };
                MessageBox.Show("Произошла ошибку загрузки файла с сохраненными настройками. Приложение будет запущено со стандартными настройками", "Ошибка загрузки", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void ChangeStartPath(string path)
        {
                XDocument xDoc = new XDocument(new XElement("statefile"));
                XElement xRoot = xDoc.Root;
                xRoot.Add(new XElement("file", new XAttribute("path", path)));
                xDoc.Save(start_file);

        }
        public void ConfigReader(string path)
        {
            try
            {
                XDocument xDoc = XDocument.Load(path);
                XElement xRoot = xDoc.Root;

                foreach (XElement colomn_name in xRoot.Element("colomns").Elements())
                    Colomns.Add(colomn_name.Attribute("value").Value);

                foreach (XElement filter in xRoot.Element("filters").Elements())
                    Filters.Add(filter.Attribute("value").Value);

                foreach (XElement condition in xRoot.Elements("colorcond").Elements())
                {
                    object[] cond = new object[5];

                    cond[0] = condition.Element("condname").Attribute("value").Value;
                    cond[1] = condition.Element("condparam").Attribute("value").Value;
                    cond[2] = condition.Element("condcond").Attribute("value").Value;
                    cond[3] = condition.Element("condvalue").Attribute("value").Value;
                    cond[4] = Color.FromArgb(Convert.ToInt32(condition.Element("condcolor").Attribute("value").Value));

                    ColorConditions.Add(cond);
                }
                
            }
            catch 
            { 
                Colomns = new List<string> { "Дисциплина", "Дата и время", "Учебная группа", "Подгруппа", "Аудитория", "Корпус", "Тип занятий", "Преподаватель" };
                MessageBox.Show("Произошла ошибку загрузки файла с сохраненными настройками. Приложение будет запущено со стандартными настройками", "Ошибка загрузки", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        
        public void ConfigWriter(string path)
        {
            ChangeStartPath(path);

            XDocument xDoc = new XDocument(new XElement("state"));
            XElement xRoot = xDoc.Root;
            xRoot.Add(new XElement("colomns"));
            xRoot.Add(new XElement("filters"));
            xRoot.Add(new XElement("colorcond"));

            foreach (string colomn in Colomns)
                xRoot.Element("colomns").Add(new XElement("colomn", new XAttribute("value", colomn)));

            foreach (string filter in Filters)
                xRoot.Element("filters").Add(new XElement("filter", new XAttribute("value", filter)));

            foreach (object[] condition in ColorConditions)
            {
                xRoot.Element("colorcond").Add(new XElement("item"));
                XElement colorcond = xRoot.Element("colorcond").Element("item");
                colorcond.Add(new XElement("condname", new XAttribute("value", condition[0])));
                colorcond.Add(new XElement("condparam", new XAttribute("value", condition[1])));
                colorcond.Add(new XElement("condcond", new XAttribute("value", condition[2])));
                colorcond.Add(new XElement("condvalue", new XAttribute("value", condition[3])));
                colorcond.Add(new XElement("condcolor", new XAttribute("value", ((Color)condition[4]).ToArgb())));
            }

            xDoc.Save(path);
        }
    }
}
