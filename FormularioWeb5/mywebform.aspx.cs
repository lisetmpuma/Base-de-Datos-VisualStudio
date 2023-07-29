using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Text;
using System.Reflection.Emit;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using FormularioWeb5.ServiceReference1;
using FormularioWeb5.ServiceReference2; //agregar

namespace FormularioWeb5
{
    public partial class mywebform : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCiudades(); 
            }  
        }
            
        private void CargarCiudades()
        {
            Service1Client client = new Service1Client();
            string[] ciudades = client.getCiudades();
            client.Close();

            Array.Sort(ciudades);
            ciudad.Items.Clear();
            ciudad.Items.Add("SELECCIONA UNA OPCION");

            foreach (string s in ciudades)
            {
                ciudad.Items.Add(s);
            }
        }


        

        private bool ValidarCampos()
        {
            if (string.IsNullOrEmpty(nombre.Text))
            { return false; }

            if (string.IsNullOrEmpty(apellidos.Text))
            { return false; }

            if (!(masculino.Checked || femenino.Checked))
            { return false; }

            string correo = this.correo.Text.Trim();
            string dominio = correo.Substring(correo.LastIndexOf('@') + 1);

            if (dominio != "unsa.edu.pe")
            { return false; }

            if (ciudad.SelectedIndex == 0)
            { return false; }

         return true;
        }
        protected void EnviarClick(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {               
                string nombre = this.nombre.Text;                                           // Obtener los datos ingresados en el formulario
                string apellidos = this.apellidos.Text;
                string sexo = this.masculino.Checked ? "Masculino" : "Femenino";
                string email = this.correo.Text;
                string direccion = this.direccion.Text;
                string ciudad = this.ciudad.SelectedValue;
                string requerimiento = this.requerimiento.Text;
               
                IService2 servicio2 = new ServiceReference2.Service2Client();                // Crear una instancia del servicio2 (suponiendo que la interfaz se llama "IService2")               
                servicio2.GuardarInformacion(nombre, apellidos, sexo, email,
                                      direccion, ciudad, requerimiento);                                              // Llamar al método GuardarInformacion() para guardar los datos en la base de datos              
            }
            else
            { cuadroResumen.Visible = false; }
        }

        protected void Eliminar_Click(object sender, EventArgs e)
        {
            EliminarInformacionDelFormulario();
        }

        private void EliminarInformacionDelFormulario()
        {
            nombre.Text = string.Empty;
            apellidos.Text = string.Empty;
            masculino.Checked = false;
            femenino.Checked = false;
            correo.Text = string.Empty;
            direccion.Text = string.Empty;
            ciudad.ClearSelection();
            requerimiento.Text = string.Empty;

            cuadroResumen.Visible = false;
        }


    }
}



