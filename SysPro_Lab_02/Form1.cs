using System;
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
        private List<AudioRecordingStudio> objectsList;

        private BindingSource selectedObjectBinding;
        private BindingSource createdObjectsBinding;

        public Form1()
        {
            InitializeComponent();

            objectsList = new List<AudioRecordingStudio>();

            selectedObjectBinding = new BindingSource();
            createdObjectsBinding = new BindingSource();

            createdObjectsBinding.DataSource = objectsList;
            CreatedObjects.DataSource = createdObjectsBinding;
        }

        private void SwitchButtons()
        {
            CreatedObjects.Enabled = !CreatedObjects.Enabled;

            GroupData.Enabled = !GroupData.Enabled;

            GroupMoney.Enabled = !GroupMoney.Enabled;
            GroupWorkers.Enabled = !GroupWorkers.Enabled;
            GroupInstruments.Enabled = !GroupInstruments.Enabled;
            GroupRooms.Enabled = !GroupRooms.Enabled;

            ButtonClone.Enabled = !ButtonClone.Enabled;
            ButtonDelete.Enabled = !ButtonDelete.Enabled;
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            if (objectsList.Count == 0)
                SwitchButtons();

            var audioStudio = new AudioRecordingStudio(
                ConstructorName.Text,
                ConstructorAdress.Text,
                (float)ConstructorMoney.Value,
                (float)ConstructorRecordCost.Value,
                (int)ConstructorRecordDuration.Value);

            objectsList.Add(audioStudio);
            createdObjectsBinding.ResetBindings(false);

            CreatedObjects.SelectedIndex = CreatedObjects.Items.Count - 1;
            SelectedClassChanged(this, new EventArgs());

            ConstructorName.Clear();
            ConstructorAdress.Clear();
            ConstructorMoney.Value = 0;
            ConstructorRecordCost.Value = 0;
            ConstructorRecordDuration.Value = 30;
        }

        private void SelectedClassChanged(object sender, EventArgs e)
        {
            if (objectsList.Count == 0)
            {
                selectedObjectBinding.Clear();
                SwitchButtons();
            }
            else
                selectedObjectBinding.DataSource = CreatedObjects.SelectedItem;

            FieldName.DataBindings.Clear();
            FieldName.DataBindings.Add("Text", selectedObjectBinding, "Name");

            FieldAdress.DataBindings.Clear();
            FieldAdress.DataBindings.Add("Text", selectedObjectBinding, "Adress");

            FieldWorkers.DataBindings.Clear();
            FieldWorkers.DataBindings.Add("Text", selectedObjectBinding, "NumberOfWorkers");

            FieldRecordCost.DataBindings.Clear();
            FieldRecordCost.DataBindings.Add("Value", selectedObjectBinding, "TrackRecordCost");

            FieldRecordDuration.DataBindings.Clear();
            FieldRecordDuration.DataBindings.Add("Value", selectedObjectBinding, "TrackRecordDuration");

            FieldWorkerWage.DataBindings.Clear();
            FieldWorkerWage.DataBindings.Add("Value", selectedObjectBinding, "WorkerWage");

            FieldTotalWage.DataBindings.Clear();
            FieldTotalWage.DataBindings.Add("Text", selectedObjectBinding, "TotalWorkersWage");

            FieldMoney.DataBindings.Clear();
            FieldMoney.DataBindings.Add("Text", selectedObjectBinding, "AvailableMoney");

            FieldInstrumentCount.DataBindings.Clear();
            FieldInstrumentCount.DataBindings.Add("Text", selectedObjectBinding, "NumberOfInstruments");

            FieldRoomCount.DataBindings.Clear();
            FieldRoomCount.DataBindings.Add("Text", selectedObjectBinding, "NumberOfRooms");
        }

        private void ButtonEarn_Click(object sender, EventArgs e)
        {
            ((AudioRecordingStudio)CreatedObjects.SelectedItem).Earn((float)UpDownEarn.Value);
            selectedObjectBinding.ResetBindings(false);
        }

        private void ButtonHire_Click(object sender, EventArgs e)
        {
            ((AudioRecordingStudio)CreatedObjects.SelectedItem).HireWorker();
            selectedObjectBinding.ResetBindings(false);
        }

        private void ButonFire_Click(object sender, EventArgs e)
        {
            ((AudioRecordingStudio)CreatedObjects.SelectedItem).FireWorker();
            selectedObjectBinding.ResetBindings(false);
        }

        private void ButtonAddInstruments_Click(object sender, EventArgs e)
        {
            int count = (int)UpDownInstruments.Value;
            if (!((AudioRecordingStudio)CreatedObjects.SelectedItem).BuyInstruments(count))
                MessageBox.Show("You don't have enough money!", "Error occured!");

            selectedObjectBinding.ResetBindings(false);
        }

        private void ButtonThrowInstrument_Click(object sender, EventArgs e)
        {
            ((AudioRecordingStudio)CreatedObjects.SelectedItem).ThrowAwayInstrument();
            selectedObjectBinding.ResetBindings(false);
        }

        private void ButtonAddRoom_Click(object sender, EventArgs e)
        {
            if (!((AudioRecordingStudio)CreatedObjects.SelectedItem).AddRoom())
                MessageBox.Show("You don't have enough money!", "Error occured!");

            selectedObjectBinding.ResetBindings(false);
        }

        private void ButtonDemoishRoom_Click(object sender, EventArgs e)
        {
            ((AudioRecordingStudio)CreatedObjects.SelectedItem).DemolishRoom();

            selectedObjectBinding.ResetBindings(false);
        }

        private void ButtonClone_Click(object sender, EventArgs e)
        {
            var selected = (AudioRecordingStudio)CreatedObjects.SelectedItem;

            var newStudio = (AudioRecordingStudio)selected.Clone();
            newStudio.Name += " (copy)";

            objectsList.Add(newStudio);
            createdObjectsBinding.ResetBindings(false);

            CreatedObjects.SelectedIndex = CreatedObjects.Items.Count - 1;
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            int previousIndex = CreatedObjects.SelectedIndex;

            objectsList.RemoveAt(previousIndex);

            createdObjectsBinding.ResetBindings(false);
        }

        private void SetName_Click(object sender, EventArgs e)
        {
            ((AudioRecordingStudio)CreatedObjects.SelectedItem).Name = FieldName.Text;
            createdObjectsBinding.ResetBindings(false);
            selectedObjectBinding.ResetBindings(false);
        }
    }
}
