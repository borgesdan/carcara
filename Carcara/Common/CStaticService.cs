using System;
using System.Collections.Generic;

namespace Microsoft.Xna.Framework
{
    /// <summary>
    /// Classe estática que armazena instâncias de classes para reutilização posterior.
    /// </summary>
    public static class CStaticService
    {
        static readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

        /// <summary>
        /// Adiciona um tipo ao serviço. Um tipo existente será sobrescrevido.
        /// </summary>
        public static void Add<T>(T provider) where T : class
        {
            Type type = typeof(T);

            if(provider == null)
                throw new ArgumentException($"{provider} cannot be null.");

            if (services.ContainsKey(type))
            {
                services[type] = provider;
            }

            services.Add(type, provider);
        }

        /// <summary>
        /// Remove uma instância da lista.. Retorna true caso sucesso.
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
            Type t = typeof(T);
            return services.ContainsKey(t) ? (T)services[t] : null;
        }

        /// <summary>
        /// Obtém uma instância da lista ao informar o seu tipo e a remove em seguida.
        /// </summary>
        public static T GetAndRemove<T>() where T : class         
        {
            T obj = Get<T>();
            Remove<T>();

            return obj;
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
