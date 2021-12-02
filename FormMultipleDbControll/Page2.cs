using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormMultipleDbControll
{
    public partial class Page2 : Form
    {
         BindingManagerBase bindingManagerBase;
        List<string> idList = new List<string>() ; 
        public Page2()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        public void fillIdList()
        {
            foreach (DataRow row in Utils.dataTable.Rows )
            {

                string id = row["id"].ToString();
                idList.Add(id); 
            }
        }
        private void Page2_Load(object sender, EventArgs e)
        {
           
            Utils.loadData();
            bindingManagerBase = this.BindingContext[Utils.dataTable];
            fillIdList(); 
            setCBDataSource();
            textBox1.DataBindings.Add("text", Utils.dataTable, "person_name", false, DataSourceUpdateMode.Never);
            textBox2.DataBindings.Add("text", Utils.dataTable, "person_address", false, DataSourceUpdateMode.Never);
            dataGridView1.DataSource = Utils.dataTable;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

         
     

        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            comboBox1.SelectedIndex =e.RowIndex;

        }

        private void Add(object sender, EventArgs e)
        {

            
        }
        public void  setCBDataSource()
        {
            comboBox1.DataSource = Utils.dataTable.AsEnumerable().Select(row => row[0]).ToList();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            comboBox1.Text = ""; 
            textBox1.Text = textBox2.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(comboBox1.Text);
            string nom = textBox1.Text;
            string address = textBox2.Text;
            if (Utils.insertCommand(id, nom, address))
            {
                MessageBox.Show("insertion effectue");
                setCBDataSource();

            }
            else MessageBox.Show("cette id deja existe");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindingManagerBase.Position = comboBox1.SelectedIndex;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(comboBox1.Text);
            string nom = textBox1.Text;
            string address = textBox2.Text;
            if (Utils.updateCommand(id, nom, address))
            {
                MessageBox.Show("Miss a jour effectue");
            }
            else MessageBox.Show("cette id  n'existe pas");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(comboBox1.Text);
            
            if (Utils.deleteCommand(id))
            {
                MessageBox.Show("delete effectue");
                setCBDataSource();

            }
            else MessageBox.Show("cette id n'existe pas");
        }
    }
}
