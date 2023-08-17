using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace FinalModel
{
    class Patient
    {
        public int Id { get; set; }
        public int time;
        public List<int> Cab = new List<int>();
        public bool InProcess = false;
        public bool InCabin = false;
        public bool original = true;
        public int NumbersOfQueue = 0;
        public Patient(int id)
        {
            Id = id;
        }
        public void Decitime()
        {
            time--;
        }
        public void AddCab(int i)
        {
            Cab.Add(i);
        }
        public void Addtime()
        {
            time++;
        }


    }
}
