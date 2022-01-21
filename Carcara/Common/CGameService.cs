using System;
using System.Collections.Generic;

namespace Microsoft.Xna.Framework
{
    /// <summary>
    /// Classe estática que armazena instâncias de classes para reutilização posterior.
    /// </summary>
    public static class CGameService
    {
        static Dictionary<Type, object> services = new Dictionary<Type, object>();

        /// <summary>
        /// Adiciona uma nova instância ao serviço.
        /// </summary>
        /// <param name="provider">A instância de uma classe a ser adicionada.</param>
        public static void Add<T>(T provider) where T : class
        {
            Type type = typeof(T);

            if (services.ContainsKey(type))
                throw new ArgumentException($"{provider}: A key with that specific type of class already exists.");

            services.Add(type, provider);
        }

        /// <summary>
        /// Remove uma determinada instância da lista determinada por seu tipo.
        /// </summary>
        public static bool Remove<T>() where T : class
        {
            return services.Remove(typeof(T));
        }

        /// <summary>
        /// Obtém uma instância da lista de instâncias.
        /// </summary>
        public static T Get<T>() where T : class
        {
            if(services.ContainsKey(typeof(T)))
            {
                object obj = services[typeof(T)];
                return (T)obj;
            }

            return null;
        }

        /// <summary>
        /// Limpa a lista de instâncias armazenadas.
        /// </summary>
        public static void Clear()
        {
            services.Clear();
        }
    }
}
