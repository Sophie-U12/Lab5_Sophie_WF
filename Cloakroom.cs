using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5_Sophie_WF
{
    public class Cloakroom : IComparable
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public int NumberOfThings { get; set; }
        public double ThingsWeight { get; set; }

        public Cloakroom()
        {
            Name = string.Empty;
            Surname = string.Empty;
            Patronymic = string.Empty;
            NumberOfThings = 0;
            ThingsWeight = 0;
        }

        public Cloakroom(string name, string surname, string patronymic, int numberOfThings, double thingsWeight)
        {
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            NumberOfThings = numberOfThings;
            ThingsWeight = thingsWeight;
        }

        public override string ToString()
        {
            return $"ПІБ: {Surname} {Name} {Patronymic}. Кількість речей: {NumberOfThings}. Загальна вага речей(кг.): {ThingsWeight}.";
        }

        public int CompareTo(object obj)
        {
            if(obj is Cloakroom)
            {
                var temp = obj as Cloakroom;
                return Math.Round(this.ThingsWeight, 3).CompareTo(Math.Round(temp.ThingsWeight, 3));
            }
            else if(obj is Node<Cloakroom>)
            {
                var temp = obj as Node<Cloakroom>;
                return Math.Round(this.ThingsWeight, 3).CompareTo(Math.Round(temp.Value.ThingsWeight, 3));
            }
            else if (obj is double)
            {
                return Math.Round(this.ThingsWeight, 3).CompareTo(Math.Round((double)obj, 3));
            }

            throw new ArgumentException();
        }
    }
}
