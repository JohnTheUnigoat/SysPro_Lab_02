﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SysPro_Lab_02
{
    public partial class Form1 : Form
    {
        private BindingSource bindingSource;

        public Form1()
        {
            InitializeComponent();

            bindingSource = new BindingSource();
        }

        private void SwitchButtons()
        {
            FieldName.Enabled = !FieldName.Enabled;
            SetName.Enabled = !SetName.Enabled;
            FieldAdress.Enabled = !FieldAdress.Enabled;
            SetAdress.Enabled = !SetAdress.Enabled;
            FieldWorkers.Enabled = !FieldWorkers.Enabled;
            FieldRecordCost.Enabled = !FieldRecordCost.Enabled;
            SetRecordCost.Enabled = !SetRecordCost.Enabled;
            FieldRecordDuration.Enabled = !FieldRecordDuration.Enabled;
            SetRecordDuration.Enabled = !SetRecordDuration.Enabled;
            FieldWorkerWage.Enabled = !FieldWorkerWage.Enabled;
            SetWage.Enabled = !SetWage.Enabled;

            UpDownEarn.Enabled = !UpDownEarn.Enabled;
            ButtonEarn.Enabled = !ButtonEarn.Enabled;
            ButtonHire.Enabled = !ButtonHire.Enabled;
            ButonFire.Enabled = !ButonFire.Enabled;
            UpDownInstruments.Enabled = !UpDownInstruments.Enabled;
            ButtonAddInstruments.Enabled = !ButtonAddInstruments.Enabled;
            ButtonThrowInstrument.Enabled = !ButtonThrowInstrument.Enabled;
            ButtonAddRoom.Enabled = !ButtonAddRoom.Enabled;
            ButtonDemoishRoom.Enabled = !ButtonDemoishRoom.Enabled;

            ButtonClone.Enabled = !ButtonClone.Enabled;
            ButtonDelete.Enabled = !ButtonDelete.Enabled;
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            if (CreatedObjects.Items.Count == 0)
                SwitchButtons();

            var audioStudio = new AudioRecordingStudio(
                ConstructorName.Text,
                ConstructorAdress.Text,
                (float)ConstructorMoney.Value,
                (float)ConstructorRecordCost.Value,
                (int)ConstructorRecordDuration.Value);

            CreatedObjects.Items.Add(audioStudio);

            CreatedObjects.SelectedIndex = CreatedObjects.Items.Count - 1;

            ConstructorName.Clear();
            ConstructorAdress.Clear();
            ConstructorMoney.Value = 0;
            ConstructorRecordCost.Value = 0;
            ConstructorRecordDuration.Value = 30;
        }

        private void SelectedClassChanged(object sender, EventArgs e)
        {
            bindingSource.DataSource = CreatedObjects.SelectedItem;

            FieldName.DataBindings.Clear();
            FieldName.DataBindings.Add("Text", bindingSource, "Name");

            FieldAdress.DataBindings.Clear();
            FieldAdress.DataBindings.Add("Text", bindingSource, "Adress");

            FieldWorkers.DataBindings.Clear();
            FieldWorkers.DataBindings.Add("Text", bindingSource, "NumberOfWorkers");

            FieldRecordCost.DataBindings.Clear();
            FieldRecordCost.DataBindings.Add("Value", bindingSource, "TrackRecordCost");

            FieldRecordDuration.DataBindings.Clear();
            FieldRecordDuration.DataBindings.Add("Value", bindingSource, "TrackRecordDuration");

            FieldWorkerWage.DataBindings.Clear();
            FieldWorkerWage.DataBindings.Add("Value", bindingSource, "WorkerWage");

            FieldTotalWage.DataBindings.Clear();
            FieldTotalWage.DataBindings.Add("Text", bindingSource, "TotalWorkersWage");

            FieldMoney.DataBindings.Clear();
            FieldMoney.DataBindings.Add("Text", bindingSource, "AvailableMoney");

            FieldInstrumentCount.DataBindings.Clear();
            FieldInstrumentCount.DataBindings.Add("Text", bindingSource, "NumberOfInstruments");

            FieldRoomCount.DataBindings.Clear();
            FieldRoomCount.DataBindings.Add("Text", bindingSource, "NumberOfRooms");
        }

        private void ButtonEarn_Click(object sender, EventArgs e)
        {
            ((AudioRecordingStudio)CreatedObjects.SelectedItem).Earn((float)UpDownEarn.Value);
            bindingSource.ResetBindings(false);
        }

        private void ButtonHire_Click(object sender, EventArgs e)
        {
            ((AudioRecordingStudio)CreatedObjects.SelectedItem).HireWorker();
            bindingSource.ResetBindings(false);
        }

        private void ButonFire_Click(object sender, EventArgs e)
        {
            ((AudioRecordingStudio)CreatedObjects.SelectedItem).FireWorker();
            bindingSource.ResetBindings(false);
        }

        private void ButtonAddInstruments_Click(object sender, EventArgs e)
        {
            int count = (int)UpDownInstruments.Value;
            if (!((AudioRecordingStudio)CreatedObjects.SelectedItem).BuyInstruments(count))
                MessageBox.Show("You don't have enough money!", "Error occured!");

            bindingSource.ResetBindings(false);
        }

        private void ButtonThrowInstrument_Click(object sender, EventArgs e)
        {
            ((AudioRecordingStudio)CreatedObjects.SelectedItem).ThrowAwayInstrument();
            bindingSource.ResetBindings(false);
        }

        private void ButtonAddRoom_Click(object sender, EventArgs e)
        {
            if (!((AudioRecordingStudio)CreatedObjects.SelectedItem).AddRoom())
                MessageBox.Show("You don't have enough money!", "Error occured!");

            bindingSource.ResetBindings(false);
        }

        private void ButtonDemoishRoom_Click(object sender, EventArgs e)
        {
            ((AudioRecordingStudio)CreatedObjects.SelectedItem).DemolishRoom();

            bindingSource.ResetBindings(false);
        }

        private void ButtonClone_Click(object sender, EventArgs e)
        {
            var selected = (AudioRecordingStudio)CreatedObjects.SelectedItem;

            var newStudio = (AudioRecordingStudio)selected.Clone();
            newStudio.Name += " (copy)";

            CreatedObjects.Items.Add(newStudio);

            CreatedObjects.SelectedIndex = CreatedObjects.Items.Count - 1;
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            int previousIndex = CreatedObjects.SelectedIndex;

            CreatedObjects.Items.RemoveAt(previousIndex);

            if (CreatedObjects.Items.Count == 0)
            {
                SwitchButtons();
                CreatedObjects.Text = "";
            }
            else if (previousIndex > 0)
                CreatedObjects.SelectedIndex = previousIndex - 1;
            else
                CreatedObjects.SelectedIndex = 0;
        }
    }
}
