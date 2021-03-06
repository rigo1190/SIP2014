﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace BusinessLogicLayer
{
    public static class Utilerias
    {
        /// <summary>
        /// TEST
        /// </summary>
        /// <param name="padre"></param>
        public static void LimpiarCampos(Control padre)
        {
            if (padre is HtmlInputText)
            {
                HtmlInputText t = (HtmlInputText)padre;
                t.Value = string.Empty;

            }
            else if (padre is TextBox)
            {
                TextBox t = (TextBox)padre;
                t.Text = string.Empty;
            }
            else if (padre is DropDownList)
            {
                DropDownList d = (DropDownList)padre;
                if (d.Items.Count > 0)
                    d.SelectedIndex = 0;
            }
            else if (padre.Controls.Count > 0)
            {
                foreach (Control c in padre.Controls)
                {
                    LimpiarCampos(c);
                }
            }

        }


        /// <summary>
        /// Funcion que se encarga de convertir una cadena a un valor int32
        /// Creado por Rigoberto TS
        /// 12/09/2014
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static int StrToInt(string valor)
        {
            int result;
            if (Int32.TryParse(valor, out result))
                return Convert.ToInt32(valor);

            return result;
        }

        public static DateTime? StrToDate(string valor)
        {
            DateTime result;
           
            if (DateTime.TryParse(valor, out result)) 
            {
                return Convert.ToDateTime(valor);
            }

            if (result == DateTime.MinValue) 
            {
               return null;
            }

            return result;

           
        }

        public static Decimal StrToDecimal(string valor)
        {
            decimal result;

            Decimal.TryParse(valor, out result);              

            return result;
        }



        /// <summary>
        /// Metodo encargado de cargar los combos para catalogos sencillos
        /// Creado por Rigoberto TS
        /// 17/09/2014
        /// </summary>
        /// <typeparam name="T">Clase</typeparam>
        /// <param name="dataList">Lista de objetos</param>
        /// <param name="d">DropDownlist</param>
        /// <param name="id">Campo Llave del Catalogo</param>
        /// <param name="descripcion">Campo Descripcion del catalogo que se mostrara</param>
        public static void ConstruyeCatalogos<T>(List<T> dataList, DropDownList d, string id, string descripcion) where T : class
        {
            d.DataSource = dataList;
            d.DataValueField = id;
            d.DataTextField = descripcion;
            d.DataBind();
        }

        public static void BindDropDownToEnum(DropDownList dropDown, Type enumType)
        { 
            string[] names = Enum.GetNames(enumType);

            for (int i = 0; i < names.Length; i++)
            {
                var type = enumType;
                var memInfo = type.GetMember(names[i]);
                var attributes = memInfo[0].GetCustomAttributes(typeof(DisplayAttribute),false);
                if (attributes.Length > 0) { names[i] = ((DisplayAttribute)attributes[0]).Name; }                
            }

            int[] values = (int[])Enum.GetValues(enumType);

            for (int i = 0; i < names.Length; i++)
            {
                dropDown.Items.Add(
                   new ListItem(
                     names[i],
                     values[i].ToString()
                    )
                );

            }

            dropDown.Items.Insert(0, new ListItem("Seleccione...", "0"));
        }


    }
}
