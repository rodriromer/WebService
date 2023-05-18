using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Services;
using System.Xml;
using System.Xml.Linq;

namespace WServices
{
    /// <summary>
    /// Descripción breve de ServicioEstudiantil
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class ServicioEstudiantil : System.Web.Services.WebService
    {
        private string conecctionString = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;


        [WebMethod]
        public XmlDocument CreateUsuario(string nombre, string apellido, string telefono)
        {
            using (SqlConnection connection = new SqlConnection(conecctionString))
            {
                string query = "INSERT INTO estudiante (nombre,apellido,telefono) VALUES (@nombre,@apellido,@telefono)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@nombre", nombre);
                command.Parameters.AddWithValue("@apellido", apellido);
                command.Parameters.AddWithValue("@telefono", telefono);

                connection.Open();
                command.ExecuteNonQuery();
            }

            XmlDocument xmlResponse = new XmlDocument();

            XmlElement rootElement = xmlResponse.CreateElement("Response");
            xmlResponse.AppendChild(rootElement);

            XmlElement responseElement = xmlResponse.CreateElement("Message");
            responseElement.InnerText = "Datos registrados correctamente";
            rootElement.AppendChild(responseElement);

            return xmlResponse;
        }

        [WebMethod]
        public DataSet GetUsuarios()
        {
            using (SqlConnection connection = new SqlConnection(conecctionString))
            {
                string query = "SELECT * FROM estudiante";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet dataSet = new DataSet();

                connection.Open();
                adapter.Fill(dataSet, "estudiante");

                return dataSet;
            }
        }


        [WebMethod]
        public XmlDocument UpdateUsuario(int id, string nuevoNombre, string nuevoApellido, string nuevoelefono)
        {
            using (SqlConnection connection = new SqlConnection(conecctionString))
            {
                string query = "UPDATE estudiante SET nombre = @nuevoNombre, apellido = @nuevoApellido, telefono = @nuevoTelefono WHERE id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@nuevoNombre", nuevoNombre);
                command.Parameters.AddWithValue("@nuevoApellido", nuevoApellido);
                command.Parameters.AddWithValue("@nuevoTelefono", nuevoelefono);
                command.Parameters.AddWithValue("@id", id);


                connection.Open();
                command.ExecuteNonQuery();
            }

            XmlDocument xmlResponse = new XmlDocument();

            XmlElement rootElement = xmlResponse.CreateElement("Response");
            xmlResponse.AppendChild(rootElement);

            XmlElement responseElement = xmlResponse.CreateElement("Message");
            responseElement.InnerText = "Se ha realizado una modificacion exitosa";
            rootElement.AppendChild(responseElement);

            return xmlResponse;
        }

        [WebMethod]
        public XmlDocument DeleteUsuario(int id)
        {
            using (SqlConnection connection = new SqlConnection(conecctionString))
            {
                string query = "DELETE FROM estudiante WHERE id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                command.ExecuteNonQuery();
            }

            XmlDocument xmlResponse = new XmlDocument();

            XmlElement rootElement = xmlResponse.CreateElement("Response");
            xmlResponse.AppendChild(rootElement);

            XmlElement responseElement = xmlResponse.CreateElement("Message");
            responseElement.InnerText = "Registro eliminado correctamente";
            rootElement.AppendChild(responseElement);

            return xmlResponse;
        }
    }
}
