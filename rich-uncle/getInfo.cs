using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rich_uncle
{
    public partial class getInfo : Form
    {
        FormMain f;
        public getInfo(FormMain f)
        {
            InitializeComponent();
            this.f = f;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            f.GotInfo = false;
            Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            GlobalVariables.NumberOfPlayers = Convert.ToInt32(numOfPlayers.Value);
            GlobalVariables.BankDeposit = (ushort)Convert.ToUInt32(bankDeposit.Value);
            GlobalVariables.PlayersInitialValue = (short)Convert.ToInt32(playerInitDeposit.Value);
            GlobalVariables.FinishRoundBonus = (ushort)(finishRoundBonus.Value);
            f.GotInfo = true;
            Close();
        }
    }
}
