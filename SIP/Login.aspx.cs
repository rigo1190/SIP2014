﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Text;
using System.Security.Cryptography;
using BusinessLogicLayer;
using DataAccessLayer.Models;

namespace SIP
{
    public partial class Login : System.Web.UI.Page
    {
        private UnitOfWork uow;
        public string clave = "3ncript4d4"; // Clave de cifrado.
        private int ejercicioActivoId;


        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork();

            ejercicioActivoId = uow.EjercicioBusinessLogic.Get(ej => ej.Estatus == enumEstatusEjercicio.Activo).FirstOrDefault().Id;


        }

        protected void btnEntrar_Click(object sender, EventArgs e)
        {

            string strlogin = hiddenLogin.Value;
            string strContrasena = hiddenContrasena.Value;

            var user = uow.UsuarioBusinessLogic.Get(u => u.Login == strlogin && u.Password == strContrasena).FirstOrDefault(u => u.Login == strlogin && u.Password == strContrasena);

            if (user != null)
            {
                FormsAuthentication.RedirectFromLoginPage(user.Login, false);
                Session.Timeout = 60;
                Session["IsAuthenticated"] = true;
                Session["NombreUsuario"] = user.Nombre;
                Session["Login"] = user.Login;
                Session["IdUser"] = user.Id.ToString();

                //EL USUARIO VA A SER UNO DE ESTOS DOS GRANDES TIPOS: SEFIPLAN O DEPENDENCIA

                if (user.Rol.EsSefiplan) //USUARIO DE SEFIPLAN
                {
                    switch (user.RolId) //SEGUN SEA SU ROL DENTRO DE SEFIPLAN
                    {
                        case 1: //Desarrollador
                            Response.Redirect("~/Formas/frmSelectorEjercicio.aspx");
                            break;

                        case 2: //Ejecutivo

                            break;

                        case 3: //Administrador
                            Response.Redirect("~/Formas/Catalogos/Inicio.aspx");
                            break;

                        case 5: //Analista
                            Response.Redirect("~/Formas/frmSelectorEjercicio.aspx");
                            break;

                        case 6: //Control Financiero

                            Response.Redirect("~/Formas/SelectorEjercicioDependencia.aspx");
                            break;
                    }
                }
                else if (user.Rol.EsDependencia) //USUARIO DE DEPENDENCIA
                {
                    Response.Redirect("~/Formas/SelectorEjercicioDependencia.aspx");
                }

            }

            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "script", "fnc_ShowMensaje()", true);
                lblMensajes.Text = "Nombre de usuario o contraseña incorrectos";
                lblMensajes.CssClass = "error";
            }

        }

        private string Encriptar(string pass)
        {
            //cifrar datos
            byte[] llave; //Arreglo donde guardaremos la llave para el cifrado 3DES.

            byte[] arreglo = UTF8Encoding.UTF8.GetBytes(pass); //Arreglo donde guardaremos la cadena descifrada.

            // Ciframos utilizando el Algoritmo MD5.
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            llave = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(clave));
            md5.Clear();

            //Ciframos utilizando el Algoritmo 3DES.
            TripleDESCryptoServiceProvider tripledes = new TripleDESCryptoServiceProvider();
            tripledes.Key = llave;
            tripledes.Mode = CipherMode.ECB;
            tripledes.Padding = PaddingMode.PKCS7;
            ICryptoTransform convertir = tripledes.CreateEncryptor(); // Iniciamos la conversión de la cadena
            byte[] resultado = convertir.TransformFinalBlock(arreglo, 0, arreglo.Length); //Arreglo de bytes donde guardaremos la cadena cifrada.
            tripledes.Clear();


            return Convert.ToBase64String(resultado, 0, resultado.Length);
        }

    }
}