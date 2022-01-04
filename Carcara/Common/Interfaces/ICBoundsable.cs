namespace Microsoft.Xna.Framework
{
    /// <summary>
    /// Representa um objeto que expõe os seus limites na tela.
    /// </summary>
    public interface ICBoundsable
    {
        /// <summary>
        /// Obtém os limites de tela do objeto.
        /// </summary>
        Rectangle Bounds { get; }
    }
}
