using System;
using System.Linq;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        // Дополнительные формы
        Form2 form2;
        HelpForm help_form = null;
        AboutForm about_form = null;

        // Данные сервера для подключения
        string[] server;

        // Полученный словарь
        string[][] dictionary = null;

        public Form1()
        {
            InitializeComponent();
            
            this.StartPosition = FormStartPosition.CenterScreen;           
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            // Вызывается выбор сервера и блокируется исходная форма
            form2 = new Form2();
            form2.Owner = this;
            
            this.Enabled = false;

            form2.getServer(this);            
        }

        // Метод для задания сервера
        public void setServer(string[] server)
        {
            this.server = server;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        // Кнопка поиск
        private void button1_Click(object sender, EventArgs e)
        {
            startSearch();
        }
        // Кнопка получить весь словарь
        private void button2_Click(object sender, EventArgs e)
        {
            startSearch(1);
        }
        
        // Метод поиска
        private void startSearch(int searchFullDictionary = 0)
        {
            // Если сервер не задан
            if (server == null)
            {
                MessageBox.Show("Сервер не выбран!\nВыберите сервер, нажав на соответствующий пункт меню.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string words;
            if (searchFullDictionary == 1)
            {
                words = "a";
            }
            else
                words = getWordsToSearch();

            if (words == null)
                return;


            TermsListBox.Items.Clear();
            descriptionTextBox.Text = "";

            int searchInDescription = 0;
            if (checkBox1.Checked)
                searchInDescription = 1;


            // Получить данные от сервера
            string dataFromDictionary = ClientMain.sendDataToServer(searchInDescription, searchFullDictionary, words, server);

            if (dataFromDictionary == null || dataFromDictionary == "")
                return;
            
            if (dataFromDictionary[0] == '!')
            {
                MessageBox.Show(dataFromDictionary.Substring(1), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Конвертирование в удобный формат
            dictionary = convertToDictionary(dataFromDictionary);

            // Добавление в список терминов
            foreach(string term in dictionary[0])
            {
                TermsListBox.Items.Add(term);
            }
        }

        // Перевод данных в удобный формат
        private string[][] convertToDictionary(string dataFromDict)
        {
            if (dataFromDict == null || dataFromDict.Length == 0)
                return null;

            string[] terms = dataFromDict.Split('~');

            string[][] dictionary = new string[2][];
            dictionary[0] = new string[terms.Length];
            dictionary[1] = new string[terms.Length];

            /*this.dictionary = new string[2][];
            this.dictionary[0] = new string[terms.Length];
            this.dictionary[1] = new string[terms.Length];*/

            for (int i = 0; i < terms.Length; i++)
            {
                string term = terms[i].Remove(0, 1);
                term = term.Remove(term.Length - 1, 1);

                dictionary[0][i] = term.Substring(0, term.IndexOf('/'));
                dictionary[1][i] = term.Substring(term.IndexOf('\n', 1) + 1);
                
            }

            return dictionary;
        }

        // Получение слов для поиска из текстового поля
        private string getWordsToSearch()
        {
            string words = SearchWordsTextBox.Text;

            if (words == "" || words.All(Char.IsPunctuation))
            {
                MessageBox.Show("Значение для поиска должно содержать буквы или цифры.\nЕсли вы хотите увидеть весь словарь, нажмите на соответствующую кнопку.", "Value Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;
            }

            words.Replace('\n', ' ');
                
            return words;
        }

        private void TermsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            descriptionTextBox.Text = dictionary[1][TermsListBox.SelectedIndex];
        }

        private void SearchWordsTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                startSearch();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            form2 = new Form2();
            form2.Owner = this;

            this.Enabled = false;

            form2.getServer(this);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (help_form == null || help_form.Visible == false)
            {
                help_form = new HelpForm();
                help_form.Owner = this;                

                help_form.Show();
            }

            else
                help_form.Select();          
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (about_form == null || about_form.Visible == false)
            {
                about_form = new AboutForm();
                about_form.Owner = this;

                about_form.Show();
            }

            else
                about_form.Select();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            label2.Select();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }
    }
}
