using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Windows.Forms.VisualStyles;
using System.Diagnostics.Eventing.Reader;
using System.Diagnostics;
using System.Xml.Schema;
using System.Data.SqlTypes;


namespace ClienteWindows
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public class Usuario
        {
            public string usr { get; set; } 

            public string clave { get; set; }
        }

        public class Respuesta
        {
            public string token { get; set; }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var cliente1 = new HttpClient();

            Usuario ob_usuario = new Usuario() { usr = "edrod", clave = "pwd123" };

            var content = new StringContent(JsonConvert.SerializeObject(ob_usuario), Encoding.UTF8, "application/json");

            var response1 = await cliente1.PostAsync("https://localhost:44307/api/Autenticacion/Validar", content);

            var json_respuesta1 = await response1.Content.ReadAsStringAsync();

            var ob_respuesta = JsonConvert.DeserializeObject<Respuesta>(json_respuesta1);

           

            var cliente2 = new HttpClient();

            cliente2.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ob_respuesta.token);

            var response = await cliente2.GetAsync("https://localhost:44307/api/Ciudad/listar ciudades");

            var test = await response.Content.ReadAsStringAsync();

            txtResp.Text = test;
           
            if (test != null)
            {
                txtMensaje.Text = "OK";
            }
            else {
                txtMensaje.Text = "SIN RESPUESTA";
            }
            
        }

       
    }
}
