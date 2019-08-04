using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace NetworkTicTacToe
{
    public partial class Form1 : Form
    {
        TcpClient connection;
        private Button[,] tictactoe = new Button[3, 3];
        public int turn;
        public List<Button> btns;

        public Form1()
        {
            InitializeComponent();
            this.tictactoe[0, 0] = this.btn00;
            this.tictactoe[0, 1] = this.btn01;
            this.tictactoe[0, 2] = this.btn02;
            this.tictactoe[1, 0] = this.btn10;
            this.tictactoe[1, 1] = this.btn11;
            this.tictactoe[1, 2] = this.btn12;
            this.tictactoe[2, 0] = this.btn20;
            this.tictactoe[2, 1] = this.btn21;
            this.tictactoe[2, 2] = this.btn22;
            btns = new List<Button>()
            {
                btn00, btn01, btn02,
                btn10, btn11, btn12,
                btn20, btn21, btn22
            };

            turn = 0;

            foreach (Button btn in btns)
            {
                btn.Click += (o, e) =>
                {
                    int[] coords = getCoords(btn.Name);
                    btn.Text = "X";
                    btn.Enabled = false;
                    SendCoordinates(coords);
                    CheckWin();
                    turn++;
                    TheirTurn();
                };
            }
        }

        private int[] getCoords(string button)
        {
            int x = 0;
            int y = 0;
            if (int.TryParse(button[button.Length - 2].ToString(), out x)) { };
            if (int.TryParse(button[button.Length - 1].ToString(), out y)) { };

            int[] coords =new int[] { x, y };

            return coords;
        }

        private void opponentMove(string coords)
        {
            List<int> xy = new List<int>();
            foreach (char c in coords)
            {
                if (int.TryParse(c.ToString(), out int val))
                {
                    xy.Add(val);
                }
            }

            if (tictactoe[xy[0], xy[1]].InvokeRequired)
            {
                tictactoe[xy[0], xy[1]].Invoke(new MethodInvoker(delegate
                {
                    tictactoe[xy[0], xy[1]].Text = "O";
                    tictactoe[xy[0], xy[1]].Enabled = false;
                }));
            }

            CheckWin();
            turn++;
        }

        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn00 = new System.Windows.Forms.Button();
            this.btn01 = new System.Windows.Forms.Button();
            this.btn02 = new System.Windows.Forms.Button();
            this.btn10 = new System.Windows.Forms.Button();
            this.btn11 = new System.Windows.Forms.Button();
            this.btn12 = new System.Windows.Forms.Button();
            this.btn20 = new System.Windows.Forms.Button();
            this.btn21 = new System.Windows.Forms.Button();
            this.btn22 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnOpenConnection = new System.Windows.Forms.Button();
            this.txtDisplayBox = new System.Windows.Forms.TextBox();
            this.btnNewGame = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btn00);
            this.flowLayoutPanel1.Controls.Add(this.btn01);
            this.flowLayoutPanel1.Controls.Add(this.btn02);
            this.flowLayoutPanel1.Controls.Add(this.btn10);
            this.flowLayoutPanel1.Controls.Add(this.btn11);
            this.flowLayoutPanel1.Controls.Add(this.btn12);
            this.flowLayoutPanel1.Controls.Add(this.btn20);
            this.flowLayoutPanel1.Controls.Add(this.btn21);
            this.flowLayoutPanel1.Controls.Add(this.btn22);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(44, 175);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(320, 321);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btn00
            // 
            this.btn00.BackColor = System.Drawing.Color.Gainsboro;
            this.btn00.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn00.Location = new System.Drawing.Point(3, 3);
            this.btn00.Name = "btn00";
            this.btn00.Size = new System.Drawing.Size(100, 100);
            this.btn00.TabIndex = 8;
            this.btn00.UseVisualStyleBackColor = false;
            // 
            // btn01
            // 
            this.btn01.BackColor = System.Drawing.Color.Gainsboro;
            this.btn01.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn01.Location = new System.Drawing.Point(109, 3);
            this.btn01.Name = "btn01";
            this.btn01.Size = new System.Drawing.Size(100, 100);
            this.btn01.TabIndex = 9;
            this.btn01.UseVisualStyleBackColor = false;
            // 
            // btn02
            // 
            this.btn02.BackColor = System.Drawing.Color.Gainsboro;
            this.btn02.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn02.Location = new System.Drawing.Point(215, 3);
            this.btn02.Name = "btn02";
            this.btn02.Size = new System.Drawing.Size(100, 100);
            this.btn02.TabIndex = 10;
            this.btn02.UseVisualStyleBackColor = false;
            // 
            // btn10
            // 
            this.btn10.BackColor = System.Drawing.Color.Gainsboro;
            this.btn10.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn10.Location = new System.Drawing.Point(3, 109);
            this.btn10.Name = "btn10";
            this.btn10.Size = new System.Drawing.Size(100, 100);
            this.btn10.TabIndex = 11;
            this.btn10.UseVisualStyleBackColor = false;
            // 
            // btn11
            // 
            this.btn11.BackColor = System.Drawing.Color.Gainsboro;
            this.btn11.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn11.Location = new System.Drawing.Point(109, 109);
            this.btn11.Name = "btn11";
            this.btn11.Size = new System.Drawing.Size(100, 100);
            this.btn11.TabIndex = 12;
            this.btn11.UseVisualStyleBackColor = false;
            // 
            // btn12
            // 
            this.btn12.BackColor = System.Drawing.Color.Gainsboro;
            this.btn12.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn12.Location = new System.Drawing.Point(215, 109);
            this.btn12.Name = "btn12";
            this.btn12.Size = new System.Drawing.Size(100, 100);
            this.btn12.TabIndex = 13;
            this.btn12.UseVisualStyleBackColor = false;
            // 
            // btn20
            // 
            this.btn20.BackColor = System.Drawing.Color.Gainsboro;
            this.btn20.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn20.Location = new System.Drawing.Point(3, 215);
            this.btn20.Name = "btn20";
            this.btn20.Size = new System.Drawing.Size(100, 100);
            this.btn20.TabIndex = 14;
            this.btn20.UseVisualStyleBackColor = false;
            // 
            // btn21
            // 
            this.btn21.BackColor = System.Drawing.Color.Gainsboro;
            this.btn21.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn21.Location = new System.Drawing.Point(109, 215);
            this.btn21.Name = "btn21";
            this.btn21.Size = new System.Drawing.Size(100, 100);
            this.btn21.TabIndex = 15;
            this.btn21.UseVisualStyleBackColor = false;
            // 
            // btn22
            // 
            this.btn22.BackColor = System.Drawing.Color.Gainsboro;
            this.btn22.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn22.Location = new System.Drawing.Point(215, 215);
            this.btn22.Name = "btn22";
            this.btn22.Size = new System.Drawing.Size(100, 100);
            this.btn22.TabIndex = 16;
            this.btn22.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Location = new System.Drawing.Point(149, 185);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(2, 300);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Location = new System.Drawing.Point(255, 185);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(2, 300);
            this.panel2.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Location = new System.Drawing.Point(53, 280);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(300, 2);
            this.panel3.TabIndex = 3;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Location = new System.Drawing.Point(53, 386);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(300, 2);
            this.panel4.TabIndex = 4;
            // 
            // btnOpenConnection
            // 
            this.btnOpenConnection.Location = new System.Drawing.Point(112, 81);
            this.btnOpenConnection.Name = "btnOpenConnection";
            this.btnOpenConnection.Size = new System.Drawing.Size(181, 39);
            this.btnOpenConnection.TabIndex = 5;
            this.btnOpenConnection.Text = "Open Connection";
            this.btnOpenConnection.UseVisualStyleBackColor = true;
            this.btnOpenConnection.Click += new System.EventHandler(this.btnOpenConnection_Click);
            // 
            // txtDisplayBox
            // 
            this.txtDisplayBox.Location = new System.Drawing.Point(86, 25);
            this.txtDisplayBox.Multiline = true;
            this.txtDisplayBox.Name = "txtDisplayBox";
            this.txtDisplayBox.Size = new System.Drawing.Size(237, 50);
            this.txtDisplayBox.TabIndex = 7;
            // 
            // btnNewGame
            // 
            this.btnNewGame.Location = new System.Drawing.Point(149, 126);
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.Size = new System.Drawing.Size(100, 43);
            this.btnNewGame.TabIndex = 8;
            this.btnNewGame.Text = "New Game";
            this.btnNewGame.UseVisualStyleBackColor = true;
            this.btnNewGame.Visible = false;
            this.btnNewGame.Click += new System.EventHandler(this.BtnNewGame_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(150, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Enter Opponent IP:";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(400, 508);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnNewGame);
            this.Controls.Add(this.txtDisplayBox);
            this.Controls.Add(this.btnOpenConnection);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "Form1";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private async void btnOpenConnection_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
            try
            {
                connection = new TcpClient(txtDisplayBox.Text, 5555);
                NewGame();
                txtDisplayBox.Text = "";
                MyTurn();
            }
            catch
            {
                AddToMessageBox("No listener found, opening listener.");
                NewGame();
                TcpListener listener = new TcpListener(Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork), 5555);
                listener.Start();
                connection = await listener.AcceptTcpClientAsync();
                await Task.Factory.StartNew(() => ListenForPacket(connection));
                listener.Stop();
                TheirTurn();
                return;
            }
            AddToMessageBox("Listener found, connection successful.");
            await Task.Factory.StartNew(() => ListenForPacket(connection));
        }

        private void AddToMessageBox(string s)
        {
            //Must invoke as delegate due to cross thread work
            this.Invoke(new MethodInvoker(delegate
            {
                txtDisplayBox.AppendText("\n" + s + "\n");
                txtDisplayBox.ScrollToCaret();
            }));
        }

        private void ListenForPacket(TcpClient singleConnection)
        {
            NetworkStream stream = singleConnection.GetStream();
            while (singleConnection.Connected) //was true
            {
                try
                {
                    byte[] bytesToRead = new byte[singleConnection.ReceiveBufferSize];
                    int bytesRead = stream.Read(bytesToRead, 0, singleConnection.ReceiveBufferSize);
                    string result = Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);
                    if (result != "")
                    {
                        if (result == "NewGame")
                            NewGame();
                        else
                            opponentMove(result);
                        MyTurn();
                    }
                }
                catch (Exception)
                {
                    AddToMessageBox("Connection cut off by opponent...sore loser I guess...");
                    if (btnNewGame.InvokeRequired)
                    {
                        btnNewGame.Invoke(new MethodInvoker(delegate
                        {
                            btnNewGame.Visible = false;
                        }));
                    }
                    else
                        btnNewGame.Visible = false;
                    break;
                }                
            }
            if (btnNewGame.InvokeRequired)
            {
                btnNewGame.Invoke(new MethodInvoker(delegate
                {
                    btnNewGame.Visible = false;
                }));
            }
            else
                btnNewGame.Visible = false;
        }

        private void SendCoordinates(int[] coords)
        {
            string xy = "(" + coords[0].ToString() + "," + coords[1].ToString() + ")";
            SendMessage(connection, xy);
        }

        private void SendMessage(TcpClient singleConnection, string s)
        {
            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(s);
            singleConnection.GetStream().Write(bytesToSend, 0, bytesToSend.Length);
        }

        private void CheckWin()
        {
            List<List<int>> user = new List<List<int>>();
            List<List<int>> opponent = new List<List<int>>();

            Button[,] wins = new Button[,] { { btn00, btn01, btn02 },
                                             { btn10, btn11, btn12 },
                                             { btn20, btn21, btn22 },
                                             { btn00, btn10, btn20 },
                                             { btn01, btn11, btn21 },
                                             { btn02, btn12, btn22 },
                                             { btn00, btn11, btn22 },
                                             { btn02, btn11, btn20 } };

            for (int i = 0; i < 8; i++)
            {
                Button a = wins[i, 0], b = wins[i, 1], c = wins[i, 2];
                if (a.Text == "" || b.Text == "" || c.Text == "")
                    continue;
                if (a.Text == "X" && a.Text == b.Text && a.Text == c.Text)
                {
                    foreach (Button btn in tictactoe)
                    {
                        if (btn.InvokeRequired)
                        {
                            btn.Invoke(new MethodInvoker(delegate
                            {
                                btn.Enabled = false;
                            }));
                        }
                    }
                    a.BackColor = b.BackColor = c.BackColor = Color.Green;
                    AddToMessageBox("You Win!");
                    btnNewGame.Visible = true;
                    break;
                }
                if (a.Text == "O" && a.Text == b.Text && a.Text == c.Text)
                {
                    foreach (Button btn in tictactoe)
                    {
                        if (btn.InvokeRequired)
                        {
                            btn.Invoke(new MethodInvoker(delegate
                            {
                                btn.Enabled = false;
                            }));
                        }
                    }
                    a.BackColor = b.BackColor = c.BackColor = Color.Red;
                    AddToMessageBox("Opponent Wins...");
                    if (btnNewGame.InvokeRequired)
                    {
                        btnNewGame.Invoke(new MethodInvoker(delegate
                        {
                            btnNewGame.Visible = true;
                        }));
                    }
                    break;
                }
            }
        }

        private void BtnNewGame_Click(object sender, EventArgs e)
        {
            SendMessage(connection, "NewGame");
            NewGame();          
        }

        private void NewGame()
        {
            foreach (Button btn in tictactoe)
            {
                if (btn.InvokeRequired)
                {
                    btn.Invoke(new MethodInvoker(delegate
                    {
                        btn.Text = "";
                        btn.Enabled = true;
                        btn.BackColor = Color.Gainsboro;
                        txtDisplayBox.Text = "";
                    }));
                }
                else
                {
                    btn.Text = "";
                    btn.Enabled = true;
                    btn.BackColor = Color.Gainsboro;
                    txtDisplayBox.Text = "";
                }
            }

            if(btnNewGame.InvokeRequired)
            {
                btnNewGame.Invoke(new MethodInvoker(delegate
                {
                    btnNewGame.Visible = false;
                }));
            }
            else
                btnNewGame.Visible = false;
        }

        private void MyTurn()
        {
            foreach (Button btn in btns)
            {
                if (btn.InvokeRequired)
                {
                    btn.Invoke(new MethodInvoker(delegate
                    {
                        if (btn.Text == "")
                            btn.Enabled = true;
                    }));
                }
                if (btn.Text == "")
                    btn.Enabled = true;
            }
        }

        private void TheirTurn()
        {
            foreach (Button btn in btns)
            {
                if (btn.InvokeRequired)
                {
                    btn.Invoke(new MethodInvoker(delegate
                    {
                        if (btn.Text == "")
                            btn.Enabled = false;
                    }));
                }
                if (btn.Text == "")
                    btn.Enabled = false;
            }
        }
    }
}
