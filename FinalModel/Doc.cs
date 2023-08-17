using FinalModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FinalModel
{
    class Doctor
    {
        public int Id { get; set; }
        public int ProcessingTime { get; set; }
        public MyQueue.Queue<Patient> Queue { get; set; }

        public Doctor(int id, int processingTime)
        {
            Id = id;
            ProcessingTime = processingTime;
            Queue = new MyQueue.Queue<Patient>();
        }
       
    }
}
