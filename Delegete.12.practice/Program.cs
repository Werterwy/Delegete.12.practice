using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegete._12.practice
{
    public class PropertyEventArgs : EventArgs
    {
        public string PropertyName { get; }

        public PropertyEventArgs(string propertyName)
        {
            PropertyName = propertyName;
        }
    }

    public delegate void PropertyEventHandler(object sender, PropertyEventArgs e);

    public interface IPropertyChanged
    {
        event PropertyEventHandler PropertyChanged;
    }

    public class ExampleClass : IPropertyChanged
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChange("Name");
                }
            }
        }

        public event PropertyEventHandler PropertyChanged;

        protected virtual void OnPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyEventArgs(propertyName));
        }
    }

    class Program
    {
        static void Main()
        {
            ExampleClass example = new ExampleClass();

            // Подписываемся на событие изменения свойства
            example.PropertyChanged += (sender, e) =>
            {
                Console.WriteLine($"Свойство {e.PropertyName} было изменено.");
            };

            // Изменяем свойство
            example.Name = "Новое значение";

            Console.ReadLine();
        }
    }
}
