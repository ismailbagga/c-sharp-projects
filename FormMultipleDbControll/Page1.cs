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
    public partial class Page1 : Form
    {

        private BindingManagerBase bindginManagerBase  ; 
        private Boolean isBinded = true ; 
        public Page1()
        {
            InitializeComponent();
        }
        private void Page1_Load(object sender, EventArgs e)
          {

         
            Utils.loadData(); 
            bindginManagerBase =this.BindingContext[Utils.dataTable];
      
            textBox2.DataBindings.Add("text", Utils.dataTable, "person_name", false, DataSourceUpdateMode.Never);
            textBox1.DataBindings.Add("text", Utils.dataTable, "id",false,DataSourceUpdateMode.Never);
            textBox3.DataBindings.Add("text", Utils.dataTable, "person_address", false, DataSourceUpdateMode.Never);
           
            dataGridView1.DataSource = Utils.dataTable;
           
            //Utils.closeConnection();

        }
        

        private void button8_Click(object sender, EventArgs e)
        {
            //isBinded = false; 
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();



        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            bindginManagerBase.Position--; 
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
            bindginManagerBase.Position++; 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            bindginManagerBase.Position = 0; 
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
            bindginManagerBase.Position = bindginManagerBase.Count - 1;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // add Btn 
            int id = Int32.Parse(textBox1.Text);
            string nom = textBox2.Text; 
            string address = textBox3.Text;
            if (Utils.insertCommand(id, nom, address)) MessageBox.Show("Person inserted");
            else MessageBox.Show("Person avec cette id  deja existe"); 
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // delete btn 
            int id = Int32.Parse(textBox1.Text);
            if (Utils.deleteCommand(id)) MessageBox.Show("suppremier effectue");
            else MessageBox.Show("cette id n existe pas");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(textBox1.Text);
            string nom = textBox2.Text;
            string address = textBox3.Text;
            if (Utils.updateCommand(id, nom, address)) MessageBox.Show("Miss a jour effectue");
            else MessageBox.Show("cette id n exist pas");
        }
    }
}
