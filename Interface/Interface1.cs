using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmplManagementSystem.Interface
{
    internal interface IDashBoard
    {
         
        void DisplayMenu();
        void HandleMainmenu(string Username);
    }

    interface Isearch
    {
        void SearchbyId(int id);
        void SearchbyName(string id);

    }
    interface IReport
    {
        void GenerateReport();
    }
}
