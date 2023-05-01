using System;
using System.Linq;
using System.Text;

using System.Net.Sockets;
using System.Net;

namespace Client
{
    // Поиск может идти в термине и в его определении; 0 - в термине, 1 - в определении
    // После текста DictionaryRequest: 0, если искать определённые слова и 1, если нужны все термины из словаря
    // Отправляемые данные выглядят так: "DictionaryRequest\n0\nw1/w2/w3\n0"
    class ClientMain
    {
        public static IPEndPoint ipEndPoint;
        // Метод, вызываемый для отправки запроса на сервер
        public static string sendDataToServer(int searchInDescription, int searchFullDictionary, string words, string[] server)
        {
            if (words == null || (searchInDescription != 0 && searchInDescription != 1) || (searchFullDictionary != 0 && searchFullDictionary != 1))
                return null;

            string data = (searchFullDictionary.ToString() + '\n');

            bool wasAWord = false;

            foreach (string word in words.Split(' '))
            {
                if (word == "" || word.All(Char.IsPunctuation))
                    continue;
                if (!wasAWord)
                {
                    data += word;
                    wasAWord = true;
                }
                else
                    data += ('/' + word);
            }

            data += ('\n' + searchInDescription.ToString());

            data = ClientLogic(data, server);

            return data;
        }
        // Основная логика клиента
        static string ClientLogic(string dataToSend, string[] server)
        {
            byte[] bytes = new byte[2048];

            try
            {
                if (server.Length < 2)
                    return "!Failed to connect to server.";
                else
                {
                    if (server[0] == "" || server[1] == "")
                        return "!Failed to connect to server.";
                }

                IPAddress address = IPAddress.Parse(server[0]);
                IPEndPoint ipEndPoint = new IPEndPoint(address, Convert.ToInt32(server[1]));

                Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sender.Connect(ipEndPoint);

                string theRequest = "DictionaryRequest";
                string theAnswer = "DictionaryAnswer";

                byte[] msg = Encoding.Unicode.GetBytes(theRequest + '\n' + dataToSend + "<TheEnd>");
                sender.Send(msg);

                string data = null;

                while (true)
                {
                    int bytesRec = sender.Receive(bytes);
                    data += Encoding.Unicode.GetString(bytes, 0, bytesRec);

                    if (data.IndexOf("<TheEnd>") > -1)
                    {
                        break;
                    }
                }

                if (data.Contains(theAnswer))
                {
                    data = data.Remove(0, theAnswer.Length);
                    data = data.Remove(data.Length - 8);
                }
                else
                    return null;

                if (data == "")
                    return "!Nothing has been found.";

                sender.Shutdown(SocketShutdown.Both);
                sender.Close();

                return data;
            }
            catch (Exception)
            {
                return "!Failed to connect to server.";
            }
        }

        // Метод дяя пробной попытки подключиться к серверу
        public static bool tryToConnect(string []server)
        {
            byte[] bytes = new byte[2048];

            try
            {
                if (server.Length < 2)
                    return false;
                else
                {
                    if (server[0] == "" || server[1] == "")
                        return false;
                }

                IPAddress address = IPAddress.Parse(server[0]);
                IPEndPoint ipEndPoint = new IPEndPoint(address, Convert.ToInt32(server[1]));

                Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);              

                System.Threading.Thread T = new System.Threading.Thread((new System.Threading.ParameterizedThreadStart(tryToSafetyConnect)));
                ClientMain.ipEndPoint = ipEndPoint;

                DateTime date = DateTime.Now;
                DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                TimeSpan diff = date.ToUniversalTime() - origin;
                double timeout = Math.Floor(diff.TotalSeconds) + 2;

                T.Start(sender);

                while (true)
                {
                    date = DateTime.Now;
                    origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                    diff = date.ToUniversalTime() - origin;

                    if (Math.Floor(diff.TotalSeconds) >= timeout || T.IsAlive == false)
                        break;
                }

                if (T.IsAlive)
                {
                    T.Abort();

                    return false;
                }

                string theRequest = "ConnectRequest";
                string theAnswer = "ConnectAnswer";

                byte[] msg = Encoding.Unicode.GetBytes(theRequest + "<TheEnd>");
                sender.Send(msg);

                string data = "";
                while (true)
                {
                    int bytesRec = sender.Receive(bytes);

                    data += Encoding.Unicode.GetString(bytes, 0, bytesRec);

                    if (data.IndexOf("<TheEnd>") > -1)
                    {
                        break;
                    }
                }

                if (!data.Contains(theAnswer))
                    return false;

                sender.Shutdown(SocketShutdown.Both);
                sender.Close();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Метод для безопасного подключения. Вызывается в отдельном потоке
        static void tryToSafetyConnect(Object obj1)
        {
            Socket sender = (Socket)obj1;
            
            sender.Connect(ipEndPoint);
        }

    }
}
