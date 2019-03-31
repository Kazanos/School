using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo
{
    public enum EventType
    {
        Move,
        Watch
    }

    public class Event
    {
        public int when;
        public Process who;
        public EventType what;
        public Event(int when, Process who, EventType what)
        {
            this.when = when;
            this.who = who;
            this.what = what;
        }
    }

    public abstract class Process
    {
        public static char[] dividers = { ' ' };
        public string section;
        public string ID;
        public abstract void Handle(Event e);
        protected Model model;

        public void log(string message)
        {
            Console.WriteLine("{0}/{3} {1}: {2}", model.Time, ID, message, section);
        }
    }

    public class Calendar
    {
        private List<Event> events;

        public Calendar()
        {
            events = new List<Event>();
        }

        public void Add(int when, Process who, EventType what)
        {
            events.Add(new Event(when, who, what));
        }

        public void Remove(Process who, EventType what)
        {
            foreach (Event e in events)
            {
                if ((e.who == who) && (e.what == what))
                {
                    events.Remove(e);
                    return;
                }
            }
        }

        public Event First()
        {
            Event first = null;
            foreach (Event e in events)
            {
                if ((first == null) || (e.when < first.when))
                {
                    first = e;
                }
            }
            events.Remove(first);
            return first;
        }

        public Event Take()
        {
            return First();
        }
    }

    public class Animal : Process
    {
        public int speed;
        public int effectiveness;

        public Animal(Model model, string desc, string section)
        {
            this.model = model;
            string[] descriptions = desc.Split(Process.dividers, StringSplitOptions.RemoveEmptyEntries);
            this.ID = descriptions[0];
            this.section = section;
            this.speed = int.Parse(descriptions[1]);
            this.effectiveness = int.Parse(descriptions[2]);
        }

        public override void Handle(Event e)
        {
            throw new NotImplementedException();
        }
    }

    public class Visitor : Process
    {
        private int joy;
        private int arrival;
        private int patience;
        private List<string> Desires;

        public Visitor(Model model, string desc)
        {
            this.model = model;
            string[] descriptions = desc.Split(Process.dividers, StringSplitOptions.RemoveEmptyEntries);
            this.ID = descriptions[0];
            this.arrival = int.Parse(descriptions[1]);
            this.patience = int.Parse(descriptions[2]);
            Desires = new List<string>();
            for (int i = 3; i < descriptions.Length; i++)
            {
                Desires.Add(descriptions[i]);
            }
            this.section = "entrance";
            this.joy = 0;
            Console.WriteLine("Visitor : {0}", ID);
            model.Plan(arrival, this, EventType.Move);
        }

        public override void Handle(Event e)
        {
            switch (e.what)
            {
                case EventType.Move:
                    if (Desires.Count == 0)
                    {
                        if (section == "entrance")
                        {
                            log("leaving");
                        }
                    }
                    else
                    {
                        //Checks whether it's not taking too long to visit all the animals
                        if (patience < (model.Time - arrival))
                            {
                            bool found = false;
                           
                            foreach (Animal a in model.AllAnimals)
                            {
                                if (a.ID == Desires[0])
                                {
                                    found = true;
                                    GoTo(a.section, a);
                                }
                            }

                            if (!found)
                            {
                                //Immediately rehandles the event with the next desire
                                log("Couldnt find " + Desires[0]);
                                Desires.RemoveAt(0);
                                Handle(e);
                            }
                        }
                        else
                        {
                            log("Got impatient");
                            GoTo("entrance", null);
                        }
                    }
                    break;
                case EventType.Watch:
                    foreach (Animal a in model.AllAnimals)
                    {
                        if (a.ID == Desires[0])
                        {
                            log("Watching: " + Desires[0]);
                            Desires.RemoveAt(0);
                            model.Plan(model.Time + a.speed, this, EventType.Move);
                            joy += a.effectiveness;
                        }
                    }  
                    break;
                default:
                    break;
            }
        }

        private void GoTo(string where, Animal target)
        {
            int travelTime = 0;
            //Sets travel time according to a circular zoo structure, based on the order of sections in the data file
            if (target != null)
            {
                travelTime = Math.Min(model.Sections.IndexOf(target.section), model.Sections.Count - model.Sections.IndexOf(target.section));
            }
            else
            {
                travelTime = Math.Min(model.Sections.IndexOf(section), model.Sections.IndexOf(section) - model.Sections.Count);
            } 
            model.Plan(model.Time + travelTime, this, EventType.Watch);
            log("Going to " + Desires[0]);
        }
    }

    public class Model
    {
        public int Time;
        public List<Animal> AllAnimals = new List<Animal>();
        private Calendar calendar;
        public List<string> Sections = new List<string>();

        public void Plan(int when, Process who, EventType what)
        {
            calendar.Add(when, who, what);
        }

        public void Unplan(Process who, EventType what)
        {
            calendar.Remove(who, what);
        }

        public void CreateProcesses()
        {
            string currentSection = null;
            System.IO.StreamReader file = new System.IO.StreamReader("data.txt", Encoding.GetEncoding(1250));

            while (!file.EndOfStream)
            {
                string s = file.ReadLine();
                if (s != "")
                {
                    switch (s[0])
                    {
                        case 'A':
                            new Animal(this, s.Substring(1), currentSection);
                            break;
                        case 'S':
                            string x = s.Substring(1);
                            Sections.Add(x);
                            currentSection = x;
                            break;
                        case 'V':
                            new Visitor(this, s.Substring(1));
                            break;
                    }
                }
            }
            file.Close();
        }

        public int Calculate()
        {
            Time = 0;
            calendar = new Calendar();
            CreateProcesses();

            Event e;

            while ((e = calendar.Take()) != null)
            {
                Time = e.when;
                e.who.Handle(e);
            }
            return Time;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Model model = new Model();
            Console.WriteLine(model.Calculate());
            Console.ReadKey();
        }
    }
}
