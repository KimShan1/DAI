using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pagina3 : System.Web.UI.Page {
    protected OdbcConnection conectarBD () {
        String stringConexion = "Driver={SQL Server Native Client 11.0}; Server=CC101-06;Uid=sa;Pwd=sqladmin;Database=GameSpot";
        try {
            OdbcConnection conexion = new OdbcConnection (stringConexion);
            conexion.Open ();
            lbError.Text = "conexion exitosa";
            return conexion;
        }
        catch (Exception ex) {
            lbError.Text = ex.StackTrace.ToString ();
            return null;
        }
    }
    protected void Page_Load ( object sender, EventArgs e ) {
        if (!IsPostBack) { 
        OdbcConnection miConexion = conectarBD ();
            if (miConexion != null) {
                OdbcCommand cmd = new OdbcCommand ("select juegos.nombre from juegos,escriben where escriben.claveU=" + Session["cu"].ToString () + "and escriben.claveJ=juegos.claveJ", miConexion);
                //ahora lo leo
                OdbcDataReader rd = cmd.ExecuteReader ();
                //vamos a limpiarlo
                ddJuegos.Items.Clear ();
                while (rd.Read ()) {
                    //comando del combobos dd juegos
                    ddJuegos.Items.Add (rd.GetString (0));
                }

                rd.Close ();
                miConexion.Close ();
            }

        }

    }

    protected void ddJuegos_SelectedIndexChanged ( object sender, EventArgs e ) {
        //Una conexion 
        OdbcConnection miConexion = conectarBD ();
        if (miConexion != null) {
            String var1 = ddJuegos.SelectedValue;
            String query = "select claveJ from juegos where nombre='"+var1+"'";
            //ahora la clave J
            OdbcCommand cmd = new OdbcCommand (query, miConexion);
            OdbcDataReader rd = cmd.ExecuteReader ();
            rd.Read ();
            //lo que se trajo el reader lo voy a guardar en:
            int clavej = rd.GetInt16(0);
            //OTRO STRING QUERY
            String query2 = "select critica.contentido from critica where critica.claveC=(select claveC from escriben where escriben.claveU="+Session["cu"].ToString()+"and escriben.claveJ="+clavej+")";
            OdbcCommand cmd2 = new OdbcCommand (query2, miConexion);
            OdbcDataReader rd2 = cmd2.ExecuteReader();
            rd2.Read ();
            lbCritica.Text = rd2.GetString (0);
            rd.Close ();
            rd2.Close ();
        }
    }

    protected void Button1_Click ( object sender, EventArgs e ) {
        Response.Redirect ("Pagina4.aspx");
    }
}
