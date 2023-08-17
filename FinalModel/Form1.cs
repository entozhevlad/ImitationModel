using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FinalModel
{
    public partial class Form1 : Form
    {
        Random rand = new Random();
        int M, N;
        List<Patient> patients = new List<Patient>();
        List<Doctor> doctors = new List<Doctor>();
        List<int> number = new List<int>();
        List<int> times = new List<int>();
        public Form1()
        {
            InitializeComponent();
        }
        private int[] PatientsInQueue(List<Doctor> mas)
        {
            int[] doctorsQue = new int[mas.Count];
            for (int i = 0; i < mas.Count; i++)
            {
                doctorsQue[i] = mas[i].Queue.Count;
            }
            return doctorsQue;
        }
        private void UnscrupulousDoctor()
        {
            doctors[rand.Next(0, N)].ProcessingTime = rand.Next(21, 25);
        }
        private int[] DelPat(int N)
        {
            int[] b = new int[N];
            for (int i = 0; i < N; i++)
            {
                b[i] = 0;
            
            }
            return b;
            
        }
        public void PatientReady(System.Windows.Forms.TextBox textBox1) { 
            List<Patient> ha = new List<Patient>();
            for (int i = 0; i < patients.Count; i++) {
                if (patients[i].Cab.Count == N) {
                    textBox1.Text += "Пациент " + patients[i].Id + " прошёл всех врачей и покидает больницу" + Environment.NewLine;
                    textBox1.Update();
                }
            }
        
        }

        private int SmallestQueue(Patient currentPatient)
        {
            int smallest = doctors[0].Queue.Count;
            int numb = 0;
            for(int i =0; i<doctors.Count;i++)
            {
                if (doctors[i].Queue.Count == 0 && currentPatient.Cab.Contains(i) == false) {
                    smallest = doctors[i].Queue.Count;
                    numb = i;
                    break;
                }
                if (doctors[i].Queue.Count < smallest && currentPatient.Cab.Contains(i) == false){
                    smallest = doctors[i].Queue.Count;
                    numb = i;
                
                }
            
            }
            return numb;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = 100;
            M = int.Parse(textBox2.Text); // количество сотрудников
            N = int.Parse(textBox3.Text); // количество врачей
            int[] docNumb = new int[N];
            for (int i = 0; i < N; i++)
            {
                times.Add(rand.Next(1, 20));
                number.Add(i + 1);
            }
            for (int i = 1; i < M; i++)
            {
                Patient p = new Patient(i);
                patients.Add(p);
            }
            for (int i = 0; i < N; i++)
            {
                int time = rand.Next(1, 20);
                Doctor doctor = new Doctor(i+1,time);
                doctors.Add(doctor);
                docNumb[i] = i + 1;
            }
            int[] b = PatientsInQueue(doctors);
            if (radioButton1.Checked) {
                UnscrupulousDoctor();
            }
            

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int[] docNumb = new int[N];
            int[] b;
            PatientReady(textBox1);
            if (patients.Count != 0)
            {
                b = PatientsInQueue(doctors);
            }
            else {
                b = DelPat(N);

            }

            Predicate<Patient> predicate = patient => patient.Cab.Count == N;


            patients.RemoveAll(predicate);
            for (int i = 0; i < patients.Count; i++)
            {
                int cabNumb = SmallestQueue(patients[i]);
                if (patients[i].NumbersOfQueue!=3)
                {
                    while (patients[i].NumbersOfQueue != 3)
                    {
                        doctors[cabNumb].Queue.Enqueue(patients[i]);
                        patients[i].InProcess = true;
                        patients[i].time = doctors[cabNumb].ProcessingTime;
                        patients[i].NumbersOfQueue++;
                    }

                }

            }
            for (int i = 0; i < doctors.Count; i++) {
                if (doctors[i].Queue.Count != 0)
                {
                        doctors[i].Queue.First.Decitime();
                        if (doctors[i].Queue.First.time == 0)
                        {
                            doctors[i].Queue.First.InProcess = false;
                            doctors[i].Queue.First.NumbersOfQueue--;
                            doctors[i].Queue.First.AddCab(i);
                            doctors[i].Queue.Dequeue();

                        }
                }
            }
            
            
            chart1.Series[0].Points.DataBindXY(docNumb, b);
            chart1.Update();
           
        }
     
    }
}
