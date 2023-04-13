using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tren_3.Properties;

namespace tren_3
{
    public partial class Form1 : Form
    {
        User02_04Entities entities = new User02_04Entities();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        int allproductCount;
        List<Product> pr;

        public void Sort() 
        {
            pr = entities.Product.ToList();

            switch (comboBox1.SelectedIndex) 
            {
                case 0:

                    pr = pr.ToList();
                    allproductCount = pr.Count;
                    break;
                case 1:
                    pr = pr.OrderBy(x => Convert.ToInt32(x.Price)).ToList();
                    break;
                case 2:
                    pr = pr.OrderByDescending(x => Convert.ToInt32(x.Price)).ToList();
                    break;
            }
            switch (comboBox2.SelectedIndex) 
            {
                case 0:
                    pr = pr.ToList();
                    break;

                case 1:
                    pr = pr.Where(x => Convert.ToInt32(x.DiscountNow) > 2).ToList();
                    break;

                case 2:
                    pr = pr.Where(x => Convert.ToInt32(x.DiscountNow) >= 4).ToList();
                    break;
            }

            for (int i = 0; i < pr.Count; i++) 
            {
                dataGridView1.Rows.Add();


                string photoname = pr.Select(x => x.ProductImage).ToArray()[i];
                if(String.IsNullOrEmpty(photoname))
                    dataGridView1.Rows[i].Cells[0].Value = Resources.pictur;
                else
                    dataGridView1.Rows[i].Cells[0].Value = Resources.ResourceManager.GetObject(photoname);

                dataGridView1.Rows[i].Cells[1].Value = $"Назваение товара: {pr.Select(x => x.Name).ToArray()[i]}\n" +
                    $"{pr.Select(x => x.Manufacture.ManufactureName).ToArray()[i]}";

                dataGridView1.Rows[i].Cells[2].Value = pr.Select(x => x.Price).ToArray()[i];
                dataGridView1.Rows[i].Cells[3].Value = pr.Select(x => x.DiscountNow).ToArray()[i];
            }

            label1.Text = dataGridView1.Rows.Count + " из " + allproductCount;


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            Sort();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            Sort();
        }
    }
}
