using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using System.IO;

namespace Mail_Sender_Csharp_Winform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            textBox1.Text = "mail.sender23@mail.ru";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Contains('@') && textBox2.Text.Contains('@'))
            {
                SmtpClient smtpClient = new SmtpClient("smtp.mail.ru", 587);

                smtpClient.Credentials = new NetworkCredential(textBox1.Text, "1C8XH4nyMqfqyL0kGMVK");

                smtpClient.EnableSsl = true;

                MailMessage mailMessage = new MailMessage();

                mailMessage.IsBodyHtml = checkBox1.Checked;

                mailMessage.From = new MailAddress(textBox1.Text, "Mail Sender");

                foreach (MailAddress item in GetMails(textBox2.Text))
                {
                    mailMessage.To.Add(item);
                }
                foreach (MailAddress item in GetMails(textBox3.Text))
                {
                    mailMessage.CC.Add(item);
                }

                mailMessage.Subject = textBox4.Text;

                mailMessage.Body = textBox5.Text;

                foreach (string item in listBox1.Items)
                {
                    mailMessage.Attachments.Add(new Attachment(item));
                }

                smtpClient.Send(mailMessage);

                MessageBox.Show("Mail");
            }
        }

        private IEnumerable<MailAddress> GetMails(string text)
        {
            string[] result = text.Split(' ', ',', ';');

            foreach (string item in result)
            {
                if (item.Contains('@'))
                {
                    yield return new MailAddress(item);
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "All files(*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (new FileInfo(openFileDialog.FileName).Length <= 30 * 1024 * 1024)
                {
                    listBox1.Items.Add(openFileDialog.FileName);
                }
            }

        }
    }
}
