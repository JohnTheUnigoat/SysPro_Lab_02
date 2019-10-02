using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysPro_Lab_02
{
    class AudioRecordingStudio : ICloneable
    {
        // static fields //

        static float instrumentCost = 750.0f;
        static float roomCost = 5500.0f;

        // properties //

        public string Name { get; }
        public string Adress { get; }

        public int NumberOfWorkers { get; private set; }

        public float TrackRecordCost { get; set; }
        public int TrackRecordDuration { get; set; }

        public float WorkerWage { get; set; }
        public float TotalWorkersWage { get { return WorkerWage * NumberOfWorkers; } }

        public float AvailableMoney { get; private set; }

        public int NumberOfInstruments { get; private set; }

        public int NumberOfRooms { get; private set; }

        // methods //

        // constructor
        public AudioRecordingStudio(string name, string adress, float initialMoney)
        {
            Name = name;
            Adress = adress;
            AvailableMoney = initialMoney;
        }

        // money
        public void Earn(float amount)
        {
            if (amount <= 0)
                return;

            AvailableMoney += amount;
        }

        // workers
        public void HireWorker()
        {
            NumberOfWorkers++;
        }

        public void FireWorker()
        {
            NumberOfWorkers--;
        }

        // instruments
        public bool BuyInstruments(int count = 1)
        {
            float totalCost = instrumentCost * count;
            if (AvailableMoney < totalCost)
                return false;

            AvailableMoney -= totalCost;
            NumberOfInstruments += count;
            return true;
        }

        public void ThrowAwayInstrument()
        {
            NumberOfInstruments--;
        }

        // rooms
        public bool AddRoom()
        {
            int instrumentsMissing = NumberOfRooms * 2 - NumberOfInstruments;

            float totalCost = roomCost + instrumentCost * instrumentsMissing;

            if (AvailableMoney < totalCost)
                return false;

            AvailableMoney -= totalCost;
            NumberOfRooms++;
            NumberOfInstruments += instrumentsMissing;

            return true;
        }

        public void DemolishRoom()
        {
            if (NumberOfRooms == 0)
                throw new OperationCanceledException("No more rooms to demolish!");

            NumberOfRooms--;
        }

        public static AudioRecordingStudio operator ++(AudioRecordingStudio a)
        {
            a.AddRoom();
            return a;
        }

        public static AudioRecordingStudio operator --(AudioRecordingStudio a)
        {
            a.DemolishRoom();
            return a;
        }

        public object Clone()
        {
            var clone = new AudioRecordingStudio(Name, Adress, AvailableMoney);
            clone.NumberOfInstruments = NumberOfInstruments;
            clone.NumberOfRooms = NumberOfRooms;
            clone.NumberOfWorkers = NumberOfWorkers;
            clone.TrackRecordCost = TrackRecordCost;
            clone.TrackRecordDuration = TrackRecordDuration;
            clone.WorkerWage = WorkerWage;

            return clone;
        }

        public float this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0:
                        return WorkerWage;
                    case 1:
                        return TotalWorkersWage;
                    case 2:
                        return AvailableMoney;
                    default:
                        return 0;
                }
            }
        }
    }
}
