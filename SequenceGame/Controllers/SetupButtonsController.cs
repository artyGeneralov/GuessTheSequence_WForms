using System;

namespace GameSetupForm
{
    public class SetupButtonsController
    {
        private const int _initialChances = 4;
        public static int Chances
        {
            get;
            private set;
        } = _initialChances;
        public static event Action OnChancesUpdated;
        public static void ChancesUpdate()
        {
            Chances++;
            if (Chances > 10 || Chances < _initialChances) { Chances = _initialChances; }
            Delegate[] actions = OnChancesUpdated?.GetInvocationList();
            foreach(Delegate action in actions)
            {
                (action as Action).Invoke();
            }
        }
    }
}
