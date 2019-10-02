using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysPro_Lab_02
{
    class AudioRecordingStudio
    {
        // static fields
        static float instrumentCost = 750.0f;
        static float roomCost = 5500.0f;

        // properties
        public string Name { get; }
        public string Adress { get; }

        public int NumberOfWorkers { get; private set; }

        public float TrackRecordCost { get; private set; }
        public int TrackRecordDuration { get; private set; }

        public float WorkerWage { get; private set; }
        public float TotalWorkersWage { get { return WorkerWage * NumberOfWorkers; } }

        public float AvailableMoney { get; private set; }

        public int NumberOfInstruments { get; private set; }

        public int NumberOfRooms { get; private set; }

        // constructor
        public AudioRecordingStudio(string name, string adress, float initialMoney)
        {
            Name = name;
            Adress = adress;
            AvailableMoney = initialMoney;
        }

        public void HireWorker()
        {
            NumberOfWorkers++;
        }

        public void FireWorker()
        {
            NumberOfWorkers--;
        }

        public bool BuyInstrument()
        {
            if (AvailableMoney < instrumentCost)
                return false;

            AvailableMoney -= instrumentCost;
            NumberOfInstruments++;
            return true;
        }

        public void ThrowAwayInstrument()
        {
            NumberOfInstruments--;
        }
    }
}
