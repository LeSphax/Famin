using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cost{


    public List<Ressource> list;

    public class Ressource
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private int number;

        public int Number
        {
            get { return number; }
            set { number = value; }
        }

        public Ressource(string name, int number)
        {
            this.name = name;
            this.number = number;
        }


    }
}
