using System;
using System.Text;

using System.Net.Sockets;
using System.Net;

using System.Threading;

// Получаем от клиента форматную строку: массив слов, которые вместе в любом порядке должны быть в искомой строке.
// Поиск может идти в термине и в его определении; 0 - в термине, 1 - в определении
// После текста DictionaryRequest: 0, если искать определённые слова и 1, если нужны все термины из словаря
// Получаемые данные выглядят так: "DictionaryRequest\n0\nw1/w2/w3\n0"

namespace Server
{
    class ServerMain
    {
        static string filePath = "Dictionary.txt";
        string address = "";
        string port = "";
        static string server_version = "1.0";

        bool activated = false;
        static void Main(string[] args)
        {
            ServerMain this_class = new ServerMain();

            Console.WriteLine("---Словарь терминов и сокращений по модели OSI. Сервер---\nВерсия " + server_version + "\n");
            while (true)
            {
                while (true)
                {
                    if (this_class.CheckData())
                        break;
                }

                this_class.ServerLogic();

                if (this_class.activated)
                    break;
            }
            return;
        }

        // Ввод данных сервера
        bool CheckData()
        {
            try
            {
                IPAddress IPaddr = null;

                if (this.address == "")
                {
                    Console.WriteLine("\nВведите IPv4 адрес, чтобы настроить сервер: ");

                    string address = Console.ReadLine();

                    if (address == "")
                    {
                        Console.WriteLine("IP адрес сервера не может быть пустым.\n");
                        return false;
                    }
                    
                    try
                    {
                        IPaddr = IPAddress.Parse(address);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Неподходящий IP адрес сервера.\n");
                        this.address = "";
                        return false;
                    }

                    this.address = address;
                }

                if (this.port == "")
                {
                    Console.WriteLine("\nВведите номер порта, чтобы настроить сервер: ");

                    string port = Console.ReadLine();

                    if (port == "")
                    {
                        Console.WriteLine("Порт сервера не может быть пустым.\n");
                        return false;
                    }

                    try
                    {
                        int _port = Convert.ToInt32(port);

                        if (_port < 0 || _port > 65535)
                        {
                            Console.WriteLine("Неподходящий порт сервера.\n");
                            this.port = "";
                            return false;
                        }

                        new IPEndPoint(IPaddr, _port);

                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Неподходящий порт сервера.\n");
                        this.port = "";
                        return false;
                    }

                    this.port = port;
                }

                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Что-то не так с данными.\n");
                this.address = "";
                this.port = "";
                return false;
            }
        }

        // Основная логика сервера
        void ServerLogic()
        {
            IPAddress ipAddr = IPAddress.Parse(address);
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, Convert.ToInt32(port));
            
            Socket sListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                sListener.Bind(ipEndPoint);
                sListener.Listen(10);

                activated = true;

                while (true)
                {
                    Console.WriteLine("\nWaiting for connection on IP address {0}{1}{2}{3}{4}", ipEndPoint.Address, " | Port : ", ipEndPoint.Port, " | Descriptor : ", sListener.Handle);

                    Socket handler = sListener.Accept();

                    Thread thread = new Thread(new ParameterizedThreadStart(ClientThread));

                    thread.Start(handler);

                    /*handler.Shutdown(SocketShutdown.Both);
                    handler.Close();*/
                }
            }
            catch (Exception e)
            {
                activated = false;
                this.address = "";
                this.port = "";
                Console.WriteLine("Exception: {0}", e.ToString());
            }
        }

        // Поток для отдельного клиента
        static void ClientThread(Object obj)
        {
            Socket client = (Socket)obj;

            Console.WriteLine("Connected to {0}{1}{2}", client.RemoteEndPoint.ToString(), " | New Descriptor : ", client.Handle);

            string theRequest = "DictionaryRequest";
            string theAnswer = "DictionaryAnswer";

            string data = null;
            while (true)
            {
                byte[] bytes = new byte[2048];
                int bytesRec = client.Receive(bytes);
                data += Encoding.Unicode.GetString(bytes, 0, bytesRec);
                if (data.IndexOf("<TheEnd>") > -1)
                {
                    break;
                }
            }

            Console.WriteLine("Text Received: {0}", data);

            if (data.Contains(theRequest))
            {
                data = data.Remove(0, theRequest.Length + 1);
                data = data.Remove(data.IndexOf("<TheEnd>"), 8);

                string dataToSend = Dictionary.FindWordsInDictionary(data, filePath);

                byte[] msg = Encoding.Unicode.GetBytes(theAnswer + dataToSend + "<TheEnd>");
                client.Send(msg);

                Console.WriteLine("\nText Sent: {0}", dataToSend);
            }
            else if (data.Contains("ConnectRequest"))
            {
                byte[] msg = Encoding.Unicode.GetBytes("ConnectAnswer<TheEnd>");
                client.Send(msg);

                Console.WriteLine("\nText Sent: {0}", "ConnectAnswer<TheEnd>");
            }
            else
            {
                byte[] msg = Encoding.Unicode.GetBytes("UnknownRequest<TheEnd>");
                client.Send(msg);
            }

            client.Shutdown(SocketShutdown.Both);
            client.Close();
        }
    }
}
