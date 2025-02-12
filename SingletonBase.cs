using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMate
{
    /// <summary>
    /// An abstract class that manages a single instance of another class using the singleton pattern.
    /// </summary>
    /// <typeparam name="T">A class which is supposed to have one instance.</typeparam>
    public abstract class SingletonBase<T> where T : class, new()
    {
        private static readonly Lazy<T> _instance = new Lazy<T>(() => new T());

        /// <summary>
        /// Method that fetches the instance.
        /// </summary>
        public static T Instance => _instance.Value;


        /// <summary>
        /// A constructor that prevents more than instance being created.
        /// </summary>
        /// <exception cref="InvalidOperationException">Gets thrown when someone tries to create a second instance.</exception>
        protected SingletonBase()
        {
            if (_instance.IsValueCreated)
            {
                throw new InvalidOperationException("Instance already created.");
            }
        }
    }
}
