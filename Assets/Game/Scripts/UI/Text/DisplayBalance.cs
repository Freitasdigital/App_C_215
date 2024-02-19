using System.Globalization;

namespace Game.Scripts.UI.Text
{
    public class DisplayBalance : DisplayText
    {
        protected override void SubscribeToEvent()
        {
            Wallet.Wallet.OnChangedMoney += SetText;
        }
        
        protected override void UnsubscribeFromEvent()
        {
            Wallet.Wallet.OnChangedMoney -= SetText;
        }

        protected override void Load()
        {
            SetText(Wallet.Wallet.Money);
        }
        
        public override void SetText(int value)
        {
            Text.text = value.ToString();
        }
    }
}