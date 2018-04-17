using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace Tuan_2._1
{
    public partial class Form1 : Form
    {
        Socket newsock;
        Socket client;
        IPEndPoint ipe;
        int connections = 0;
        public Form1()
        {
            InitializeComponent();
            ipe = new IPEndPoint(IPAddress.Any, 9050);
            newsock = new Socket(AddressFamily.InterNetwork,
                            SocketType.Stream, ProtocolType.Tcp);
            newsock.Bind(ipe);
            newsock.Listen(10);
            listBox1.Items.Add("Waiting for clients...");
            client = newsock.Accept();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
                byte[] data = new byte[1024];
                data = Encoding.ASCII.GetBytes(textBox2.Text);
                client.Send(data,data.Length,SocketFlags.None);
                listBox1.Items.Add(textBox2.Text.ToString());
                        Thread newthread = new Thread(new ThreadStart(HandleConnection));
                        newthread.Start();
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void HandleConnection()
        {
            while (true)
            {
                byte[] data = new byte[1024];
                int recv = client.Receive(data, SocketFlags.None);
                string stringData = Encoding.ASCII.GetString(data, 0, recv);
                listBox1.Items.Add(stringData.Substring(0, recv));
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                byte[] data = new byte[1024];
                data = Encoding.ASCII.GetBytes(textBox2.Text);
                client.Send(data, data.Length, SocketFlags.None);
                listBox1.Items.Add(textBox2.Text.ToString());
                Thread newthread = new Thread(new ThreadStart(HandleConnection));
                newthread.Start();
                textBox2.Clear();
                SendKeys.Send(textBox2.Text);
                e.SuppressKeyPress = true;
                textBox2.Focus();
            }
        }
    }
}
