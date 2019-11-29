using System;
using System.Windows.Forms;
using AVL.App.Presenters;
using AVL.UI.Views;


namespace AVL.UI
{
    public class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm form = new MainForm();
            AppPresenter presenter = new AppPresenter(form);
            Application.Run(form);
        }
    }
}
