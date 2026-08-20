using System;
using System.Windows.Forms;

namespace BusControl.Presentation
{
    public static class InputHelper
    {
        public static string PedirTexto(string mensaje, string titulo = "Entrada de datos")
        {
            Form prompt = new Form()
            {
                Width = 400,
                Height = 150,
                Text = titulo,
                StartPosition = FormStartPosition.CenterScreen,
                BackColor = System.Drawing.Color.LightBlue // 🎨 estilo bonito
            };

            Label textLabel = new Label()
            {
                Left = 20,
                Top = 20,
                Text = mensaje,
                Width = 340,
                Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold)
            };

            TextBox inputBox = new TextBox()
            {
                Left = 20,
                Top = 50,
                Width = 340
            };

            Button confirmation = new Button()
            {
                Text = "OK",
                Left = 280,
                Width = 80,
                Top = 80,
                BackColor = System.Drawing.Color.LightGreen // 🎨 estilo bonito
            };
            confirmation.DialogResult = DialogResult.OK;

            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(inputBox);
            prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? inputBox.Text : "";
        }
    }
}
