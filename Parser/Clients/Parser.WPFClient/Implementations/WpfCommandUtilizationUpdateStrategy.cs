using System.Windows.Controls;

using Parser.LogFile.Reader.Contracts;

namespace Parser.WPFClient.Implementations
{
    public class WpfCommandUtilizationUpdateStrategy : ICommandUtilizationUpdateStrategy, ILabelContainer
    {
        public Label Label { get; set; }

        public void DisplayUpdate(string update)
        {
            if (this.Label != null)
            {
                this.Label.Content = update;
            }
        }
    }
}
