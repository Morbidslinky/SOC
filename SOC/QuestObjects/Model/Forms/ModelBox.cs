﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOC.UI;
using SOC.QuestObjects.Common;
using System.IO;

namespace SOC.QuestObjects.Model
{
    public partial class ModelBox : QuestBox
    {
        public int modelID;

        public ModelBox(Model m)
        {
            InitializeComponent();
            modelID = m.ID;

            m_textBox_xcoord.Text = m.position.coords.xCoord;
            m_textBox_ycoord.Text = m.position.coords.yCoord;
            m_textBox_zcoord.Text = m.position.coords.zCoord;

            m_textBox_xrot.Text = m.position.rotation.quatRotation.xval;
            m_textBox_yrot.Text = m.position.rotation.quatRotation.yval;
            m_textBox_zrot.Text = m.position.rotation.quatRotation.zval;
            m_textBox_wrot.Text = m.position.rotation.quatRotation.wval;

            m_comboBox_model.Items.AddRange(getModelList());

            if (m_comboBox_model.Items.Contains(m.model))
                m_comboBox_model.Text = m.model;
            else if (m_comboBox_model.Items.Count > 0)
                m_comboBox_model.SelectedIndex = 0;

            if (m_checkBox_collision.Enabled)
                m_checkBox_collision.Checked = m.collision;
        }

        private string[] getModelList()
        {

            string[] FileNames = Directory.GetFiles(ModelAssets.modelAssetsPath, "*.fmdl");
            for (int i = 0; i < FileNames.Length; i++)
            {
                FileNames[i] = Path.GetFileNameWithoutExtension(FileNames[i]);
            }
            return FileNames;
        }

        private bool hasGeom()
        {
            if (!string.IsNullOrEmpty(m_comboBox_model.Text))
            {
                string[] geomNames = Directory.GetFiles(ModelAssets.modelAssetsPath, "*.geom");
                for (int i = 0; i < geomNames.Length; i++)
                {
                    if (geomNames[i].Contains(m_comboBox_model.Text + ".geom"))
                        return true;
                }
            }
            return false;
        }

        private void m_comboBox_model_selectedIndexChanged(object sender, EventArgs e)
        {
            if (!hasGeom() && !string.IsNullOrEmpty(m_comboBox_model.Text))
            {
                DisableCollisionCheckBox("Missing .Geom");
            }
            else
            {
                EnableCollisionCheckBox();
            }

        }

        private void EnableCollisionCheckBox()
        {
            m_checkBox_collision.Enabled = true;
            m_checkBox_collision.Text = "Enable Collision";
        }

        private void DisableCollisionCheckBox(string reason)
        {
            m_checkBox_collision.Text = $"Enable Collision ({reason})";
            m_checkBox_collision.Checked = false;
            m_checkBox_collision.Enabled = false;
        }

        public override QuestObject getQuestObject()
        {
            return new Model(this);
        }
    }
}
