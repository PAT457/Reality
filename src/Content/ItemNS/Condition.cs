using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reality.Content.ItemNS
{
    class Condition
    {

        //Condition Class. A neat little way to determine a object's condition, give names to it, and etc.

        //Condition Vars
        private string cName;
        private string cDesc;
        private int damagePercentage;

        //Preset Conditions
        public static Condition likeNew = new Condition("Great Condition", "Completely in well condition, Almost like brand new ", 100);

        public Condition(string name, string description, int conditionPercentage)
        {
            cName = name;
            cDesc = description;
            damagePercentage = conditionPercentage;
        }

        public string getName()
        {
            return cName;
        }

        public string getDesc()
        {
            return cDesc;
        }

        public static Condition getCondition(int damagePercent)
        {
            return likeNew; //Work on later
        }
    }
}
