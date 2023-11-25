namespace Assets.Scripts.Models.Transitions
{
    /// <summary>
    /// Obiekt wykorzystywany do zmiany tranzycji zadania
    /// </summary>
    public class MoveTransition
    {
        /// <summary>
        /// Id tranzycji na ktora ma zostać przerzucone zadanie
        /// </summary>
        public TransitionId Transition { get; set; }
    }
}
