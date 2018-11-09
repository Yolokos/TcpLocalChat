using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpClientProtocol
{
    class Program
    {
        const int port = 8888; // порт для прослушивания подключений
        const string localAddr = "127.0.0.1";
        static void Main(string[] args)
        {
            Console.Write("Your name: ");
            string userName = Console.ReadLine();
            TcpClient client = null;
            try {
                client = new TcpClient(localAddr, port);
                NetworkStream stream = client.GetStream();
                Console.WriteLine($"Input your message {userName}");
                while (true)
                {
                    do
                    {
                        Console.Write($"{userName}: ");
                        string message = Console.ReadLine();
                        message = String.Format("{0}: {1}", userName, message);
                        byte[] data = Encoding.Unicode.GetBytes(message);
                        stream.Write(data, 0, data.Length);
                        //Console.WriteLine("Message sended");

                    }
                    while (stream.DataAvailable);

                    do
                    {
                        byte[] data = new byte[64];
                        StringBuilder builder = new StringBuilder();
                        int bytes = 0;
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                        string message = builder.ToString();
                        Console.WriteLine($"{message}");
                    }
                    while (stream.DataAvailable);

                }

                //stream.Close();

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if(client != null)
                {
                    client.Close();
                }
            }
        }
    }
    
}
