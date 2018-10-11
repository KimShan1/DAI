using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page {
    protected OdbcConnection conectarBD () {
        String stringConexion = "Driver={SQL Server Native Client 11.0}; Server=CC101-06;Uid=sa;Pwd=sqladmin;Database=GameSpot";
 try
 {
 OdbcConnection conexion = new OdbcConnection (stringConexion);
    conexion.Open();
 lbContador.Text = "conexion exitosa";
 return conexion;
 }
 catch (Exception ex)
 {
 lbContador.Text = ex.StackTrace.ToString();
 return null;
 }
 }

    protected void Page_Load ( object sender, EventArgs e ) {
        //Para probar conexión
        // OdbcConnection miConexion = conectarBD ();
        txContraseña.Text = "juanito";
        txUsuario.Text = "juan@gmail.com"; // esto es para predeterminar y ya no tener que estar escribiendo. 

    }

    protected void btPagina2_Click ( object sender, EventArgs e ) {
        //Me voy a diseño  y botonn 2
        OdbcConnection miConexion = conectarBD ();
        if(miConexion != null) {
            String query = "select claveU from usuario where email='"+txUsuario.Text+"' and password='"+txContraseña.Text +"'";
            //ahora hacemos el command
            OdbcCommand cmd = new OdbcCommand (query, miConexion);
            OdbcDataReader rd = cmd.ExecuteReader ();
            if (rd.HasRows) {//HAY COLUMNAS? SI SÍ LEEMOS EL PRIMER DATO therefore existen usuarios
                rd.Read ();
                //hay que crear una sesion que guarde info que me sirva para la siguiebnte, esta sesionla recuperamos con su nombre 
                Session["cu"] = rd.GetInt32 (0).ToString ();
                Response.Redirect ("Pagina2.aspx"); //comando para llamar a la siguiente pagina

            }
            else
                lbContador.Text = "el usuario o password son incorrectos";
        }
    }
}
