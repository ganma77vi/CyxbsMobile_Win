﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace LiveTileBackgroundTask
{
    class Util
    {
        static int largeTileGroupCount = 0;
        public static async void UpdateTile(List<ClassList> tempList, int nowWeek, string weekDay)
        {
            //为应用创建磁贴更新
            var updater = TileUpdateManager.CreateTileUpdaterForApplication();
            //这里设置的是所以磁贴都可以为动态
            updater.EnableNotificationQueue(true);
            updater.Clear();
            int itemCount = 0;
            List<int> correctCount = new List<int>(0);
            #region 创建动态磁贴XML文档
            //1：创建动态磁贴模板
            string tileXml = "<tile>" +
                    "<visual>" +
                        //中磁贴
                        "<binding template=\"TileMedium\">" +
                            "<text hint-wrap=\"true\"></text>" +
                            "<text></text>" +
                            "<text hint-wrap=\"true\"></text>" +
                        "</binding>" +
                        //宽磁贴
                        "<binding template=\"TileWide\">" +
                            "<text hint-style=\"subtitle\"></text>" +
                            "<text></text>" +
                            "<text hint-wrap=\"true\"></text>" +
                        "</binding>" +
                        //大磁贴
                        "<binding template=\"TileLarge\">" +
                            "<group>" +
                                "<subgroup>" +
                                    "<text hint-wrap=\"true\" hint-style=\"subtitle\"></text>" +
                                    "<text></text>" +
                                    "<text hint-wrap=\"true\"></text>" +
                                "</subgroup>" +
                            "</group>" +
                            "<text>" + "\n" + "</text>" +
                            "<group>" +
                                "<subgroup>" +
                                    "<text hint-wrap=\"true\" hint-style=\"subtitle\"></text>" +
                                    "<text></text>" +
                                    "<text hint-wrap=\"true\"></text>" +
                                "</subgroup>" +
                            "</group>" +
                        "</binding>" +
                    "</visual>" +
                 "</tile>";
            #endregion
            for (int i = 0; i < tempList.Count; i++)
            {
                string course = tempList[i].Course.ToString();
                string teacher = tempList[i].Teacher.ToString();
                string lesson = tempList[i].Lesson.ToString() + "  ";
                string classroom = tempList[i].Classroom.ToString();
                int[] weeks = tempList[i].Week;
                //判断课程是否为本周课程
                for (int j = 0; j < weeks.Length; j++)
                {
                    if (weeks[j] == nowWeek && tempList[i].Day.Equals(weekDay))
                    {
                        //满足条件的课程编号存入correctCount
                        correctCount.Add(i);
                        //微软规定动态磁贴的队列数目小于5个
                        if (itemCount++ > 5)
                        {
                            break;
                        }
                    }
                }
            }
            //为XML对象赋值并推送更新
            for (int i = 0; i < correctCount.Count; i++)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(tileXml);
                XmlNodeList elements = doc.GetElementsByTagName("text");
                elements[0].AppendChild(doc.CreateTextNode(tempList[correctCount[i]].Course));
                elements[1].AppendChild(doc.CreateTextNode(tempList[correctCount[i]].Teacher));
                elements[2].AppendChild(doc.CreateTextNode(tempList[correctCount[i]].Lesson));
                elements[2].AppendChild(doc.CreateTextNode(tempList[correctCount[i]].Classroom));
                elements[3].AppendChild(doc.CreateTextNode(tempList[correctCount[i]].Course));
                elements[4].AppendChild(doc.CreateTextNode(tempList[correctCount[i]].Teacher));
                elements[5].AppendChild(doc.CreateTextNode(tempList[correctCount[i]].Lesson));
                elements[5].AppendChild(doc.CreateTextNode(tempList[correctCount[i]].Classroom));
                elements[6].AppendChild(doc.CreateTextNode(tempList[correctCount[i]].Course));
                elements[7].AppendChild(doc.CreateTextNode(tempList[correctCount[i]].Teacher));
                elements[8].AppendChild(doc.CreateTextNode(tempList[correctCount[i]].Lesson));
                elements[8].AppendChild(doc.CreateTextNode(tempList[correctCount[i]].Classroom));
                try
                {
                    elements[10].AppendChild(doc.CreateTextNode(tempList[correctCount[i + 1]].Course));
                    elements[11].AppendChild(doc.CreateTextNode(tempList[correctCount[i + 1]].Teacher));
                    elements[12].AppendChild(doc.CreateTextNode(tempList[correctCount[i + 1]].Lesson));
                    elements[12].AppendChild(doc.CreateTextNode(tempList[correctCount[i + 1]].Classroom));
                }
                catch(ArgumentOutOfRangeException ex)
                {
                    elements[10].AppendChild(doc.CreateTextNode("没课啦 \\(≧▽≦)/"));
                    elements[11].AppendChild(doc.CreateTextNode("记得好好复习哟~"));
                }
                finally
                {
                    updater.Update(new TileNotification(doc));
                }
            }
        }
    }
}