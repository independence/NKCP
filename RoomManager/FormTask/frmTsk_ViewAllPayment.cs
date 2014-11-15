using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DataAccess;
namespace RoomManager
{
    public partial class frmTsk_ViewAllPayment : DevExpress.XtraEditors.XtraForm
    {
        public frmTsk_ViewAllPayment()
        {
            InitializeComponent();
        }

        private void frmTsk_ViewAllPayment_Load(object sender, EventArgs e)
        {
            DatabaseDA aDatabaseDA = new DatabaseDA();
            //DatabaseDA aDatabaseDA = new DatabaseDA();
            grdViewAllPayment.DataSource = aDatabaseDA.sp_PaymentExt_GetAllData().ToList();
        }
    }
}