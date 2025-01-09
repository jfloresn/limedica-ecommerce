namespace Web.Xmarket.Utilitario
{
    using CommandContracts.Xmarket.Contactenos;
    using log4net;
    using QueryContracts.Xmarket.Carrito.Result;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Reflection;
    using System.Text;
    using WebGrease.Css.Extensions;

    public class Mailer
    {

        private ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public Mailer(Diccionario diccionario)
        {
            this.Notificacion = diccionario;
        }

        private Diccionario Notificacion { get; set; }

        void EnviarCorreo(List<string> correosPara, string correoDe, List<string> correosCopia,
                                   string asunto, string cuerpo, List<Attachment> adjuntoList)
        {
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(correoDe);
            mail.Body = cuerpo;
            mail.Subject = asunto;
            mail.IsBodyHtml = true;
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            adjuntoList.ForEach(x => mail.Attachments.Add(x));


            mail.To.Add(new MailAddress(correosPara.ElementAt(0)));

            log.Info("CorreosCopia cantidad de items: " + correosCopia.ElementAt(0));
            log.Info("Mail suject: " + mail.Subject);

     

            WebConfigReader.Mailer.CopiaOculta.Split(';').ToList().ForEach(x =>
                {
                    if (!string.IsNullOrEmpty(x))
                        mail.CC.Add(new MailAddress(x));
                });


            SmtpClient Cliente = new SmtpClient();

            Cliente.Host = WebConfigReader.Mailer.Host;
            if (WebConfigReader.Mailer.Port > 0)
                Cliente.Port = Convert.ToInt32(WebConfigReader.Mailer.Port);

            Cliente.EnableSsl = WebConfigReader.Mailer.EnabledSSL;
            Cliente.UseDefaultCredentials = WebConfigReader.Mailer.UseDefaultCredentials;
            if (WebConfigReader.Mailer.UseDefaultCredentials == false)
                Cliente.Credentials = new NetworkCredential(WebConfigReader.Mailer.CredentialsUser, WebConfigReader.Mailer.CredentialsClave);

            Cliente.DeliveryMethod = SmtpDeliveryMethod.Network;
            Cliente.Send(mail);

         
        }

        public void Enviar()
        {
            string asunto = "";
            string cuerpo = "";
            try
            {

            

                asunto = Notificacion.Asunto;
                cuerpo = Notificacion.Cuerpo;

                foreach (var item in Notificacion.AsuntoValores)
                    asunto = asunto.Replace(item.Key, item.Value);

                foreach (var item in Notificacion.CuerpoValores)
                    cuerpo = cuerpo.Replace(item.Key, item.Value);

                this.Notificacion.CorreoDe = WebConfigReader.Mailer.From;

               if(Notificacion.ConCopia?.Length>0)
                    this.Notificacion.CorreosPara.AddRange(Notificacion.ConCopia.Split(';').ToList());

                
                log.Info("Notification: " + this.Notificacion.ToString());

                this.Notificacion.CorreosPara.ForEach(x =>
                {

                    if (x != "")
                    {
                        EnviarCorreo(new List<string> { x }, 
                            Notificacion.CorreoDe, 
                            Notificacion.CorreosCopia, 
                            asunto, 
                            cuerpo,
                            Notificacion.Adjuntos);
                    }
                });
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
    }

    public class Diccionario
    {
        public Diccionario()
        {
            this.AsuntoValores = new Dictionary<string, string>();
            this.CuerpoValores = new Dictionary<string, string>();
            this.CorreosPara = new List<string>();
            this.CorreosCopia = new List<string>();
            this.Adjuntos = new List<Attachment>();
        }
        public string Asunto { get; set; }
        public string Cuerpo { get; set; }
        public string CorreoDe { get; set; }
        public string ConCopia { get; set; }
        public Dictionary<string, string> AsuntoValores { get; set; }
        public Dictionary<string, string> CuerpoValores { get; set; }
        public List<string> CorreosPara { get; set; }
        public List<string> CorreosCopia { get; set; }
        public List<Attachment> Adjuntos { get; set; }
        public string ResolveBody()
        {
            var cuerpo = this.Cuerpo;
            foreach (var item in CuerpoValores)
                cuerpo = cuerpo.Replace(item.Key, item.Value);
            return cuerpo;
        }


        public override String ToString()
        {

            StringBuilder datos = new StringBuilder();


            datos.Append("Inicio Informacion de Clase Diccionario");

            datos.Append("Atributo:  Informacion de Clase Diccionario");


            datos.AppendLine();

            if (this.CorreosCopia != null)
            {
                datos.Append("Atributo: Ini CorreosCopia list");
                datos.AppendLine();
                this.CorreosCopia.ForEach(x =>
                {
                    datos.Append("Correo " + x);
                    datos.AppendLine();
                });

                datos.Append("Atributo: Fin CorreosCopia list");
                datos.AppendLine();
            }


            datos.AppendLine();

            if (this.CorreosPara != null)
            {
                datos.Append("Atributo: Ini CorreosPara list");
                datos.AppendLine();
                this.CorreosCopia.ForEach(x =>
                {
                    datos.Append("Correo " + x);
                    datos.AppendLine();
                });

                datos.Append("Atributo: Fin CorreosPara list");
                datos.AppendLine();
            }



            datos.Append("Fin Informacion de Clase Diccionario");
            datos.AppendLine();
            return datos.ToString();

        }

    }

    public class FormatoCorreo
    {
        public string body_message_recuperar_clave(string nombre, string usuario_red, string password)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table width='100%' cellpadding='0' cellspacing='0' border='0' dir='ltr' style='font-size:16px;background-color:rgb(227,225,225)'>");
            sb.Append("<tbody>");
            sb.Append("<tr>");
            sb.Append("        <td align='center' valign='top' style='margin:0;padding:40'>");
            sb.Append("            <table align='center' border='0' cellspacing='0' cellpadding='0' width='700' bgcolor='#1ab394' style='width:700px;border:1px solid ");
            sb.Append("         transparent; ");
            sb.Append("order-radius:13px;margin:auto;background-color:#1872a6'>");
            sb.Append("                <tbody>");
            sb.Append("					<tr>");
            sb.Append("					<td>");
            sb.Append("						<table cellpadding='0' cellspacing='0' border='0' width='100%'>");
            sb.Append("						<tbody>");
            sb.Append("							<tr>");
            sb.Append("							<td valign='top' align='left' style='padding:0px;margin:0px'>");
            sb.Append("								<table cellpadding='0' cellspacing='0' border='0' width='100%'>");
            sb.Append("								<tbody>");
            sb.Append("									<tr>");
            sb.Append("									<td align='left' valign='top'>");
            sb.Append("									<table width='100%' border='0' cellpadding='0' cellspacing='0' align='center'>");
            sb.Append("										<tbody>");
            sb.Append("											<tr>");
            sb.Append("											<td align='left' valign='top' style='font-family:Arial,Helvetica,sans-serif;font-size:20px;border-radius:6px");
            sb.Append("	                                        color:rgb(' sb.Append('55,255,255)'>");
            sb.Append("												<div style='text-align:center'><span style='color:rgb(255,255,255);font-weight:bold'><br></span></div>");
            sb.Append("												<div style='text-align:center'><span style='color:rgb(255,255,255);font-weight:bold'> LIMEDICA");
            sb.Append("                                             </span></div>");
            sb.Append("												<div style='text-align:center'><span style='color:rgb(255,255,255);font-weight:bold'><br></span></div>");
            sb.Append("												<span style='color:rgb(38,38,38)'></span>");
            sb.Append("											</td>");
            sb.Append("											</tr>");
            sb.Append("										</tbody>");
            sb.Append("									</table>");
            sb.Append("									</td>");
            sb.Append("									</tr>");
            sb.Append("									<tr>");
            sb.Append("									<td>");
            sb.Append("										<table width='100%' border='0' cellpadding='10' cellspacing='10' align='center'  bgcolor='white'>");
            sb.Append("										<tbody>");
            sb.Append("										       <tr>");
            sb.Append("												<td align='left' valign='top' style='padding:10px;font-family:Arial,Helvetica,sans-serif;color:rgb(38,38,38)");
            sb.Append("                                             font-size:12px;background-color:rgb(255,255,255);width:190px '  colspan='4'> ");
            sb.Append("													Sr(a). " + nombre + "");
            sb.Append("												</td>");
            sb.Append("											</tr>");
            sb.Append("											<tr>");
            sb.Append("												<td colspan='2' align='left' valign='top' style='padding:10px;font-family:Arial,Helvetica,sans-serif;color:rgb(38,38,38);");
            sb.Append("                                         font-size:12px;background-color:rgb(255,255,255);width:190px '  colspan='4'>");
            sb.Append("													Bienvenido a la web Transmares, para poder ingresar a la web, digite su usuario y contrasena proporcionado ");
            sb.Append("                                         por Transmares.");
            sb.Append("												</td>");
            sb.Append("											</tr>");
            sb.Append("												<tr>");
            sb.Append("												<td colspan='2' align='left' valign='top' style='padding:10px;font-family:Arial,Helvetica,sans-serif;color:rgb(38,38,38);");
            sb.Append("                                             font-size:14px;font-weight:bold;background-color:rgb(255,255,255)'  colspan='4'>");
            sb.Append("													<span style='font-family:Arial,Helvetica,sans-serif;color:rgb(38,38,38)'>Recordatorio de Password  ");
            sb.Append(".</span>");
            sb.Append("												</td>");
            sb.Append("											</tr>");
            sb.Append("											<tr>");
            sb.Append("												<td align='left' valign='top' style='padding:10px;font-family:Arial,Helvetica,sans-serif;color:rgb(38,38,38);");
            sb.Append("ont-size:12px;background-color:rgb(255,255,255);width:190px'>");
            sb.Append("													Usuario : " + usuario_red + "");
            sb.Append("												</td>");
            sb.Append("												<td align='left' valign='top' style='padding:10px;font-family:Arial,Helvetica,sans-serif;color:rgb(38,38,38);");
            sb.Append("ont-size:12px;background-color:rgb(255,255,255);width:190px'>");
            sb.Append("												</td>");
            sb.Append("												<td align='left' valign='top' style='padding:10px;font-family:Arial,Helvetica,sans-serif;color:rgb(38,38,38);");
            sb.Append("ont-size:12px;background-color:rgb(255,255,255);width:190px'>");
            sb.Append("													Password : " + password + " ");
            sb.Append("												</td>");
            sb.Append("												<td align='left' valign='top' style='padding:10px;font-family:Arial,Helvetica,sans-serif;color:rgb(38,38,38);");
            sb.Append("ont-size:12px;background-color:rgb(255,255,255);width:190px'>	");
            sb.Append("												</td>");
            sb.Append("											</tr>");
            sb.Append("									");
            sb.Append("										</tbody>");
            sb.Append("										");
            sb.Append("										</table>");
            sb.Append("									</td>");
            sb.Append("									</tr>");
            sb.Append("									<tr>");
            sb.Append("									<td>");
            sb.Append("										");
            sb.Append("						</tbody>");
            sb.Append("						</table>");
            sb.Append("					</td>");
            sb.Append("					</tr>");
            sb.Append("				</tbody>");
            sb.Append("			</table>");
            sb.Append("        </td>");
            sb.Append("    </tr>");
            sb.Append("</tbody>");
            sb.Append("</table>");


            return sb.ToString();
        }

        public string BodyMensajeSolicitudIncidencia(string pNombreUsuario, string pMensaje,string pNro)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table width='100%' cellpadding='0' cellspacing='0' border='0' dir='ltr' style='font-size:16px;background-color:rgb(255,255,255)'>");
            sb.Append("<tbody>");
            sb.Append("<tr>");
            sb.Append("        <td align='center' valign='top' style='margin:0;padding:40'>");
            sb.Append("            <table align='center' border='0' cellspacing='0' cellpadding='0' width='700' bgcolor='#1ab394' style='width:700px;border:1px solid ");
            sb.Append("         transparent; ");
            sb.Append("order-radius:13px;margin:auto;background-color:#1872a6'>");
            sb.Append("                <tbody>");
            sb.Append("					<tr>");
            sb.Append("					<td>");
            sb.Append("						<table cellpadding='0' cellspacing='0' border='0' width='100%'>");
            sb.Append("						<tbody>");
            sb.Append("							<tr>");
            sb.Append("							<td valign='top' align='left' style='padding:0px;margin:0px'>");
            sb.Append("								<table cellpadding='0' cellspacing='0' border='0' width='100%'>");
            sb.Append("								<tbody>");
            sb.Append("									<tr>");
            sb.Append("									<td align='left' valign='top'>");
            sb.Append("									<table width='100%' border='0' cellpadding='0' cellspacing='0' align='center'>");
            sb.Append("										<tbody>");
            sb.Append("											<tr>");
            sb.Append("											<td align='left' valign='top' style='font-family:Arial,Helvetica,sans-serif;font-size:20px;border-radius:6px");
            sb.Append("	                                        color:rgb(' sb.Append('55,255,255)'>");
            sb.Append("												<div style='text-align:center'><span style='color:rgb(255,255,255);font-weight:bold'><br></span></div>");
            sb.Append("												<div style='text-align:center'><span style='color:rgb(255,255,255);font-weight:bold'> LIMEDICA PEDIDO ");
            sb.Append("                                             </span></div>");
            sb.Append("												<div style='text-align:center'><span style='color:rgb(255,255,255);font-weight:bold'><br></span></div>");
            sb.Append("												<span style='color:rgb(38,38,38)'></span>");
            sb.Append("											</td>");
            sb.Append("											</tr>");
            sb.Append("										</tbody>");
            sb.Append("									</table>");
            sb.Append("									</td>");
            sb.Append("									</tr>");
            sb.Append("									<tr>");
            sb.Append("									<td>");
            sb.Append("										<table width='100%' border='0' cellpadding='10' cellspacing='10' align='center'  bgcolor='white'>");
            sb.Append("										<tbody>");
            sb.Append("										       <tr>");
            sb.Append("												<td align='left' valign='top' style='padding:10px;font-family:Arial,Helvetica,sans-serif;color:rgb(38,38,38)");
            sb.Append("                                             font-size:12px;background-color:rgb(255,255,255);width:190px '  colspan='4'> ");
            sb.Append("													Sr(a). " + pNombreUsuario + "");
            sb.Append("												</td>");
            sb.Append("											</tr>");
            sb.Append("											<tr>");
            sb.Append("												<td colspan='2' align='left' valign='top' style='padding:10px;font-family:Arial,Helvetica,sans-serif;color:rgb(38,38,38);");
            sb.Append("                                         font-size:12px;background-color:rgb(255,255,255);width:190px '  colspan='4'>");
            sb.Append("													Bienvenido a la web Transmares,"+ pMensaje + "");
            sb.Append("												</td>");
            sb.Append("											</tr>");
            sb.Append("												<tr>");
            sb.Append("												<td colspan='2' align='left' valign='top' style='padding:10px;font-family:Arial,Helvetica,sans-serif;color:rgb(38,38,38);");
            sb.Append("                                             font-size:14px;font-weight:bold;background-color:rgb(255,255,255)'  colspan='4'>");
            sb.Append("													<span style='font-family:Arial,Helvetica,sans-serif;color:rgb(38,38,38)'> Nro:  " + pNro + "");
            sb.Append("</span>");
            sb.Append("												</td>");
            sb.Append("											</tr>");
            sb.Append("									");
            sb.Append("										</tbody>");
            sb.Append("										");
            sb.Append("										</table>");
            sb.Append("									</td>");
            sb.Append("									</tr>");
            sb.Append("									<tr>");
            sb.Append("									<td>");
            sb.Append("										");
            sb.Append("						</tbody>");
            sb.Append("						</table>");
            sb.Append("					</td>");
            sb.Append("					</tr>");
            sb.Append("				</tbody>");
            sb.Append("			</table>");
            sb.Append("        </td>");
            sb.Append("    </tr>");
            sb.Append("</tbody>");
            sb.Append("</table>");


            return sb.ToString();
        }


        public string plantillaContacto(ContactenosRegistrarCommand comman)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table width='100%' cellpadding='0' cellspacing='0' border='0' dir='ltr' style='font-size:16px;background-color:rgb(255,255,255)'>");
            sb.Append("<tbody>");
            sb.Append("<tr>");
            sb.Append("        <td align='center' valign='top' style='margin:0;padding:40'>");
            sb.Append("            <table align='center' border='0' cellspacing='0' cellpadding='0' width='700' bgcolor='#1ab394' style='width:700px;border:1px solid ");
            sb.Append("         transparent; ");
            sb.Append("order-radius:13px;margin:auto;background-color:#1872a6'>");
            sb.Append("                <tbody>");
            sb.Append("					<tr>");
            sb.Append("					<td>");
            sb.Append("						<table cellpadding='0' cellspacing='0' border='0' width='100%'>");
            sb.Append("						<tbody>");
            sb.Append("							<tr>");
            sb.Append("							<td valign='top' align='left' style='padding:0px;margin:0px'>");
            sb.Append("								<table cellpadding='0' cellspacing='0' border='0' width='100%'>");
            sb.Append("								<tbody>");
            sb.Append("									<tr>");
            sb.Append("									<td align='left' valign='top'>");
            sb.Append("									<table width='100%' border='0' cellpadding='0' cellspacing='0' align='center'>");
            sb.Append("										<tbody>");
            sb.Append("											<tr>");
            sb.Append("											<td align='left' valign='top' style='font-family:Arial,Helvetica,sans-serif;font-size:20px;border-radius:6px");
            sb.Append("	                                        color:rgb(' sb.Append('55,255,255)'>");
            sb.Append("												<div style='text-align:center'><span style='color:rgb(255,255,255);font-weight:bold'><br></span></div>");
            sb.Append("												<div style='text-align:center'><span style='color:rgb(255,255,255);font-weight:bold'> NUEVO CONTACTO REGISTRADO DESDE LA WEB DE LIMEDICA  ");
            sb.Append("                                             </span></div>");
            sb.Append("												<div style='text-align:center'><span style='color:rgb(255,255,255);font-weight:bold'><br></span></div>");
            sb.Append("												<span style='color:rgb(38,38,38)'></span>");
            sb.Append("											</td>");
            sb.Append("											</tr>");
            sb.Append("										</tbody>");
            sb.Append("									</table>");
            sb.Append("									</td>");
            sb.Append("									</tr>");
            sb.Append("									<tr>");
            sb.Append("									<td>");
            sb.Append("										<table width='100%' border='0' cellpadding='10' cellspacing='10' align='center'  bgcolor='white'>");
            sb.Append("										<tbody>");
            sb.Append("										       <tr>");
            sb.Append("												<td align='left' valign='top' style='padding:10px;font-family:Arial,Helvetica,sans-serif;color:rgb(38,38,38)");
            sb.Append("                                             font-size:12px;background-color:rgb(255,255,255);width:190px '  colspan='4'> ");
            sb.Append("													Nombre : " + comman.nombre + "");
            sb.Append("												</td>");
            sb.Append("											</tr>");

            sb.Append("											<tr>");
            sb.Append("												<td align='left' valign='top' style='padding:10px;font-family:Arial,Helvetica,sans-serif;color:rgb(38,38,38);");
            sb.Append("ont-size:12px;background-color:rgb(255,255,255);width:190px'>");
            sb.Append("													Apellido : " + comman.apellido + "");
            sb.Append("												</td>");
            sb.Append("											</tr>");

            sb.Append("											<tr>");
            sb.Append("												<td align='left' valign='top' style='padding:10px;font-family:Arial,Helvetica,sans-serif;color:rgb(38,38,38);");
            sb.Append("ont-size:12px;background-color:rgb(255,255,255);width:190px'>");
            sb.Append("													Correo : " + comman.correo + "");
            sb.Append("												</td>");
            sb.Append("											</tr>");

            sb.Append("											<tr>");
            sb.Append("												<td align='left' valign='top' style='padding:10px;font-family:Arial,Helvetica,sans-serif;color:rgb(38,38,38);");
            sb.Append("ont-size:12px;background-color:rgb(255,255,255);width:190px'>");
            sb.Append("													Ciudad : " + comman.idCiudad + "");
            sb.Append("												</td>");
            sb.Append("											</tr>");


            sb.Append("											<tr>");
            sb.Append("												<td align='left' valign='top' style='padding:10px;font-family:Arial,Helvetica,sans-serif;color:rgb(38,38,38);");
            sb.Append("ont-size:12px;background-color:rgb(255,255,255);width:190px'>");
            sb.Append("													Asunto : " + comman.asunto + "");
            sb.Append("												</td>");
            sb.Append("											</tr>");

            sb.Append("											<tr>");
            sb.Append("												<td align='left' valign='top' style='padding:10px;font-family:Arial,Helvetica,sans-serif;color:rgb(38,38,38);");
            sb.Append("ont-size:12px;background-color:rgb(255,255,255);width:190px'>");
            sb.Append("													Mensaje : " + comman.Mensaje + "");
            sb.Append("												</td>");
            sb.Append("											</tr>");

            sb.Append("									");
            sb.Append("										</tbody>");
            sb.Append("										");
            sb.Append("										</table>");
            sb.Append("									</td>");
            sb.Append("									</tr>");
            sb.Append("									<tr>");
            sb.Append("									<td>");
            sb.Append("										");
            sb.Append("						</tbody>");
            sb.Append("						</table>");
            sb.Append("					</td>");
            sb.Append("					</tr>");
            sb.Append("				</tbody>");
            sb.Append("			</table>");
            sb.Append("        </td>");
            sb.Append("    </tr>");
            sb.Append("</tbody>");
            sb.Append("</table>");


            return sb.ToString();
        }

        public string formatoEnviaPedidoCliente(ListarCarritoResult carritoResult)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table width='100%' cellpadding='0' cellspacing='0' border='0' dir='ltr' style='font-size:16px;background-color:rgb(255,255,255)'>");
            sb.Append("<tbody>");
            sb.Append("<tr>");
            sb.Append("        <td align='center' valign='top' style='margin:0;padding:40'>");
            sb.Append("            <table align='center' border='0' cellspacing='0' cellpadding='0' width='700' bgcolor='#1ab394' style='width:700px;border:1px solid ");
            sb.Append("         transparent; ");
            sb.Append("order-radius:13px;margin:auto;background-color:#1872a6'>");
            sb.Append("                <tbody>");
            sb.Append("					<tr>");
            sb.Append("					<td>");
            sb.Append("						<table cellpadding='0' cellspacing='0' border='0' width='100%'>");
            sb.Append("						<tbody>");
            sb.Append("							<tr>");
            sb.Append("							<td valign='top' align='left' style='padding:0px;margin:0px'>");
            sb.Append("								<table cellpadding='0' cellspacing='0' border='0' width='100%'>");
            sb.Append("								<tbody>");
            sb.Append("									<tr>");
            sb.Append("									<td align='left' valign='top'>");
            sb.Append("									<table width='100%' border='0' cellpadding='0' cellspacing='0' align='center'>");
            sb.Append("										<tbody>");
            sb.Append("											<tr>");
            sb.Append("											<td align='left' valign='top' style='font-family:Arial,Helvetica,sans-serif;font-size:20px;border-radius:6px");
            sb.Append("	                                        color:rgb(' sb.Append('55,255,255)'>");
            sb.Append("												<div style='text-align:center'><span style='color:rgb(255,255,255);font-weight:bold'><br></span></div>");
            sb.Append("												<div style='text-align:center'><span style='color:rgb(255,255,255);font-weight:bold'> LIMEDICA PEDIDO ");
            sb.Append("                                             </span></div>");
            sb.Append("												<div style='text-align:center'><span style='color:rgb(255,255,255);font-weight:bold'><br></span></div>");
            sb.Append("												<span style='color:rgb(38,38,38)'></span>");
            sb.Append("											</td>");
            sb.Append("											</tr>");
            sb.Append("										</tbody>");
            sb.Append("									</table>");
            sb.Append("									</td>");
            sb.Append("									</tr>");
            sb.Append("									<tr>");
            sb.Append("									<td>");
            sb.Append("										<table width='100%' border='0' cellpadding='10' cellspacing='10' align='center'  bgcolor='white'>");
            sb.Append("										<tbody>");
            sb.Append("										       <tr>");
            sb.Append("												<td align='left' valign='top' style='padding:10px;font-family:Arial,Helvetica,sans-serif;color:rgb(38,38,38)");
            sb.Append("                                             font-size:12px;background-color:rgb(255,255,255);width:190px '  colspan='4'> ");
            sb.Append("													Sr(a). ");
            sb.Append("												</td>");
            sb.Append("											</tr>");
            sb.Append("											<tr>");
            sb.Append("												<td colspan='2' align='left' valign='top' style='padding:10px;font-family:Arial,Helvetica,sans-serif;color:rgb(38,38,38);");
            sb.Append("                                         font-size:12px;background-color:rgb(255,255,255);width:190px '  colspan='4'>");

            carritoResult.Hit.carritoDetalle.ForEach(Xmarket => {

                sb.Append($"					{Xmarket.bookISBN}								 ");

                

            });
            


            sb.Append("												</td>");
            sb.Append("											</tr>");
            sb.Append("												<tr>");
            sb.Append("												<td colspan='2' align='left' valign='top' style='padding:10px;font-family:Arial,Helvetica,sans-serif;color:rgb(38,38,38);");
            sb.Append("                                             font-size:14px;font-weight:bold;background-color:rgb(255,255,255)'  colspan='4'>");
            sb.Append("													<span style='font-family:Arial,Helvetica,sans-serif;color:rgb(38,38,38)'> Nro:  ");
            sb.Append("</span>");
            sb.Append("												</td>");
            sb.Append("											</tr>");
            sb.Append("									");
            sb.Append("										</tbody>");
            sb.Append("										");
            sb.Append("										</table>");
            sb.Append("									</td>");
            sb.Append("									</tr>");
            sb.Append("									<tr>");
            sb.Append("									<td>");
            sb.Append("										");
            sb.Append("						</tbody>");
            sb.Append("						</table>");
            sb.Append("					</td>");
            sb.Append("					</tr>");
            sb.Append("				</tbody>");
            sb.Append("			</table>");
            sb.Append("        </td>");
            sb.Append("    </tr>");
            sb.Append("</tbody>");
            sb.Append("</table>");


            return sb.ToString();
        }

    }
}
