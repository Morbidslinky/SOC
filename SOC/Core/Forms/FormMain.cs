﻿using SOC.Classes.Common;
using SOC.Classes.QuestBuild;
using SOC.QuestObjects.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace SOC.UI
{
    public partial class FormMain : Form
    {
        MasterManager managerMaster = new MasterManager(new ManagerArray());

        private SetupDisplay setupPage;
        private DetailDisplay detailPage;
        private Waiting waitingPage = new Waiting();
        private int pageNum = 0;

        public FormMain()
        {
            setupPage = new SetupDisplay(managerMaster);
            detailPage = new DetailDisplay(managerMaster);
            InitializeComponent();

            GoToPanel();
            buttonBack.Visible = false;
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            pageNum++;
            this.GoToPanel();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            pageNum--;
            this.GoToPanel();
        }
        private bool isFilled()
        {
            //return true; // FOR DEBUG
            if (string.IsNullOrEmpty(setupPage.textBoxFPKName.Text) || string.IsNullOrEmpty(setupPage.textBoxQuestNum.Text) || string.IsNullOrEmpty(setupPage.textBoxQuestTitle.Text) || string.IsNullOrEmpty(setupPage.textBoxQuestDesc.Text))
                return false;
            if (setupPage.comboBoxCategory.SelectedIndex == -1 || setupPage.comboBoxReward.SelectedIndex == -1 || setupPage.comboBoxProgressNotifs.SelectedIndex == -1 || setupPage.comboBoxRegion.SelectedIndex == -1)
                return false;
            if (setupPage.comboBoxCP.Enabled)
                if (setupPage.comboBoxCP.SelectedIndex == -1 || setupPage.comboBoxLoadArea.SelectedIndex == -1 || setupPage.comboBoxRadius.SelectedIndex == -1 || string.IsNullOrEmpty(setupPage.textBoxXCoord.Text) || string.IsNullOrEmpty(setupPage.textBoxYCoord.Text) || string.IsNullOrEmpty(setupPage.textBoxZCoord.Text))
                    return false;

            return true;
        }
        private void GoToPanel()
        {
            switch (pageNum)
            {
                case 0:
                    ShowSetup();
                    break;

                case 1:
                    if (isFilled())
                    {
                        ShowWait();
                        Application.DoEvents();
                        ShowDetails();
                    }
                    else
                    {
                        MessageBox.Show("Please fill in the remaining Setup and Flavor Text fields.", "Missing Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        pageNum--;
                        return;
                    }
                    break;

                case 2:
                    BuildQuest();
                    pageNum--;
                    break;
            }
        }

        private void ShowSetup()
        {
            buttonBack.Visible = false;
            
            panelMain.Controls.Clear();
            panelMain.Controls.Add(setupPage);
            setupPage.EnableScrolling();
            managerMaster.UpdateAllDetailsFromControl();
            managerMaster.RefreshAllStubTexts();
            buttonNext.Text = "Next >>";
        }

        private void ShowWait()
        {
            setupPage.DisableScrolling();
            panelMain.Controls.Clear();
            panelMain.Controls.Add(waitingPage);

            buttonNext.Enabled = false;
        }

        private void ShowDetails()
        {
            buttonBack.Visible = true;
            buttonNext.Text = "Build";
            detailPage.RefreshObjectPanels(setupPage.GetCoreDetails());
            buttonNext.Enabled = true;

            panelMain.Controls.Clear();
            panelMain.Controls.Add(detailPage);
        }

        private void BuildQuest()
        {
            managerMaster.UpdateAllDetailsFromControl();
            Quest quest = new Quest(setupPage.GetCoreDetails(), managerMaster.GetQuestObjectDetails());
            
            if (BuildManager.Build(quest))
            {
                MessageBox.Show("Build Complete", "Sideop Companion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Build Failed", "Sideop Companion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FormMain_Activated(object sender, EventArgs e)
        {
            setupPage.refreshNotifsList();
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog loadFile = new OpenFileDialog();
            loadFile.Filter = "Xml Files|*.xml|All Files|*.*";

            DialogResult result = loadFile.ShowDialog();
            if (result != DialogResult.OK) return;

            if (pageNum != 0)
            {
                pageNum = 0; GoToPanel();
            }

            Quest quest = new Quest();

            if (quest.Load(loadFile.FileName))
            {
                managerMaster.SetManagerArray(new ManagerArray(quest.questObjectDetails));
                setupPage.managerMaster.ToString();
                setupPage.SetForm(quest.coreDetails);
                managerMaster.RefreshAllStubTexts();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isFilled())
            {
                DialogResult result = MessageBox.Show("Do you want to save this Sideop to an Xml file?", "SOC", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                    Save();
                else if (result == DialogResult.Cancel)
                    e.Cancel = true;
            }
        }

        private void Save()
        {
            CoreDetails core = setupPage.GetCoreDetails();
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Xml File|*.xml";
            saveFile.FileName = core.FpkName;
            DialogResult saveResult = saveFile.ShowDialog();
            if (saveResult != DialogResult.OK) return;
            if (pageNum != 0)
            {
                pageNum = 0; GoToPanel();
            }
            Quest quest = new Quest(core, managerMaster.GetQuestObjectDetails());
            quest.Save(saveFile.FileName);
        }

        private void buttonOpenFolder_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(AppDomain.CurrentDomain.BaseDirectory);
            }
            catch { }
        }

        private void buttonBatchBuild_Click(object sender, EventArgs e)
        {
            OpenFileDialog loadFile = new OpenFileDialog();
            loadFile.Filter = "Xml Files|*.xml|All Files|*.*";
            loadFile.Multiselect = true;

            DialogResult result = loadFile.ShowDialog();
            if (result != DialogResult.OK) return;
            
            List<Quest> quests = new List<Quest>();

            int failedCount = 0;
            foreach (string filePath in loadFile.FileNames)
            {
                Quest quest = new Quest();
                if (quest.Load(filePath))
                {
                    if (!quests.Exists(questInList => questInList.coreDetails.FpkName == quest.coreDetails.FpkName)
                        && !quests.Exists(questInList => questInList.coreDetails.QuestNum == quest.coreDetails.QuestNum))
                        quests.Add(quest);
                    else failedCount++;
                }
                else failedCount++;
            }
            if (failedCount > 0)
                MessageBox.Show($"{failedCount} Sideops could not be built \n(Either caused by failing to load Xml file(s) or more than one sideop using the same .FPK Filename/Quest Number)", "Sideop Companion", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (quests.Count > 0)
            {
                if (BuildManager.Build(quests.ToArray()))
                {
                    MessageBox.Show("Batch Build Complete", "Sideop Companion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Batch Build Failed", "Sideop Companion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
