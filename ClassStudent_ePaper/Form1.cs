﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using K12.Data;
using System.IO;
using SmartSchool.ePaper;
using Aspose.Words;

namespace ClassStudent_ePaper
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 班級電子報表
        /// </summary>
        SmartSchool.ePaper.ElectronicPaper paperForClass { get; set; }

        /// <summary>
        /// 學生電子報表
        /// </summary>
        SmartSchool.ePaper.ElectronicPaper paperForStudent { get; set; }

        /// <summary>
        /// 範本
        /// </summary>
        MemoryStream template = new MemoryStream(Properties.Resources.Template);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //未輸入電子報表名稱的檢查
            if (textBox1.Text.Trim() != "")
            {
                //主要的Word文件
                Document doc = new Document();
                doc.Sections.Clear();

                if (radioButton2.Checked)
                {
                    #region 班級電子報表
                    //建立一個班級電子報表
                    //傳入參數 : 報表名稱,學年度,學期,類型(學生/班級/教師/課程)
                    paperForClass = new SmartSchool.ePaper.ElectronicPaper(textBox1.Text, School.DefaultSchoolYear, School.DefaultSemester, SmartSchool.ePaper.ViewerType.Class);

                    MemoryStream stream = new MemoryStream();

                    Document each_page = new Document(template, "", LoadFormat.Doc, "");
                    each_page.Save(stream, SaveFormat.Doc);

                    //取得所選擇的班級ID
                    List<string> ClassID = K12.Presentation.NLDPanels.Class.SelectedSource;
                    foreach (string each in ClassID)
                    {
                        //傳參數給PaperItem
                        //格式 / 內容 / 對象的系統編號
                        paperForClass.Append(new PaperItem(PaperFormat.Office2003Doc, stream, each));
                    }

                    //開始上傳
                    SmartSchool.ePaper.DispatcherProvider.Dispatch(paperForClass); 
                    #endregion
                }
                else
                {
                    #region 班級學生的電子報表
                    //建立一個學生電子報表
                    //傳入參數 : 報表名稱,學年度,學期,類型(學生/班級/教師/課程)
                    paperForStudent = new SmartSchool.ePaper.ElectronicPaper(textBox1.Text, School.DefaultSchoolYear, School.DefaultSemester, SmartSchool.ePaper.ViewerType.Student);

                    //學生個人的文件
                    Document each_page = new Document(template, "", LoadFormat.Doc, "");
                    MemoryStream stream = new MemoryStream();
                    each_page.Save(stream, SaveFormat.Doc);
                    doc.Sections.Add(doc.ImportNode(each_page.Sections[0], true)); //合併至doc

                    List<string> ClassID = K12.Presentation.NLDPanels.Class.SelectedSource; //取得畫面上所選班級的ID清單
                    List<StudentRecord> srList = Student.SelectByClassIDs(ClassID); //依據班級ID,取得學生物件
                    foreach (StudentRecord sr in srList)
                    {
                        //傳參數給PaperItem
                        //格式 / 內容 / 對象的系統編號
                        paperForStudent.Append(new PaperItem(PaperFormat.Office2003Doc, stream, sr.ID));
                    }

                    //開始上傳
                    SmartSchool.ePaper.DispatcherProvider.Dispatch(paperForStudent); 
                    #endregion
                }
            }
            else
            {
                MessageBox.Show("請輸入電子報表名稱!!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
