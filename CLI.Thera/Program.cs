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
            List<Patient> patients = new List<Patient>();
            List<Physician> physicians = new List<Physician>();
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
                        DateTime birthdate;
                        while (!DateTime.TryParse(Console.ReadLine(), out birthdate))
                        {
                            Console.Write("Invalid format. Try again (MM/dd/yyyy): ");
                        }

                        Console.WriteLine("Enter race: ");
                        string race = Console.ReadLine() ?? "";

                        Console.WriteLine("Enter gender: ");
                        string gender = Console.ReadLine() ?? "";

                        patients.Add(new Patient
                        {
                            Name = name,
                            Address = address,
                            BirthDate = birthdate,
                            Race = race,
                            Gender = gender
                        });

                        Console.WriteLine("Patient added successfully!");
                        break;

                    case "2":
                        Console.WriteLine("Enter physician name: ");
                        string docName = Console.ReadLine() ?? "";

                        Console.WriteLine("Enter license number: ");
                        string license = Console.ReadLine() ?? "";

                        Console.WriteLine("Enter graduation date (MM/dd/yyyy): ");
                        DateTime gradDate;
                        while (!DateTime.TryParse(Console.ReadLine(), out gradDate))
                        {
                            Console.Write("Invalid format. Try again (MM/dd/yyyy): ");
                        }

                        Console.WriteLine("Enter specialization: ");
                        string specialization = Console.ReadLine() ?? "";

                        physicians.Add(new Physician
                        {
                            Name = docName,
                            LicenseNumber = license,
                            GraduationDate = gradDate,
                            Specialization = specialization
                        });

                        Console.WriteLine("Physician added successfully!");
                        break;

                    case "3":
                        Console.WriteLine("Enter physician's name for appointment: ");
                        string apptDocName = Console.ReadLine() ?? "";
                        Physician apptDoc = physicians.Find(d => d.Name == apptDocName)!;
                        if (apptDoc == null)
                        {
                            Console.WriteLine("Physician not found.");
                            break;
                        }

                        Console.WriteLine("Enter patient name for appointment: ");
                        string apptPatientName = Console.ReadLine() ?? "";
                        Patient apptPatient = patients.Find(p => p.Name == apptPatientName)!;
                        if (apptPatient == null)
                        {
                            Console.WriteLine("Patient not found.");
                            break;
                        }

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
                            if (a.Doctor!.Name == apptDoc.Name && a.Time == apptDate)
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
                            foreach (Patient p in patients)
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
                            foreach (Physician d in physicians)
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
