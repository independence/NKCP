using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DataAccess;
using Entity;
using BussinessLogic;
using DevExpress.XtraScheduler.Native;
using DevExpress.XtraScheduler;


namespace RoomManager
{
    public partial class uc_StatusRooms : DevExpress.XtraEditors.XtraUserControl
    {
        public uc_StatusRooms()
        {
            InitializeComponent();
        }
        public List<RoomExtStatusEN> Datasource { set; get; }
        public int StatusButtonPopup = 0;
        private void Uc_ProcessImage_Load(object sender, EventArgs e)
        {
            //hiennv
            flowLayoutPanel1.AutoScroll = true;
        }

        public void DataBind()
        {
            uc_RoomStatusItem[] Item;
            Item = new uc_RoomStatusItem[Datasource.Count];
            flowLayoutPanel1.Controls.Clear();

            for (int i = 0; i < Datasource.Count; i++)
            {
                Item[i] = new uc_RoomStatusItem(this.Datasource[i]);
                Item[i].Visible = true;
                Item[i].StatusButtonPopup = this.StatusButtonPopup;
                flowLayoutPanel1.Controls.Add(Item[i]);
            }
        }
    }
}
