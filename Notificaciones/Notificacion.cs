using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotificacionesDll
{
    public class Notificacion
    {
        public static bool con_borde = true;
        public static void mostrar_toast_style1(int duracion_segundos)
        {
            Notification_style1 forma = new Notification_style1();
            forma.intervalo_timer = duracion_segundos;
            forma.Show();

        }
        public static void show_Toast(int duracion_segundos, string mensaje)
        {
            Notification_style2 forma = new Notification_style2();
            Notificacion.con_borde = false;
			forma.BringToFront();
			
            forma.lblMensaje.Text = mensaje;
            forma.intervalo_timer = duracion_segundos;
            forma.Show();
        } 
    }
}
