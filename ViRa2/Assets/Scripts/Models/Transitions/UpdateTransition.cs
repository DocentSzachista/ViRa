using System;

namespace Assets.Scripts.Models.Transitions
{
    /// <summary>
    /// Obiekt wykorzystywany do zmiany tranzycji zadania
    /// </summary>
    [Serializable]
    public class UpdateTransition
    {
        /// <summary>
        /// Id tranzycji na ktora ma zostać przerzucone zadanie
        /// </summary>
        public TransitionId transition;
    }
}
