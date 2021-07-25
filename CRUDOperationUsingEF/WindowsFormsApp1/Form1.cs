using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        EmployeeEntities ctx = new EmployeeEntities();
        Employee employee = new Employee();
        int EmpId = 0;
        private void Form1_Load(object sender, EventArgs e)
        {

            ClearData();
            SetDataGridView();
        }

        private void SetDataGridView()
        {
            //dataGridView2.AutoGenerateColumns = false;
            dataGridView2.DataSource = ctx.Employees.ToList<Employee>();            

        }

        private void ClearData()
        {
            txtEmpAdd.Text = txtEmpAge.Text = txtEmpCity.Text = txtEmpName.Text = txtEmpSalary.Text = string.Empty;
            btnDelete.Enabled = false;
            btnSave.Text = "Save";
            EmpId = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            employee.EmployeeName = txtEmpName.Text.Trim();
            employee.EmployeeAge = Convert.ToInt32(txtEmpAge.Text.Trim());
            employee.EmployeeAddress = txtEmpAdd.Text.Trim();
            employee.EmployeeCity = txtEmpCity.Text.Trim();
            employee.EmployeeSalary = Convert.ToInt32(txtEmpSalary.Text.Trim());

            if (EmpId > 0)
                ctx.Entry(employee).State = EntityState.Modified;
            else
            {
                ctx.Employees.Add(employee);
            }

            ctx.SaveChanges();
            ClearData();
            SetDataGridView();
            MessageBox.Show("Record Save Successfully");
        }

        private void dataGridView2_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentCell.RowIndex != -1)
            {
                EmpId = Convert.ToInt32(dataGridView2.CurrentRow.Cells["EmployeeId"].Value);
                employee = ctx.Employees.Where(x => x.EmployeeId == EmpId).FirstOrDefault();
                txtEmpName.Text = employee.EmployeeName;
                txtEmpAdd.Text = employee.EmployeeAddress;
                txtEmpAge.Text = employee.EmployeeAge.ToString();
                txtEmpSalary.Text = employee.EmployeeSalary.ToString();
                txtEmpAge.Text = employee.EmployeeAge.ToString();
                txtEmpCity.Text = employee.EmployeeCity;
            }
            btnSave.Text = "Update";
            btnDelete.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this record ?", "Delete ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ctx.Employees.Remove(employee);
                ctx.SaveChanges();
                ClearData();
                SetDataGridView();
                MessageBox.Show("Record Deleted Successfully");
            }
        }
    }
}
