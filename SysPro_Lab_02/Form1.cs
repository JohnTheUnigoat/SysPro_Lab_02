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

        private BindingSource bs;

        public Form1()
        {
            InitializeComponent();

            objectsList = new List<AudioRecordingStudio>();

            bs = new BindingSource();

            bs.DataSource = objectsList;
            CreatedObjects.DataSource = bs;

            FieldName.DataBindings.Add("Text", bs, "Name");
            FieldAdress.DataBindings.Add("Text", bs, "Adress");
            FieldWorkers.DataBindings.Add("Text", bs, "NumberOfWorkers");
            FieldRecordCost.DataBindings.Add("Value", bs, "TrackRecordCost");
            FieldRecordDuration.DataBindings.Add("Value", bs, "TrackRecordDuration");
            FieldWorkerWage.DataBindings.Add("Value", bs, "WorkerWage");
            FieldTotalWage.DataBindings.Add("Text", bs, "TotalWorkersWage");
            FieldMoney.DataBindings.Add("Text", bs, "AvailableMoney");
            FieldInstrumentCount.DataBindings.Add("Text", bs, "NumberOfInstruments");
            FieldRoomCount.DataBindings.Add("Text", bs, "NumberOfRooms");
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

            bs.Add(audioStudio);

            CreatedObjects.SelectedIndex = CreatedObjects.Items.Count - 1;

            ConstructorName.Clear();
            ConstructorAdress.Clear();
            ConstructorMoney.Value = 0;
            ConstructorRecordCost.Value = 0;
            ConstructorRecordDuration.Value = 30;
        }

        private void ButtonEarn_Click(object sender, EventArgs e)
        {
            ((AudioRecordingStudio)bs.Current).Earn((float)UpDownEarn.Value);
            bs.ResetCurrentItem();
        }

        private void ButtonHire_Click(object sender, EventArgs e)
        {
            ((AudioRecordingStudio)bs.Current).HireWorker();
            bs.ResetCurrentItem();
        }

        private void ButonFire_Click(object sender, EventArgs e)
        {
            ((AudioRecordingStudio)bs.Current).FireWorker();
            bs.ResetCurrentItem();
        }

        private void ButtonAddInstruments_Click(object sender, EventArgs e)
        {
            int count = (int)UpDownInstruments.Value;

            if (!((AudioRecordingStudio)bs.Current).BuyInstruments(count))
                MessageBox.Show("You don't have enough money!", "Error occured!");

            bs.ResetCurrentItem();
        }

        private void ButtonThrowInstrument_Click(object sender, EventArgs e)
        {
            ((AudioRecordingStudio)bs.Current).ThrowAwayInstrument();
            bs.ResetCurrentItem();
        }

        private void ButtonAddRoom_Click(object sender, EventArgs e)
        {
            if (!((AudioRecordingStudio)bs.Current).AddRoom())
                MessageBox.Show("You don't have enough money!", "Error occured!");

            bs.ResetCurrentItem();
        }

        private void ButtonDemoishRoom_Click(object sender, EventArgs e)
        {
            ((AudioRecordingStudio)bs.Current).DemolishRoom();
            bs.ResetCurrentItem();
        }

        private void ButtonClone_Click(object sender, EventArgs e)
        {
            var selected = (AudioRecordingStudio)bs.Current;

            var newStudio = (AudioRecordingStudio)selected.Clone();
            newStudio.Name += " (copy)";

            bs.Add(newStudio);
            bs.Position = bs.Count - 1;
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            bs.RemoveCurrent();
        }
    }
}
