using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace 똑딱test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbPortName.DataSource = SerialPort.GetPortNames();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = cmbPortName.Text;                
                serialPort1.StopBits = StopBits.One;                
                serialPort1.RtsEnable = true;                
                serialPort1.Open();
                if (serialPort1.IsOpen)
                    MessageBox.Show("포트가 연결 됬습니다.");
                cmbPortName.Enabled = false;
            }
            catch
            {
                if (serialPort1.IsOpen)
                {
                    var message = "포트가 이미 열려 있습니다. 연결을 취소 하시겠습니까? ";
                    DisConnect(message);
                }
                else
                {
                    return;
                }

            }

            finally
            {

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (PortIsOpen() == false)                
                return;
            var message = "연결을 취소 하시겠습니까?";
            DisConnect(message);
        }

        private void DisConnect(string message)
        {
            if (PortIsOpen() == false)
                return;

            var result = MessageBox.Show(message, "", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                serialPort1.Close();
                cmbPortName.Enabled = true;
            }
        }

        private void btnOn_Click(object sender, EventArgs e)
        {
            if (PortIsOpen() == false)
                return;
            byte[] hex = { 0xA0, 0x01, 0x01, 0xA2 };
            serialPort1.Write(hex, 0, 4);
        }

        private void btnOff_Click(object sender, EventArgs e)
        {
            if (PortIsOpen() == false)
                return;
            byte[] hex = { 0xA0, 0x01, 0x00, 0xA1 };
            serialPort1.Write(hex, 0, 4);
        }

        private bool PortIsOpen() 
        {
            if (serialPort1.IsOpen == false)
                MessageBox.Show("연결된 포트가 없습니다.");
            return serialPort1.IsOpen;
        }

        private void cmbPortName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

    }
}
