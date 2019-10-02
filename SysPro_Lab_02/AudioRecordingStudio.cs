using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysPro_Lab_02
{
    class AudioRecordingStudio
    {
        // properties
        public string Name { get; }
        public string Adress { get; }

        public int NumberOfWorkers { get; private set; }

        public float TrackRecordCost { get; private set; }
        public int TrackRecordDuration { get; private set; }

        public float WorkerWage { get; private set; }
        public float TotalWorkersWage { get { return WorkerWage * NumberOfWorkers; } }

        public float availableMoney { get; private set; }

        public int NumberOfInstruments { get; private set; }

        public int NumberOfRooms { get; private set; }
    }
}
