using System;

namespace STGO.UI
{
    public class UIManager : IUIManager
    {
        public event Action<bool> OnResultShow;

        public void ShowResult(bool isVictory)
        {
            OnResultShow?.Invoke(isVictory);
        }
    }
}