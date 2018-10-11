/en Pagina2.aspx estála opción de dividir, y está el diseño como tal
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pagina2 : System.Web.UI.Page {
    protected OdbcConnection conectarBD () {
        String stringConexion = "Driver={SQL Server Native Client 11.0}; Server=CC101-06;Uid=sa;Pwd=sqladmin;Database=GameSpot";
        try {
            OdbcConnection conexion = new OdbcConnection (stringConexion);
            conexion.Open ();
            Label1.Text = "conexion exitosa";
            return conexion;
        }
        catch (Exception ex) {
            Label1.Text = ex.StackTrace.ToString ();
            return null;
        }
    }
    protected void Page_Load ( object sender, EventArgs e ) {// cuando se pase a la siguiente se pongan los datos
        OdbcConnection miConexion = conectarBD ();
        if(miConexion != null) {
            String query = "select nombre, edad, sexo from usuario where claveU="+Session["cu"].ToString();
            OdbcCommand cmd = new OdbcCommand (query, miConexion);
            OdbcDataReader rd = cmd.ExecuteReader ();
            if (rd.HasRows) {//HAY COLUMNAS? SI SÍ LEEMOS EL PRIMER DATO therefore existen usuarios
                rd.Read ();
                txNombre.Text = rd.GetString (0);
                txEdad.Text = rd.GetString (1);
                txSexo.Text = rd.GetString (2);


             }
            rd.Close ();
            miConexion.Close ();
            }

        
}

    protected void Button1_Click ( object sender, EventArgs e ) {
        Response.Redirect ("Pagina3.aspx");
    }
}
