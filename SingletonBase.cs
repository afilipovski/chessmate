using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMate
{
    public abstract class SingletonBase<T> where T : class, new()
    {
        private static readonly Lazy<T> _instance = new Lazy<T>(() => new T());

        public static T Instance => _instance.Value;

        protected SingletonBase()
        {
            if (_instance.IsValueCreated)
            {
                throw new InvalidOperationException("Instance already created.");
            }
        }
    }
}
