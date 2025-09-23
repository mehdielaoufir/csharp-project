using System;
using System.Collections.Generic;
using CLI.Thera.Models;


namespace MyApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            string choice = "";
            List<string> patients = new List<string>();
            List<string> physicians = new List<string>();
            List<Appointment> appointments = new List<Appointment>();

            do
            {
                Console.WriteLine("Enter a number choice: ");
                Console.WriteLine("1. Add Patient");
                Console.WriteLine("2. Add Physician");
                Console.WriteLine("3. Add Appointment");
                Console.WriteLine("4. List Patients");
                Console.WriteLine("5. List Physicians");
                Console.WriteLine("6. List Appointments");
                Console.WriteLine("0. Exit");
                Console.Write("Choice: ");

                choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Enter patient name: ");
                        string name = Console.ReadLine() ?? "";

                        Console.WriteLine("Enter address: ");
                        string address = Console.ReadLine() ?? "";

                        Console.WriteLine("Enter birthdate (MM/dd/yyyy): ");
                        string birthdate = Console.ReadLine() ?? "";

                        Console.WriteLine("Enter race: ");
                        string race = Console.ReadLine() ?? "";

                        Console.WriteLine("Enter gender: ");
                        string gender = Console.ReadLine() ?? "";

                        string patientInfo = $"{name}, {address}, {birthdate}, {race}, {gender}";
                        patients.Add(patientInfo);

                        Console.WriteLine("Patient added successfully!");
                        break;
                    case "2":
                        Console.WriteLine("Enter physician name: ");
                        string docName = Console.ReadLine() ?? "";

                        Console.WriteLine("Enter license number: ");
                        string license = Console.ReadLine() ?? "";

                        Console.WriteLine("Enter graduation date: ");
                        string gradDate = Console.ReadLine() ?? "";

                        string physicianInfo = $"{docName}, License: {license}, GradDate: {gradDate}";
                        physicians.Add(physicianInfo);

                        Console.WriteLine("Physician added successfully!");

                        break;
                    case "3":
                        Console.WriteLine("Enter physician's name for appointment: ");
                        string apptDoc = Console.ReadLine() ?? "";

                        Console.WriteLine("Enter patient name for appointment: ");
                        string apptPatient = Console.ReadLine() ?? "";

                        Console.WriteLine("Enter date and time of appointment (MM/dd/yyyy HH:mm): ");
                        if (!DateTime.TryParse(Console.ReadLine(), out DateTime apptDate))
                        {
                            Console.WriteLine("Invalid date format.");
                            break;
                        }

                        if (apptDate.Hour < 8 || apptDate.Hour >= 17 ||
                            apptDate.DayOfWeek == DayOfWeek.Saturday ||
                            apptDate.DayOfWeek == DayOfWeek.Sunday)
                        {
                            Console.WriteLine("Appointments must be scheduled between 8am–5pm, Mon–Fri.");
                            break;
                        }

                        bool doubleBooked = false;
                        foreach (Appointment a in appointments)
                        {
                            if (a.Doctor == apptDoc && a.Time == apptDate)
                            {
                                doubleBooked = true;
                                break;
                            }
                        }

                        if (doubleBooked)
                        {
                            Console.WriteLine("This physician is already booked at that time.");
                            break;
                        }

                        appointments.Add(new Appointment
                        {
                            Doctor = apptDoc,
                            Patient = apptPatient,
                            Time = apptDate
                        });

                        Console.WriteLine("Appointment scheduled successfully!");
                        break;

                    case "4":
                        Console.WriteLine("\n--- Patient List ---");
                        if (patients.Count == 0)
                        {
                            Console.WriteLine("No patients found.");
                        }
                        else
                        {
                            foreach (string p in patients)
                            {
                                Console.WriteLine(p);
                            }
                        }
                        break;
                    case "5":
                        Console.WriteLine("\n--- Physician List ---");
                        if (physicians.Count == 0)
                        {
                            Console.WriteLine("No physicians found.");
                        }
                        else
                        {
                            foreach (string d in physicians)
                            {
                                Console.WriteLine(d);
                            }
                        }
                        break;
                    case "6":
                        Console.WriteLine("\n--- Appointment List ---");
                        if (appointments.Count == 0)
                        {
                            Console.WriteLine("No appointments found.");
                        }
                        else
                        {
                            foreach (Appointment a in appointments)
                            {
                                Console.WriteLine(a);
                            }
                        }
                        break;
                    case "0":
                        Console.WriteLine("Exiting program...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice, try again.");
                        break;

                }

            } while (choice != "0");
        }
    }
}
