﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.Foundation;

namespace RemindBackgroundTask3
{

class SelectedWeekNum
    {
        private int weekNum;
        private static DateTime oneWeekTime = new DateTime(2016, 9, 5, 0, 0, 0);
        private DateTime weekNumOfMonday;
        public void SetWeekTime(int i)
        {
            weekNumOfMonday = oneWeekTime.AddDays(i * 7);
            Debug.WriteLine(weekNumOfMonday);
        }
        public DateTime WeekNumOfMonday
        {
            get
            {
                return weekNumOfMonday;
            }

            set
            {
                weekNumOfMonday = value;
            }
        }
        public int WeekNum
        {
            get
            {
                return weekNum;
            }

            set
            {
                weekNum = value;
            }
        }

    }
    [DataContract]
class DateItemModel
    {
        ///<summary>
        /// 
        /// </summary>
        [DataMember(Name = "week")]
        public string Week { get; set; }
        ///<summary>
        /// 
        /// </summary>
        [DataMember(Name = "class")]
        public string Class { get; set; }

        ///<summary>
        /// 
        /// </summary>
        [DataMember(Name = "day")]
        public string Day { get; set; }

    }



    [DataContract]
class MyRemind
    {
        public Guid Id_system { get; set; }
        public DateTimeOffset time { get; set; }
        private string classDay;
        private string totalWeek;
        private Visibility dot;
        private Visibility rewrite;
        private Visibility deleteIcon;
        public string Tag { get; set; }





        ///<summary>
        /// timestamp+4位随机
        /// </summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        ///<summary>
        /// 
        /// </summary>
        [DataMember(Name = "stuNum")]
        public string StuNum { get; set; }

        ///<summary>
        /// 
        /// </summary>
        [DataMember(Name = "idNum")]
        public string IdNum { get; set; }

        ///<summary>
        /// 
        /// </summary>
        [DataMember(Name = "date")]
        public List<DateItemModel> DateItems { get; set; }
        private string _title;
        private string _content;
        ///<summary>
        /// 相对上课的时间，单位分钟，如1-2课上课，7.45提醒，time应为15，如果设置为8.05提醒，则设置为-5
        /// </summary>
        [DataMember(Name = "time")]
        public string Time { get; set; }

        ///<summary>
        /// 事项的标题，不能为空
        /// </summary>
        [DataMember(Name = "title")]
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;

            }
        }

        ///<summary>
        /// 事项的具体内容,可为空
        /// </summary>
        [DataMember(Name = "content")]
        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
            }
        }

        ///<summary>
        /// 当前学期，不传默认本学期 格式 本学期为201620171 下学期为201620172 
        /// </summary>
        [DataMember(Name = "term")]
        public string Term { get; set; }

        public string ClassDay
        {
            get
            {
                return classDay;
            }

            set
            {
                classDay = value;
            }
        }

        public string TotalWeek
        {
            get
            {
                return totalWeek;
            }

            set
            {
                totalWeek = value;
            }
        }
    }

    class AddRemindReturn
    {
        [DataMember(Name = "stuNum")]
        public string StuNum { get; set; }

        ///<summary>
        /// 
        /// </summary>
        [DataMember(Name = "idNum")]
        public string IdNum { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }
    }



}
