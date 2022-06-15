using System;

namespace XML
{
    public abstract class BaseElement 
    {
        protected void SetDependence<T>(string name, ref T property, T value) where T : IField
        {
            property = value;

            // Зависимости
        }

        protected abstract void Refresh();
    }
}