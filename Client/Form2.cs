using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace Client
{
    // Форма выбора сервера
    public partial class Form2 : Form
    {
        Form1 form1;

        Control[] input_menu_items;

        int editing_item_index = 0;
        bool editing = false;
        bool adding = false;

        bool connected = false;

        string filePath = "Servers.txt";

        public Form2()
        {
            InitializeComponent();

            input_menu_items = new Control[] { IPaddress_label, IPaddress_textBox, serverName_label, serverName_textBox, port_label, port_textBox};
        }

        // Метод вызываемый для начала выбора сервера
        public void getServer(Form1 form)
        {
            this.form1 = form;

            CenterToParent();
            Show();

            while (true)
            {
                int result = loadServersList();

                if (result == -1)
                    continue;
                else if (result == 0)
                {
                    Close();
                    return;
                }
                else
                    break;
            }          
        }

        // Загружает в элемент список серверов. Возвращает 1, если успешно; 0, если ошибка - закрытие программы; -1, если попроьовать снова.
        int loadServersList()
        {
            try
            {
                if (!File.Exists(filePath))
                    File.Create(filePath);

                string fileText = File.ReadAllText(filePath);

                if (fileText.Length == 0)
                    return 1;

                string[] servers = fileText.Split('~');

                servers_listView.Items.Clear();

                for (int i = 0; i < servers.Length; i++)
                {
                    string server = servers[i];
                    if (server == "")
                        break;

                    string[] lines = server.Split('\n');

                    servers_listView.Items.Add(lines[1]);
                    servers_listView.Items[i].SubItems.Add(lines[2]);
                    servers_listView.Items[i].SubItems.Add(lines[3]);
                }

                return 1;       
            }

            catch (FileNotFoundException)
            {
                DialogResult result = MessageBox.Show("Файл 'Servers.txt' не найден в каталоге с исполняемым файлом.\nПовторить попытку?", "File not found error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                if (result == DialogResult.Yes)
                    return -1;

                return 0;
            }

            catch (Exception e)
            {
                DialogResult result = MessageBox.Show(e.ToString() + "\nПовторить попытку?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                if (result == DialogResult.Yes)
                    return -1;

                return 0;
            }
        }

        // Нажатие на кнопку изменить
        private void edit_button_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection items = servers_listView.SelectedItems;

            if (items.Count < 1)
                MessageBox.Show("Выберите элемент для редактирования.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            else
            {
                editing = true;
                adding = false;

                editing_item_index = servers_listView.SelectedIndices[0];

                add_button.Text = "Ок";
                cancel_button.Visible = true;

                foreach (Control item in input_menu_items)
                {
                    item.Visible = true;
                }

                serverName_textBox.Text = items[0].SubItems[0].Text;
                IPaddress_textBox.Text = items[0].SubItems[1].Text;
                port_textBox.Text = items[0].SubItems[2].Text;
            }       
        }

        // Проверка введённых данных на правильность
        private bool check_data()
        {
            try
            {
                if (serverName_textBox.Text == "")
                {
                    MessageBox.Show("Имя сервера не может быть пустым.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return false;
                }

                foreach (ListViewItem item in servers_listView.Items)
                {
                    if (item.Index == editing_item_index && !adding)
                        continue;

                    if (serverName_textBox.Text == item.Text)
                    {
                        MessageBox.Show("Имя сервера занято.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        serverName_textBox.Text = "";
                        return false;
                    }
                }

                if (IPaddress_textBox.Text == "")
                {
                    MessageBox.Show("IP адрес сервера не может быть пустым.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return false;
                }
                IPAddress IPaddr;
                try
                {
                    IPaddr = IPAddress.Parse(IPaddress_textBox.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Неподходящий IP адрес сервера.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    IPaddress_textBox.Text = "";
                    return false;
                }

                if (port_textBox.Text == "")
                {
                    MessageBox.Show("Порт сервера не может быть пустым.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return false;
                }

                try
                {
                    int port = Convert.ToInt32(port_textBox.Text);

                    if (port < 0 || port > 65535)
                    {
                        MessageBox.Show("Неподходящий порт сервера.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return false;
                    }

                    new IPEndPoint(IPaddr, port);

                    foreach (ListViewItem item in servers_listView.Items)
                    {
                        if (item.Index == editing_item_index && !adding)
                            continue;

                        if (IPaddress_textBox.Text == item.Text && port_textBox.Text == item.SubItems[3].Text)
                        {
                            MessageBox.Show("Конечная точка занята.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            return false;
                        }
                    }
                }

                catch (Exception)
                {
                    MessageBox.Show("Неподходящий порт сервера.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return false;
                }

                return true;

            }
            catch (Exception)
            {
                MessageBox.Show("Что-то не так с данными.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Процедура редактирования
        private void edit_proc()
        {
            try
            {
                if (check_data() == false)
                    return;

                servers_listView.Items[editing_item_index].Text = serverName_textBox.Text;
                servers_listView.Items[editing_item_index].SubItems[1].Text = IPaddress_textBox.Text;
                servers_listView.Items[editing_item_index].SubItems[2].Text = port_textBox.Text;

                string fileText = File.ReadAllText(filePath);

                string[] servers = fileText.Split('~');

                servers[editing_item_index] = "\n" + serverName_textBox.Text + "\n" + IPaddress_textBox.Text + "\n" + port_textBox.Text + "\n";

                fileText = "";
                for (int i = 0; i < servers.Length; i++)
                {
                    fileText += servers[i];

                    if (i != servers.Length - 1)
                        fileText += '~';
                }

                File.WriteAllText(filePath, fileText);

                foreach (Control item in input_menu_items)
                {
                    item.Visible = false;
                }

                add_button.Text = "Добавить";
                cancel_button.Visible = false;
                editing = false;
            }
            catch (Exception)
            {
                MessageBox.Show("Что-то не так.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
        }
                
        // Нажатие на кнопку удалить
        private void delete_button_Click(object sender, EventArgs e)
        {
            try
            {
                ListView.SelectedListViewItemCollection items = servers_listView.SelectedItems;

                if (items.Count < 1)
                    MessageBox.Show("Выберите элемент для удаления.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                else
                {
                    int deleting_item_index = servers_listView.SelectedIndices[0];

                    DialogResult result = MessageBox.Show("Вы уверены?\nУдаление безвозвратное.", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                    if (result == DialogResult.Yes)
                    {
                        servers_listView.Items.RemoveAt(deleting_item_index);

                        string fileText = File.ReadAllText(filePath);

                        string[] servers = fileText.Split('~');

                        var servers_list = servers.ToList();
                        servers_list.RemoveAt(deleting_item_index);

                        fileText = "";
                        for (int i = 0; i < servers_list.Count; i++)
                        {
                            fileText += servers_list[i];

                            if (i != servers_list.Count - 1)
                                fileText += '~';
                        }

                        File.WriteAllText(filePath, fileText);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Что-то не так.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
        }

        // Нажатие на кнопку редактировать
        private void connect_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (servers_listView.SelectedItems.Count != 0)
                {
                    string[] server = { "", "" };

                    server[0] = servers_listView.SelectedItems[0].SubItems[1].Text;
                    server[1] = servers_listView.SelectedItems[0].SubItems[2].Text;

                    bool result = ClientMain.tryToConnect(server);
                    connected = result;

                    if (result)
                    {
                        MessageBox.Show("Подключение успешно.", "Connected", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        form1.setServer(server);

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Не удалось подключиться к серверу.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                    MessageBox.Show("Выберите сервер.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
                        
        }

        // Реакция на закртиые формы
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (connected)
            {
                Owner.Enabled = true;
            }
            else
            {
                DialogResult result = MessageBox.Show("Нет подключения к серверу!\nНевозможна дальнейшая работа программы.\nВы точно хотите выйти?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                if (result == DialogResult.Yes)
                {
                    form1.setServer(null);
                    Owner.Enabled = true;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        // Нажатие на кнопку добавить
        private void add_button_Click(object sender, EventArgs e)
        {
            try
            {
                // Если происходит редактирование
                if (editing)
                {
                    edit_proc();
                }
                // Если нажали на Добавить
                else if (!editing && !adding)
                {
                    add_button.Text = "Ок";
                    cancel_button.Visible = true;

                    foreach (Control item in input_menu_items)
                    {
                        item.Visible = true;
                    }

                    serverName_textBox.Text = "";
                    IPaddress_textBox.Text = "";
                    port_textBox.Text = "";

                    adding = true;
                }
                // Если происходит добавление
                else if (adding && !editing)
                {                   
                    if (check_data() == false)
                        return;

                    string fileText = File.ReadAllText(filePath);
                    if (!(fileText == ""))
                        fileText += "~";
                    fileText += "\n" + serverName_textBox.Text + "\n" + IPaddress_textBox.Text + "\n" + port_textBox.Text + "\n";

                    File.WriteAllText(filePath, fileText);

                    ListViewItem server = new ListViewItem(serverName_textBox.Text);
                    server.SubItems.Add(IPaddress_textBox.Text);
                    server.SubItems.Add(port_textBox.Text);
                    servers_listView.Items.Add(server);

                    foreach (Control item in input_menu_items)
                    {
                        item.Visible = false;
                    }

                    add_button.Text = "Добавить";
                    cancel_button.Visible = false;
                    editing = false;
                    adding = false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Что-то не так.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                
                return;
            }
        }

        // Нажатие на кнопку отменить
        private void cancel_button_Click(object sender, EventArgs e)
        {
            foreach (Control item in input_menu_items)
            {
                item.Visible = false;
            }

            add_button.Text = "Добавить";
            cancel_button.Visible = false;
            editing = false;
            adding = false;
        }
    }
}
