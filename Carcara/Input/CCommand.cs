using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Xna.Framework.Input
{
    //Exemplo de comando
    /*
     * CCommand command = new CCommand(3.0F, 
     *                          () => { Window.Title = "Result text"},
     *                          (args) => { return keyboards.Press(Keys.A)},
     *                          (args) => { return keyboards.Press(Keys.S)},
     *                          (args) => { return keyboards.Press(Keys.D)},
     */


    /// <summary>
    /// Representa um comando personalizável com um tempo de execução, os eventos de validação e o evento resultante caso as validações sejam satisfatórias.
    /// </summary>
    public class CCommand
    {
        private double elapsedTime = 0;
        private int currentIndex = 0;        

        /// <summary>Obtém o tempo de execução máximo para realizar esse comando.</summary>
        public float ExecutionTime { get; }
        /// <summary>Obtém a litsa de validações para que esse comando seja bem sucedido.</summary>
        public List<Func<CCommandEventArgs, bool>> Validations { get; }
        /// <summary>Obtém TRUE caso o comando foi bem sucedido após as chamadas do método de atualização.</summary>
        public bool IsSucess { get; private set; }
        /// <summary>Obtém a ação a ser realizada caso o comando seja bem sucedido.</summary>
        public Action Result { get; private set; }

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        /// <param name="executionTime">O tempo de execução máximo para realizar esse comando.</param>
        /// <param name="result">A ação a ser realizada caso o comando seja bem sucedido.</param>
        /// <param name="validations">A lista de validações para que esse comando seja bem sucedido.</param>
        public CCommand(float executionTime, Action result, params Func<CCommandEventArgs, bool>[] validations)
        {
            ExecutionTime = executionTime;
            Result = result;

            if (validations == null || validations.Length == 0)
                throw new ArgumentException($"{nameof(validations)} cannot be null or 0");

            Validations = validations.ToList();            
        }

        public void Update(GameTime gameTime)
        {
            IsSucess = false;
            elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

            if(elapsedTime <= ExecutionTime || ExecutionTime <= 0)
            {
                CCommandEventArgs e = new CCommandEventArgs(gameTime);

                if (Validations[currentIndex].Invoke(e))
                {
                    currentIndex++;

                    if (currentIndex > Validations.Count - 1)
                    {
                        Result?.Invoke();
                        IsSucess = true;
                        Reset();
                    }
                }
            }
            else
            {
                Reset();
            }
        }     
        
        private void Reset()
        {
            elapsedTime = 0;
            currentIndex = 0;
        }
    }
}
