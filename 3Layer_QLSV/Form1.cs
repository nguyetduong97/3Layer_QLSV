using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLogic;
using System.IO;

namespace _3Layer_QLSV
{
    public partial class frmQLSV : Form
    {
        string path = "../../Hinh";
        XL_SINHVIEN Bang_SINHVIEN;
        XL_LOP Bang_LOP;
        BindingManagerBase DSSV;
        public frmQLSV()
        {
            InitializeComponent();
        }

        private void frmQLSV_Load(object sender, EventArgs e)
        {
            Bang_SINHVIEN = new XL_SINHVIEN();
            Bang_LOP = new XL_LOP();
            loadcobLop();
            LoadgvSinhVien();
            txtMaSV.DataBindings.Add("text", Bang_SINHVIEN, "MaSV", true);
            txtHoTen.DataBindings.Add("text", Bang_SINHVIEN, "HoTen", true);
            dateTimePicker1.DataBindings.Add("text", Bang_SINHVIEN, "NgaySinh", true);
            radNam.DataBindings.Add("checked", Bang_SINHVIEN, "GioiTinh", true);
            cobLop.DataBindings.Add("SelectedValue", Bang_SINHVIEN, "MaLop", true);
            txtDiaChi.DataBindings.Add("text", Bang_SINHVIEN, "DiaChi", true);
            picChonHinh.DataBindings.Add("ImageLocation", Bang_SINHVIEN, "Hinh", true);
            DSSV = this.BindingContext[Bang_SINHVIEN];
            EnabledNutLenh(false);
        }

        private void EnabledNutLenh(bool pCapNhat)
        {
            btthem.Enabled = !pCapNhat;
            btXoa.Enabled = !pCapNhat;
            btSua.Enabled = !pCapNhat;
            btThoat.Enabled = !pCapNhat;
            btLuu.Enabled = pCapNhat;
            btHuy.Enabled = pCapNhat;
        }

        private void loadcobLop()
        {
            cobLop.DataSource = Bang_LOP;
            cobLop.DisplayMember = "TenLop";
            cobLop.ValueMember = "MaLop";
        }

        private void LoadgvSinhVien()
        {
            dgvSinhVien.AutoGenerateColumns = false;
            dgvSinhVien.DataSource = Bang_SINHVIEN;
        }

        private void radNam_CheckedChanged(object sender, EventArgs e)
        {
            radNu.Checked = !radNam.Checked;
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            try
            {
                DSSV.EndCurrentEdit();
                Bang_SINHVIEN.Ghi();
                EnabledNutLenh(false);
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btthem_Click(object sender, EventArgs e)
        {
            DSSV.AddNew();
            EnabledNutLenh(true);
        }

        private void btHuy_Click(object sender, EventArgs e)
        {
                DSSV.EndCurrentEdit();
                Bang_SINHVIEN.RejectChanges();
                EnabledNutLenh(false);
            
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            DSSV.RemoveAt(DSSV.Position);
            if(!Bang_SINHVIEN.Ghi())
            {
                MessageBox.Show("Xóa Thất Bại!");
            }
        }

        private void btSua_Click(object sender, EventArgs e)
        {
            try
            {
                DSSV.EndCurrentEdit();
                Bang_SINHVIEN.Ghi();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow r = Bang_SINHVIEN.Select("MaSV= '" + txtTimKiem.Text + "'")[0];
                DSSV.Position = Bang_SINHVIEN.Rows.IndexOf(r);


            }catch(Exception ex)
            {
                MessageBox.Show("Không Tìm Thấy!");
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTimKiem_MouseDown(object sender, MouseEventArgs e)
        {
            txtTimKiem.Text = "";
        }

        private void btChonHinh_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "JPG Filter|*.jpg|PNG Filter|*.png|ALL Filter|*.*";
            if(openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                string fileName = openFileDialog1.SafeFileName;
                string pathFile = path + "/" + fileName;
                if (!File.Exists(pathFile))
                    File.Copy(openFileDialog1.FileName, pathFile);
                picChonHinh.ImageLocation = pathFile;
            }
        }

        private void btThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
