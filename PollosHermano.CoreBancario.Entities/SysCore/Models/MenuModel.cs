using System.Collections.Generic;

namespace PollosHermano.CoreBancario.Entities.SysCore.Models
{
    public class MenuModel : Menu
    {
        public MenuModel()
            : base()
        {
        }

        public List<MenuModel> Childrens { get; set; } = new List<MenuModel>();
    }
}
