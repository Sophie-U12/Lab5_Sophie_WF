using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5_Sophie_WF
{
    public partial class Form1 : Form
    {
        private List<Cloakroom> _cloakrooms;
        public Form1()
        {
            InitializeComponent();
            _cloakrooms = new List<Cloakroom>();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _cloakrooms.Add(new Cloakroom(tbName.Text, tbSurname.Text, tbPatronymic.Text, (int)nudNumberOfThings.Value,
                (double)nudThingsWeight.Value));
            lbItems.Items.Add(_cloakrooms[_cloakrooms.Count - 1]);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<Cloakroom> tempCloakroom = _cloakrooms.Where((double)nudSearchThingWeight.Value);
            lbSearchItems.Items.Clear();

            for(int i = 0; i < tempCloakroom.Count; i++)
            {
                lbSearchItems.Items.Add(tempCloakroom[i].ToString());
            }
        }

        private void btnReadFromFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            openFileDialog.Title = "Відкрити JSON файл";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                lbItems.Items.Clear();
                try
                {
                    string json = "";

                    using (Stream stream = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                        {
                            while (!reader.EndOfStream)
                            {
                                json += reader.ReadLine();
                            }
                        }
                    }
                    JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
                    serializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    serializerSettings.TypeNameHandling = TypeNameHandling.All;

                    Cloakroom[] array = JsonConvert.DeserializeObject<Cloakroom[]>(json, serializerSettings);
                    _cloakrooms = new List<Cloakroom>();
                    for (int i = 0; i < array.Length; i++)
                    {
                        _cloakrooms.Add(array[i]);
                        lbItems.Items.Add(_cloakrooms[i].ToString());
                    }
                }
                catch
                {
                    _cloakrooms = new List<Cloakroom>();
                    MessageBox.Show("Помилка завантаження!");
                }
            }
        }

        private void btnSaveToFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            saveFileDialog.Title = "Зберегти як JSON файл";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _cloakrooms.SaveToFile(saveFileDialog.FileName);
                }
                catch
                {
                    MessageBox.Show("Помилка зберегання!");
                }
            }
        }

        private void btnRemoveAt_Click(object sender, EventArgs e)
        {
            if(lbItems.SelectedIndex != -1)
            {
                _cloakrooms.RemoveAt(lbItems.SelectedIndex);
                lbItems.Items.RemoveAt(lbItems.SelectedIndex);
            }
            else
            {
                MessageBox.Show("Помилка видалення!");
            }
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            List<Cloakroom> temp = new List<Cloakroom>();
            for(int i = 0; i < _cloakrooms.Count; i++)
            {
                temp.Add(_cloakrooms[i]);
            }

            temp.Sort();
            lbSortItems.Items.Clear();

            for(int i = 0; i < temp.Count; i++)
            {
                lbSortItems.Items.Add(temp[i].ToString());
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
