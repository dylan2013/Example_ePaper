using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.Presentation;
using FISCA;

namespace ClassStudent_ePaper
{
    public class Program
    {
        [MainMethod]
        public static void Main()
        {

            RibbonBarItem item = MotherForm.RibbonBarItems["班級", "資料統計"];
            item["報表"]["外掛報表"]["電子報表簡單範例"].Click += delegate
            {
                Form1 F = new Form1();
                F.ShowDialog();
            };

            item = MotherForm.RibbonBarItems["班級", "資料統計"];
            item["報表"]["外掛報表"]["刪除班級電子報表範例"].Click += delegate
            {
                DataGridViewForm F = new DataGridViewForm();
                F.ShowDialog();
            };

        }
    }
}
