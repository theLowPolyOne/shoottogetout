using System;

namespace STGO.UI
{
    public interface IUIManager
    {
        public void ShowResult(bool isVictory);
        public event Action<bool> OnResultShow;
    }
}