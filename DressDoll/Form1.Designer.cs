using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;

namespace DressDoll
{
    partial class Form1 
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.parteEntera = new System.Windows.Forms.PictureBox();
            this.zapatos = new System.Windows.Forms.PictureBox();
            this.parteAbajo = new System.Windows.Forms.PictureBox();
            this.parteArriba = new System.Windows.Forms.PictureBox();
            this.cuerpo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.parteEntera)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zapatos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.parteAbajo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.parteArriba)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cuerpo)).BeginInit();
            this.SuspendLayout();
            // 
            // parteEntera
            // 
            this.parteEntera.BackColor = System.Drawing.Color.Transparent;
            this.parteEntera.Location = new System.Drawing.Point(169, 45);
            this.parteEntera.Name = "parteEntera";
            this.parteEntera.Size = new System.Drawing.Size(376, 734);
            this.parteEntera.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.parteEntera.TabIndex = 4;
            this.parteEntera.TabStop = false;
            // 
            // zapatos
            // 
            this.zapatos.BackColor = System.Drawing.Color.Transparent;
            this.zapatos.Location = new System.Drawing.Point(169, 45);
            this.zapatos.Name = "zapatos";
            this.zapatos.Size = new System.Drawing.Size(376, 734);
            this.zapatos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.zapatos.TabIndex = 3;
            this.zapatos.TabStop = false;
            // 
            // parteAbajo
            // 
            this.parteAbajo.BackColor = System.Drawing.Color.Transparent;
            this.parteAbajo.Location = new System.Drawing.Point(169, 45);
            this.parteAbajo.Name = "parteAbajo";
            this.parteAbajo.Size = new System.Drawing.Size(376, 734);
            this.parteAbajo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.parteAbajo.TabIndex = 2;
            this.parteAbajo.TabStop = false;
            // 
            // parteArriba
            // 
            this.parteArriba.BackColor = System.Drawing.Color.Transparent;
            this.parteArriba.Location = new System.Drawing.Point(169, 45);
            this.parteArriba.Name = "parteArriba";
            this.parteArriba.Size = new System.Drawing.Size(376, 734);
            this.parteArriba.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.parteArriba.TabIndex = 1;
            this.parteArriba.TabStop = false;
            // 
            // cuerpo
            // 
            this.cuerpo.BackColor = System.Drawing.Color.Transparent;
            this.cuerpo.Image = ((System.Drawing.Image)(resources.GetObject("cuerpo.Image")));
            this.cuerpo.Location = new System.Drawing.Point(169, 45);
            this.cuerpo.Name = "cuerpo";
            this.cuerpo.Size = new System.Drawing.Size(376, 734);
            this.cuerpo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.cuerpo.TabIndex = 0;
            this.cuerpo.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(699, 791);
            this.Controls.Add(this.parteEntera);
            this.Controls.Add(this.zapatos);
            this.Controls.Add(this.parteAbajo);
            this.Controls.Add(this.parteArriba);
            this.Controls.Add(this.cuerpo);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.parteEntera)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zapatos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.parteAbajo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.parteArriba)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cuerpo)).EndInit();
            this.ResumeLayout(false);

        }

        private void InicializarImagenes() {

            //Poner encima del cuerpo parte de abajo (CAPA 1)
            cuerpo.Controls.Add(parteAbajo);
            parteAbajo.Location = new Point(0, 0);
            parteAbajo.BackColor = Color.Transparent;

            //Poner encima de la capa de abajo la parte de arriba (CAPA 2)
            parteAbajo.Controls.Add(parteArriba);
            parteArriba.Location = new Point(0, 0);
            parteArriba.BackColor = Color.Transparent;

            //Poner encima los zapatos (CAPA 3)
            parteArriba.Controls.Add(zapatos);
            zapatos.Location = new Point(0, 0);
            zapatos.BackColor = Color.Transparent;

            //Poner encima de todo la parte entera (CAPA 4)
            zapatos.Controls.Add(parteEntera);
            parteEntera.Location = new Point(0, 0);
            parteEntera.BackColor = Color.Transparent;

            /*parteArriba.Image = DressDoll.Properties.Resources.Spring_Cardigan;
            parteAbajo.Image = DressDoll.Properties.Resources.Summer_Skirt;
            zapatos.Image = DressDoll.Properties.Resources.Summer_Sandals;
            parteEntera.Image = DressDoll.Properties.Resources.Winter_Dress;
            parteEntera.Image = DressDoll.Properties.Resources.TrajeFestivo;
          */
            this.BackgroundImage = DressDoll.Properties.Resources.parque;
        }
        #endregion

        private System.Windows.Forms.PictureBox cuerpo;
        private PictureBox parteArriba;
        private PictureBox parteAbajo;
        private PictureBox zapatos;
        private PictureBox parteEntera;
    }
}

