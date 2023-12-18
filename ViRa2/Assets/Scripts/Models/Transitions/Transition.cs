using System;

namespace Assets.Scripts.Models.Transitions
{
    /// <summary>
    /// Typ zadania np TODO, do pobrania tranzycji i danych o Issue
    /// </summary>
    [Serializable]
    public class Transition
    {
        /// <summary>
        /// Id zadania
        /// </summary>
        public int id;

        /// <summary>
        /// Nazwa zadania
        /// </summary>
        public string name;
    }
}
